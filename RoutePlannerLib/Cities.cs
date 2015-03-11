using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int counter = 0;
            cities = new List<City>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (sr.Peek() >= 0)
                    {
                        String line = sr.ReadLine();
                        string[] splits = line.Split('\t');
                        cities.Add(new City(splits[0], splits[1], Convert.ToInt32(splits[2]), Convert.ToDouble(splits[3]), Convert.ToDouble(splits[4])));
                        ++counter;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Count += counter;
            return counter;
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


        //Implemented own InsertionSort algorithm for Sorting List
        public List<City> SortByDistance(List<City> neighbours,WayPoint location)
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
                if (String.Compare(c.Name,cityName,true)==0)
                    return true;
                else
                    return false;
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
