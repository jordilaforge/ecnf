using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutePlanner (Version {0})", Assembly.GetExecutingAssembly().GetName().Version);
            var bern = new WayPoint("Bern",46.951081, 7.438637);
            var tripolis = new WayPoint("Tripolis",32.883333333333, 13.166666666667);
            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            var City = new City("Bern", "Schweiz", 75000, 47.479319847061966, 8.212966918945312);
            Console.WriteLine(wayPoint);
            Console.WriteLine("Distance Bern to Tripolis: "+bern.Distance(tripolis)+"km");
            Cities cities = new Cities();
            cities.ReadCities(@"..\..\..\RoutePlannerTest\data\citiesTestDataLab2.txt");
            RoutesDijkstra routes = new RoutesDijkstra(cities);
            routes.ReadRoutes(@"..\..\..\RoutePlannerTest\data\linksTestDataLab3.txt");
            Cities citiesFail = new Cities();
            citiesFail.ReadCities(@"irgendeinfile.txt");
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
