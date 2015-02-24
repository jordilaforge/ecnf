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
                        ++Count;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return Count;
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
            

            return neighbours;
        }
    }
}
