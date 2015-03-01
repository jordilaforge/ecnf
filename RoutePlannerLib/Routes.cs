
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public class Routes
    {
        List<Link> routes = new List<Link>();
        Cities cities;

        //Delegate For EventRouteRequest
        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);

        public event RouteRequestHandler RouteRequestEvent;

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
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var linkAsString = line.Split('\t');

                    City city1 = cities.FindCity(linkAsString[0]);
                    City city2 = cities.FindCity(linkAsString[1]);

                    // only add links, where the cities are found 
                    if ((city1 != null) && (city2 != null))
                    {
                        routes.Add(new Link(city1, city2, city1.Location.Distance(city2.Location),
                                                   TransportModes.Rail));
                    }
                }
            }

            return Count;

        }

        public List<Link> FindShortestRouteBetween(string fromCity, string toCity,
                                        TransportModes mode)
        {
            OnRaiseRouteRequestEvent(new RouteRequestEventArgs(fromCity,toCity,mode));
            return null;
            //TODO
        }

        private void OnRaiseRouteRequestEvent(RouteRequestEventArgs routeRequestEventArgs)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            RouteRequestHandler handler = RouteRequestEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                //routeRequestEventArgs.FromCity += String.Format("bla");
                //routeRequestEventArgs.ToCity += String.Format("Zürich");
                // Use the () operator to raise the event.
                handler(this, routeRequestEventArgs);
            }
        }

    }
}