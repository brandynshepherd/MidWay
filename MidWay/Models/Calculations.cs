using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MidWay.Models
{
    public class Calculations
    {
        public Location Midpoint(Location latlong1, Location latlong2)
        {
            double arcLength = haversineDistance(latlong1, latlong2);
            double brng = calculateBearing(latlong1, latlong2);

            return calculateCoord(latlong1, brng, arcLength / 2);
        }

        public int MilesToMeters(int miles)
        {
            return miles * 1609;
        }

        double earthRadius = 6367;
        private double haversineDistance(Location latlong1, Location latlong2)
        {
            var lat1 = DegtoRad(latlong1.lat);
            var lon1 = DegtoRad(latlong1.lng);
            var lat2 = DegtoRad(latlong2.lat);
            var lon2 = DegtoRad(latlong2.lng);
            var dLat = lat2 - lat1;
            var dLon = lon2 - lon1;
            var cordLength = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dLon / 2), 2);
            var centralAngle = 2 * Math.Atan2(Math.Sqrt(cordLength), Math.Sqrt(1 - cordLength));
            return earthRadius * centralAngle;
        }


        private double DegtoRad(double x)
        {
            return x * Math.PI / 180;
        }

        private double RadtoDeg(double x)
        {
            return x * 180 / Math.PI;
        }


        private double calculateBearing(Location latlong1, Location latlong2)
        {
            var lat1 = DegtoRad(latlong1.lat);
            var lon1 = latlong1.lng;
            var lat2 = DegtoRad(latlong2.lat);
            var lon2 = latlong2.lng;
            var dLon = DegtoRad(lon2 - lon1);
            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            var brng = (RadtoDeg(Math.Atan2(y, x)) + 360) % 360;
            return brng;
        }

        private Location calculateCoord(Location origin, double brng, double arcLength)
        {
            var lat1 = DegtoRad(origin.lat);
            var lon1 = DegtoRad(origin.lng);
            var centralAngle = arcLength / earthRadius;
            var lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(centralAngle) + Math.Cos(lat1) * Math.Sin(centralAngle) * Math.Cos(DegtoRad(brng)));

            var lon2 = lon1 + Math.Atan2(Math.Sin(DegtoRad(brng)) * Math.Sin(centralAngle) * Math.Cos(lat1), Math.Cos(centralAngle) - Math.Sin(lat1) * Math.Sin(lat2));

            return new Location { lat = RadtoDeg(lat2), lng = RadtoDeg(lon2)};
        }
    }
}