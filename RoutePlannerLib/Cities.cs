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
        public int Count { get { return cities.Count; } }

        public Cities()
        {
            cities = new List<City>();
        }


        public int ReadCities(string filename)
        {
            int countOld = cities.Count;
            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    var citiesAsStrings = reader.GetSplittedLines('\t').ToList();
                    cities.AddRange(citiesAsStrings.Select(c => new City(c[0].Trim(),c[1].Trim(),int.Parse(c[2]),double.Parse(c[3], CultureInfo.InvariantCulture), double.Parse(c[4], CultureInfo.InvariantCulture))));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cities.Count-countOld;
        }

        public City this[int index] //indexer implementation
        {
            get { return (index < cities.Count) ? cities[index] : null; }
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            return (cities.Where(c => location.Distance(c.Location) < distance).OrderBy(d => location.Distance(d.Location)).ToList());
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
            return cities.Find(c => c.Name.Equals(cityName, StringComparison.InvariantCultureIgnoreCase));
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
