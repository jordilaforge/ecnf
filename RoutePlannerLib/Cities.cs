using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using System.Globalization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        List<City> cities;
        public int Count;

        public Cities()
        {
            cities = new List<City>();
            Count = 0;
        }


        public int ReadCities(string filename)
        {
            cities = new List<City>();
            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                    foreach(string[] cs in citiesAsStrings)
                    {
                        cities.Add(new City(cs[0].Trim(),
                            cs[1].Trim(), int.Parse(cs[2]),
                            double.Parse(cs[3], CultureInfo.InvariantCulture), double.Parse(cs[4], CultureInfo.InvariantCulture)));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            Count += cities.Count;
            return cities.Count;
        }

        public City this[int index] //indexer implementation
        {
            get 
            {
                if (index > Count)
                {
                    return null;
                }
                return this.cities[index]; 
            }
            set { this.cities[index] = value; }
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            List<City> neighbours = new List<City>();
            for (int i = 0; i < cities.Count; ++i)
            {
                double distanceCities = location.Distance(cities[i].Location);
                if(distanceCities<=distance){
                    neighbours.Add(cities[i]);
                }
            }
            return SortByDistance(neighbours,location);
        }


        private List<City> SortByDistance(List<City> neighbours,WayPoint location)
        {
            List<City> neighboursSorted = neighbours;
            neighboursSorted.Sort(delegate(City a, City b)
            {
                return (a.Location.Distance(location).CompareTo(b.Location.Distance(location)));

            }
            );
            return neighboursSorted;
        }

        public City FindCity(string cityName)
        {
            var city = cities.Find(delegate(City c)
            {
                return String.Compare(c.Name, cityName, true) == 0;
            });
            return city;
        }

        /// <summary>
        /// Find all cities between 2 cities 
        /// </summary>
        /// <param name="from">source city</param>
        /// <param name="to">target city</param>
        /// <returns>list of cities</returns>
        public List<City> FindCitiesBetween(City from, City to)
        {
            var foundCities = new List<City>();
            if (from == null || to == null)
                return foundCities;

            foundCities.Add(from);

            var minLat = Math.Min(from.Location.Latitude, to.Location.Latitude);
            var maxLat = Math.Max(from.Location.Latitude, to.Location.Latitude);
            var minLon = Math.Min(from.Location.Longitude, to.Location.Longitude);
            var maxLon = Math.Max(from.Location.Longitude, to.Location.Longitude);

            // rename the name of the "cities" variable to your name of the internal City-List
            foundCities.AddRange(cities.FindAll(c =>
                c.Location.Latitude > minLat && c.Location.Latitude < maxLat
                        && c.Location.Longitude > minLon && c.Location.Longitude < maxLon));

            foundCities.Add(to);
            return foundCities;
        }

    }
}
