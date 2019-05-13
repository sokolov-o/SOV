using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using SOV.Common;
using SOV.Geo;

namespace SOV
{
    /// <summary>
    /// Формат поля данных.
    /// </summary>
    public enum EnumFieldFormat : long
    {
        GRID = 1,
        XYZ = 2
    }

    [DataContract]
    public class Field
    {
        public Dictionary<string, object> MetaInfo = new Dictionary<string, object>();

        public Field(Grid grid, EnumFieldFormat fieldFormat, double predictTime, double[] data)
        {
            Grid = grid;
            FieldFormat = fieldFormat;
            Value = data;
        }
        /// <summary>
        /// Сетка поля
        /// </summary>
        [DataMember]
        public Grid Grid { get; set; }
        /// <summary>
        /// Заблаговременность поля.
        /// </summary>
        [DataMember]
        public double PredictTime { get; set; }
        /// <summary>
        /// Тип сетки поля
        /// </summary>
        [DataMember]
        public EnumFieldFormat FieldFormat { get; set; }
        /// <summary>
        /// Значения в узлах поля.
        /// </summary>
        [DataMember]
        public double[] Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (Grid != null && value != null && value.Length != Grid.PointsQ)
                    throw new Exception("(Grid != null && value.Length != Grid.PointsQ)");
                _value = value;
            }
        }
        double[] _value;

        public void ToFile(string path, char splitter = ';')
        {
            string format = "{0};{1};{2}".Replace(';', splitter);
            StreamWriter sw = new StreamWriter(path, false);
            try
            {
                Grid.MoveFirst();
                while (!Grid.EOF)
                {
                    sw.WriteLine(format, Grid.CurLonGrd, Grid.CurLatGrd, Value[Grid.CurPointIdx].ToString());
                    Grid.MoveNext();
                }
            }
            finally
            {
                sw.Close();
            }
        }
        /// <summary>
        /// Выделение узлов сетки, относящихся к региону.
        /// OSokolov@ferhri.ru
        /// </summary>
        /// <param name="geoReg">Регион.</param>
        /// <returns>Выделенное поле, возможно с отсутствием точек, или null, если сетка не задана.</returns>
        public Field Truncate(GeoRectangle geoReg)
        {
            if (this.FieldFormat == EnumFieldFormat.GRID && Grid == null)
                throw new Exception("(FieldFormat == EnumFieldFormat.GRID && Grid == null)");

            List<int>[] iLatLon = this.Grid.GetLatLonIdxs(geoReg);
            Grid grid = new Grid(null, Grid.TypeId,
                iLatLon[0].Count, iLatLon[0][0], Grid.LatStepMin,
                iLatLon[1].Count, iLatLon[1][0], Grid.LonStepMin,
                "Trancated from grid [" + Grid + "]");

            double[] values = null;
            List<int> iNodes = this.Grid.GetNodeIdxs(iLatLon);
            if (iNodes.Count > 0)
            {
                values = new double[iNodes.Count];

                for (int i = 0; i < iNodes.Count; i++)
                {
                    values[i] = Value[iNodes[i]];
                }
            }

            return new Field(grid, this.FieldFormat, this.PredictTime, values) { MetaInfo = this.MetaInfo };
        }
        static public double[] GetValuesAtPoint(List<Field> fields, GeoPoint point, EnumPointNearestType nearestType, EnumDistanceType distanceType)
        {
            double[] ret = new double[fields.Count];
            for (int i = 0; i < fields.Count; i++)
            {
                ret[i] = fields[i].GetValueAtPoint(point, nearestType, distanceType);
            }
            return ret;
        }
        public double[] GetValuesAtPoints(List<GeoPoint> points, EnumPointNearestType nearestType, EnumDistanceType distanceType)
        {
            double[] ret = new double[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] != null)
                {
                    ret[i] = GetValueAtPoint(points[i], nearestType, distanceType);
                }
                else
                {
                    ret[i] = double.NaN;
                }
            }
            return ret;
        }
        /// <summary>
        /// Интерполяция значений в заданную точку.
        /// </summary>
        /// <param name="point">Точка интерполяции.</param>
        /// <returns>Результат интерполяции, либо double.NaN, если точка не принадлежит региону сетки.</returns>
        public double GetValueAtPoint(GeoPoint point, EnumPointNearestType nearestType, Geo.EnumDistanceType distanceType)
        {
            List<int[/*iLat,iLon*/]> iLatLon = Grid.GetNearestPointsIdx(point.LatMin, point.LonMin);
            if (iLatLon == null) return double.NaN;

            double[] values = new double[iLatLon.Count];
            List<GeoPoint> nearestPoints = new List<GeoPoint>();

            for (int i = 0; i < iLatLon.Count; i++)
            {
                nearestPoints.Add(new GeoPoint(Grid.LatsMin[iLatLon[i][0]] / 60, Grid.LonsMin[iLatLon[i][1]] / 60));
                values[i] = Value[Grid.GetNodeIdx(iLatLon[i][0], iLatLon[i][1])];
            }

            return SOV.Geo.Geo.GetValueAtPoint(point, nearestPoints, values, nearestType, distanceType);
        }

        public double _DELME_GetValueAtPoint(GeoPoint point, EnumPointNearestType nearestType, Geo.EnumDistanceType distanceType)
        {
            List<int[/*latIdx,lonIdx*/]> iLatLon = Grid.GetNearestPointsIdx(point.LatMin, point.LonMin);
            if (iLatLon == null) return double.NaN;

            double[] dists = new double[iLatLon.Count];
            double[] values = new double[iLatLon.Count];
            double latrad0 = Vector.grad2Radians(point.LatGrd);
            double lonrad0 = Vector.grad2Radians(point.LonGrd);

            for (int i = 0; i < iLatLon.Count; i++)
            {
                dists[i] = Geo.Geo.SphereDistance(
                    lonrad0,
                    Vector.grad2Radians(Grid.LonsMin[iLatLon[i][1]] / 60),
                    latrad0,
                    Vector.grad2Radians(Grid.LatsMin[iLatLon[i][0]] / 60),
                    distanceType);
                values[i] = this.Value[Grid.GetNodeIdx(iLatLon[i][0], iLatLon[i][1])];
            }

            switch (nearestType)
            {
                // Берем значение в точке с минимальным расстоянием
                case EnumPointNearestType.Nearest:
                    int ind = System.Array.IndexOf(dists, dists.Min());
                    return values[ind];
                // Линейная взвешеная интерполяция
                case EnumPointNearestType.Interpolate:
                    double[] ret = Support.InterpolateLine(dists, values);
                    return ret[0];
                default:
                    throw new Exception("UNKNOWN GeoPoint.NearestType=" + nearestType);
            }

        }

        static public Field[] Truncate(List<Field> fields, GeoRectangle gr2Trancate)
        {
            Field[] ret = new Field[fields.Count];
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i] != null)
                    ret[i] = fields[i].Truncate(gr2Trancate);
            }
            return ret;
        }
    }
}
