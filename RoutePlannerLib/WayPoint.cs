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
        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name;
            Latitude = _latitude;
            Longitude = _longitude;
        }

        public override String ToString()
        {
            if (this.Name != null || !(this.Name.Equals("")))
            {
                return "Waypoint: " + this.Name + " " + Math.Round(this.Latitude, 2) + "/" + Math.Round(this.Longitude, 2);
            }
            else
            {
                return "Waypoint: " + Math.Round(this.Latitude, 2) + "/" + Math.Round(this.Longitude, 2);
            }
        }

        public double Distance(WayPoint target)
        {
            int radius = 6371;
            return radius * Math.Acos(Math.Sin(this.Latitude)*Math.Sin(target.Latitude)+Math.Cos(this.Latitude)*Math.Cos(target.Latitude)*Math.Cos(this.Longitude-target.Longitude));
        }
    }
}
