﻿#define trace

using GeographicLib;
using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using System.Device.Location;
using SOV.Geo;

namespace SOV.SGMO
{
    public class Track : IdNameRE
    {
        public int SiteId { get; set; }
        /// <summary>
        /// Дата начала маршрута.
        /// </summary>
        public DateTime DateSUTC { get; set; }
        public int? ParentId { get; set; }

        public List<Track> ChildTracks { get; set; }
        public List<TrackPoint> Points { get; set; }

        public override string ToString()
        {
            return Name + " - " + Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refPoints"></param>
        /// <param name="dateSUTC"></param>
        /// <param name="startLocation"></param>
        /// <param name="objectSpeed">m/s</param>
        /// <param name="nextRefPointIndex"></param>
        /// <param name="hourStart"></param>
        /// <param name="hourFinish"></param>
        /// <param name="hourStep"></param>
        /// <returns></returns>
        public static List<GeoPoint> GetTrackPoints(List<GeoPoint> refPoints, DateTime dateSUTC, GeoPoint startLocation, double objectSpeed, int nextRefPointIndex = 0,
            int hourStart = 1, int hourFinish = 120, int hourStep = 1)
        {
            List<GeoPoint> resultPoints = new List<GeoPoint>();

            //DateTime curDate = dateSUTC;
            GeoPoint currentLocation = new GeoPoint(startLocation.LatGrd, startLocation.LonGrd);
            resultPoints.Add(currentLocation); //начальная точка с заблаговременностью 0
                                               //рассчёт точек
            Geodesic geod = Geodesic.WGS84;
            

            for (int hour = hourStart; hour < hourFinish; hour += hourStep)
            {
                double distanceInHourStep = objectSpeed * 3600 * hourStep;
                var refPoint = refPoints[nextRefPointIndex];
                //проверяем достигание опорной точки, в случае достигания изменяем следующую опорную точку
                while (GetDistanceTo(currentLocation, refPoint) < distanceInHourStep)
                {
                    if (nextRefPointIndex == refPoints.Count() - 1)//если достигли последней точки
                    {
                        GeoPoint destination = refPoint;
                        //destination.Date = currentLocation.Date.AddHours(currentLocation.GetDistanceTo(refPoint) / distanceInHourStep);
                        resultPoints.Add(destination);
                        return resultPoints;
                    }
                    distanceInHourStep -= GetDistanceTo(currentLocation, refPoint);
                    currentLocation = new GeoPoint(refPoint.LatGrd, refPoint.LonGrd);

                    nextRefPointIndex++;
                    refPoint = refPoints[nextRefPointIndex];

                }
                var dir = geod.Inverse(currentLocation.LatGrd, currentLocation.LonGrd, refPoint.LatGrd, refPoint.LonGrd);

                GeodesicLine line = geod.Line(currentLocation.LatGrd, currentLocation.LonGrd, dir.azi1);
                GeodesicData g = line.Position(distanceInHourStep, GeodesicMask.LATITUDE | GeodesicMask.LONGITUDE);

                GeoPoint coord = new GeoPoint(g.lat2, g.lon2);// , Date = currentLocation.Date.AddHours(1) };
                resultPoints.Add(coord);

                currentLocation = coord;
            }
            return resultPoints;
        }
        private static double GetDistanceTo(GeoPoint source, GeoPoint destination)
        {
            return (new GeoCoordinate(source.LatGrd, source.LonGrd)).GetDistanceTo(new GeoCoordinate(destination.LatGrd, destination.LonGrd));
        }
    }
}
