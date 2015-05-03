
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System.Diagnostics;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public abstract class Routes : IRoutes
    {
        private static readonly TraceSource logger = new TraceSource("Routes");
        public List<Link> routes = new List<Link>();
        public Cities cities;
        public bool ExecuteParallel { set; get; }


        public int Count
        {
            get { return routes.Count; }
        }

        /// <summary>
        /// Initializes the Routes with the cities.
        /// </summary>
        /// <param name="cities"></param>
        public Routes(Cities cities)
        {
            this.cities = cities;
        }

        /// <summary>
        /// Reads a list of links from the given file.
        /// Reads only links where the cities exist.
        /// </summary>
        /// <param name="filename">name of links file</param>
        /// <returns>number of read route</returns>
        public int ReadRoutes(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {
                logger.TraceEvent(TraceEventType.Information, 1, "ReadRoutes started");
                IEnumerable<string[]> routesAsStrings = reader.GetSplittedLines('\t');

                foreach (string[] route in routesAsStrings)
                {
                    City city1 = cities.FindCity(route[0].Trim());
                    City city2 = cities.FindCity(route[1].Trim());
                    // only add links, where the cities are found 
                    if ((city1 != null) && (city2 != null))
                    {
                        routes.Add(new Link(city1, city2, city1.Location.Distance(city2.Location),
                                                   TransportModes.Rail));
                    }
                }

            } 
            logger.TraceEvent(TraceEventType.Information, 1, "ReadRoutes ended");
            return Count;

        }




        public abstract List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportModes mode);

        //Neu Protected
        /// <summary>
        /// Find all cities between two cities
        /// </summary>
        /// <param name="fromCity">Start city as string</param>
        /// <param name="toCity">End city as string</param>
        /// <returns></returns>
        protected List<City> FindCitiesBetween(string fromCity, string toCity)
        {
            var fc = cities.FindCity(fromCity);
            var tc = cities.FindCity(toCity);
            return cities.FindCitiesBetween(fc, tc);
        }


    }
}