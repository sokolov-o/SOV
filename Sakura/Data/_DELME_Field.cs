using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Sakura.Meta;
using System.IO;
using System.Runtime.Serialization;
using FERHRI.Common;

namespace FERHRI.Sakura.Data
{
    [DataContract]
    public class _DELME_Field
    {
        public _DELME_Field(_DELME_Grid grid, FERHRI.Sakura.Meta.Enums.FieldFormat fieldFormat, double predictTime, double[] data)
        {
            Grid = grid;
            FieldFormat = fieldFormat;
            Value = data;
        }
        /// <summary>
        /// Сетка поля
        /// </summary>
        [DataMember]
        public _DELME_Grid Grid { get; set; }
        /// <summary>
        /// Заблаговременность поля.
        /// </summary>
        [DataMember]
        public double PredictTime { get; set; }
        /// <summary>
        /// Тип сетки поля
        /// </summary>
        [DataMember]
        public FERHRI.Sakura.Meta.Enums.FieldFormat FieldFormat { get; set; }
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
        /// Выделение узлов сетки, попадающих в регион.
        /// Результирующий Grid = null (на 2016.11)
        /// 
        /// OSokolov@ferhri.ru
        /// </summary>
        /// <param name="geoReg">Регион.</param>
        /// <returns>Выделенное поле, возможно с отсутствием точек, или null, если сетка не задана.</returns>
        public _DELME_Field Extract(_DELME_GeoRectangle geoReg)
        {
            if (Grid != null)
            {
                double[] values = null;
                List<int>[] iLatLon = this.Grid.GetLatLonIdxs(geoReg);
                List<int> iNodes = this.Grid.GetNodeIdxs(iLatLon);
                if (iNodes.Count > 0)
                {
                    values = new double[iNodes.Count];

                    for (int i = 0; i < iNodes.Count; i++)
                    {
                        values[i] = Value[iNodes[i]];
                    }
                }
                _DELME_Grid grid = new _DELME_Grid(null, Grid.TypeId,
                    iLatLon[0].Count, Grid.LatsMin[iLatLon[0][0]], Grid.LatStepMin,
                    iLatLon[1].Count, Grid.LonsMin[iLatLon[1][0]], Grid.LonStepMin,
                    "Trancated from grid [" + Grid + "]");

                return new _DELME_Field(grid, this.FieldFormat, this.PredictTime, values);
            }
            return null;
        }
        /// <summary>
        /// Интерполяция значений в заданную точку.
        /// </summary>
        /// <param name="point">Точка интерполяции.</param>
        /// <returns>Результат интерполяции, либо double.NaN, если точка не принадлежит региону сетки.</returns>
        public double Interpolate(_DELME_GeoPoint point, _DELME_GeoPoint.NearestType nearestType, Sakura.Meta._DELME_Geo.DistanceType distanceType)
        {
            List<int[/*latIdx,lonIdx*/]> iLatLon = Grid.GetNearestPointsIdx(point.LatMin, point.LonMin);
            if (iLatLon == null) return double.NaN;

            double[] dists = new double[iLatLon.Count];
            double[] values = new double[iLatLon.Count];
            double latrad0 = Vector.grad2Radians(point.LatGrd);
            double lonrad0 = Vector.grad2Radians(point.LonGrd);

            for (int i = 0; i < iLatLon.Count; i++)
            {
                dists[i] = Meta._DELME_Geo.SphereDistance(
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
                case _DELME_GeoPoint.NearestType.Nearest:
                    int ind = System.Array.IndexOf(dists, dists.Min());
                    return values[ind];
                // Линейная интерполяция
                case _DELME_GeoPoint.NearestType.Interpolate:
                    double[] ret = Support.InterpolateLine(dists, values);
                    return ret[0];
                default:
                    throw new Exception("UNKNOWN GeoPoint.NearestType=" + nearestType);
            }

        }
    }
}
