using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class City
    {
        public String Name { get; set; }
        public String Country { get; set; }
        public int Population { get; set; }
        public WayPoint Location { get; set; }


        public City() { }

        public City(String name, String country, int population, double latitude, double longitude)
        {
            this.Name = name;
            this.Country = country;
            this.Population = population;
            this.Location = new WayPoint(name, latitude, longitude);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return (this.Name.Equals(((City)obj).Name)) &&
                    this.Country.Equals(((City)obj).Country);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
