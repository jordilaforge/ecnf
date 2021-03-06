﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesDijkstra : Routes
    {


        
        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);
       
        public event RouteRequestHandler RouteRequestEvent;

        public City[] FindCities(TransportModes transportMode)
        {
            return routes.Where(r => r.TransportMode == transportMode)
                .Select(c => c.FromCity).Distinct()
                .Union(routes.Where(r => r.TransportMode == transportMode).Select(c => c.ToCity).Distinct())
                .ToArray();
        }


        public RoutesDijkstra(Cities cities)
            : base(cities)
        {

        }

        public void OnRaiseRouteRequestEvent(RouteRequestEventArgs routeRequestEventArgs)
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

        public override List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportModes mode)
        {
            OnRaiseRouteRequestEvent(new RouteRequestEventArgs(fromCity, toCity, mode));
            City fromCityObi = cities.FindCity(fromCity);
            City toCityObi = cities.FindCity(toCity);
            var citiesBetween = cities.FindCitiesBetween(fromCityObi, toCityObi);
            if (citiesBetween == null || citiesBetween.Count < 1 || routes == null || routes.Count < 1)
                return null;

            var source = citiesBetween[0];
            var target = citiesBetween[citiesBetween.Count - 1];

            Dictionary<City, double> dist;
            Dictionary<City, City> previous;
            var q = FillListOfNodes(citiesBetween, out dist, out previous);
            dist[source] = 0.0;

            // the actual algorithm
            previous = SearchShortestPath(mode, q, dist, previous);

            // create a list with all cities on the route
            var citiesOnRoute = GetCitiesOnRoute(source, target, previous);

            // prepare final list if links
            return FindPath(citiesOnRoute, mode);
        }

        private List<Link> FindPath(List<City> citiesOnRoute, TransportModes mode)
        {
            List<Link> links = new List<Link>();

            for (int i = 0; i < citiesOnRoute.Count - 1; ++i)
            {
                City city1 = citiesOnRoute[i];
                City city2 = citiesOnRoute[i + 1];
                links.Add(FindLink(city1, city2, mode));
            }
            return links;
        }

        private static List<City> FillListOfNodes(List<City> cities, out Dictionary<City, double> dist, out Dictionary<City, City> previous)
        {
            var q = new List<City>(); // the set of all nodes (cities) in Graph ;
            dist = new Dictionary<City, double>();
            previous = new Dictionary<City, City>();
            foreach (var v in cities)
            {
                dist[v] = double.MaxValue;
                previous[v] = null;
                q.Add(v);
            }

            return q;
        }

        /// <summary>
        /// Searches the shortest path for cities and the given links
        /// </summary>
        /// <param name="mode">transportation mode</param>
        /// <param name="q"></param>
        /// <param name="dist"></param>
        /// <param name="previous"></param>
        /// <returns></returns>
        private Dictionary<City, City> SearchShortestPath(TransportModes mode, List<City> q, Dictionary<City, double> dist, Dictionary<City, City> previous)
        {
            while (q.Count > 0)
            {
                City u = null;
                var minDist = double.MaxValue;
                // find city u with smallest dist
                foreach (var c in q)
                    if (dist[c] < minDist)
                    {
                        u = c;
                        minDist = dist[c];
                    }

                if (u != null)
                {
                    q.Remove(u);
                    foreach (var n in FindNeighbours(u, mode))
                    {
                        var l = FindLink(u, n, mode);
                        var d = dist[u];
                        if (l != null)
                            d += l.Distance;
                        else
                            d += double.MaxValue;

                        if (dist.ContainsKey(n) && d < dist[n])
                        {
                            dist[n] = d;
                            previous[n] = u;
                        }
                    }
                }
                else
                    break;

            }

            return previous;
        }

        /// <summary>
        /// Finds the link between two cities.
        /// </summary>
        /// <param name="c1">first city</param>
        /// <param name="c2">second city</param>
        /// <param name="t"></param>
        /// <returns>found link or null</returns>
        protected Link FindLink(City c1, City c2, TransportModes t)
        {
            foreach (Link l in routes)
            {
                if (t.Equals(l.TransportMode) &&
                ((c1.Equals(l.FromCity) && c2.Equals(l.ToCity))
                || (c2.Equals(l.FromCity) && c1.Equals(l.ToCity))))
                {
                    return new Link(c1, c2, l.Distance, TransportModes.Rail);
                }
            }
            return null;
        }


        /// <summary>
        /// Finds all neighbor cities of a city. 
        /// </summary>
        /// <param name="city">source city</param>
        /// <param name="mode">transportation mode</param>
        /// <returns>list of neighbor cities</returns>
        private List<City> FindNeighbours(City city, TransportModes mode)
        {
            var neighbors = new List<City>();
            foreach (var r in routes)
                if (mode.Equals(r.TransportMode))
                {
                    if (city.Equals(r.FromCity))
                        neighbors.Add(r.ToCity);
                    else if (city.Equals(r.ToCity))
                        neighbors.Add(r.FromCity);
                }

            return neighbors;
        }

        private List<City> GetCitiesOnRoute(City source, City target, Dictionary<City, City> previous)
        {
            var citiesOnRoute = new List<City>();
            var cr = target;
            while (previous[cr] != null)
            {
                citiesOnRoute.Add(cr);
                cr = previous[cr];
            }
            citiesOnRoute.Add(source);

            citiesOnRoute.Reverse();
            return citiesOnRoute;
        }


        public async Task<List<Link>> FindShortestRouteBetweenAsync(string fromCity, string toCity, TransportModes mode, IProgress<string> progress)
        {
            if (progress != null) progress.Report("Started FindShortesRouteBetweenAsync done");
            OnRaiseRouteRequestEvent(new RouteRequestEventArgs(fromCity, toCity, mode));
            City fromCityObi = cities.FindCity(fromCity);
            City toCityObi = cities.FindCity(toCity);
            var citiesBetween = cities.FindCitiesBetween(fromCityObi, toCityObi);
            if (citiesBetween == null || citiesBetween.Count < 1 || routes == null || routes.Count < 1)
                return null;
            if (progress != null) progress.Report("FindCitiesBetween done");

            var source = citiesBetween[0];
            var target = citiesBetween[citiesBetween.Count - 1];

            Dictionary<City, double> dist;
            Dictionary<City, City> previous;
            var q = FillListOfNodes(citiesBetween, out dist, out previous);
            dist[source] = 0.0;
            if (progress != null) progress.Report("FillListofNodes done");
            // the actual algorithm
            previous = SearchShortestPath(mode, q, dist, previous);
            if (progress != null) progress.Report("SearchShortestPath done");
            // create a list with all cities on the route
            var citiesOnRoute = GetCitiesOnRoute(source, target, previous);
            // creating new Task with Factory to wait for
            var task = Task.Factory.StartNew(() => FindPath(citiesOnRoute, mode));
            // prepare final list if links
            if (progress != null) progress.Report("FindPath done");
            return await task;
        }
        
        public Task<List<Link>> FindShortestRouteBetweenAsync(string fromCity, string toCity, TransportModes mode)
        {
            return FindShortestRouteBetweenAsync(fromCity, toCity, mode, null);
        }
    }
}
