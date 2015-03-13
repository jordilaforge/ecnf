using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public WayPoint(string name, double latitude, double longitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public override String ToString()
        {
            if (this.Name == null || this.Name.Equals(""))
            {
                return "WayPoint: " + String.Format("{0:0.00}", this.Latitude) + "/" + String.Format("{0:0.00}", this.Longitude);
                
            }
            else
            {
                return "WayPoint: " + this.Name + " " + String.Format("{0:0.00}", this.Latitude) + "/" + String.Format("{0:0.00}", this.Longitude);
            }
        }

        public double Distance(WayPoint target)
        {
            int Radius = 6371;
            double LatitudeThis = ConvertToRadians(this.Latitude);
            double LongitudeThis = ConvertToRadians(this.Longitude);
            double LatitudeTarget = ConvertToRadians(target.Latitude);
            double LongitudeTarget = ConvertToRadians(target.Longitude);
            return Radius * Math.Acos(Math.Sin(LatitudeThis) * Math.Sin(LatitudeTarget) + Math.Cos(LatitudeThis) * Math.Cos(LatitudeTarget) * Math.Cos(LongitudeThis - LongitudeTarget));
        }

        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static WayPoint operator+(WayPoint p1, WayPoint p2){
            return new WayPoint(p1.Name, p1.Latitude + p2.Latitude, p1.Longitude + p2.Longitude);
        }

        public static WayPoint operator -(WayPoint p1, WayPoint p2)
        {
            return new WayPoint(p1.Name, p1.Latitude - p2.Latitude, p1.Longitude - p2.Longitude);
        }
    }
}
