using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Console.WriteLine("Starting parallel test......");
            Cities citiesP = new Cities();
            cities.ReadCities(@"..\..\..\RoutePlannerTest\data\citiesTestDataLab10.txt");


            int warmUpRounds = 100;
            int measureRounds = 20;
            Routes routesP = new RoutesFloydWarshall(cities);
            int count = routesP.ReadRoutes(@"..\..\..\RoutePlannerTest\data\linksTestDataLab10.txt");

            Console.WriteLine("doing warmup");
            for (int i = 0; i < warmUpRounds; i++)
            {
                List<Link> links = routesP.FindShortestRouteBetween("Lyon", "Berlin", TransportModes.Rail);
            }
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("sequential mode: ");
            // test short routes in parallel mode
            routesP.ExecuteParallel = false;
            for (int i = 0; i < measureRounds; i++)
            {
                List<Link> links = routesP.FindShortestRouteBetween("Lyon", "Berlin", TransportModes.Rail);
            }
            watch.Stop();
            Console.WriteLine("Elapsed Time: " + watch.ElapsedMilliseconds + "ms");

            watch.Reset();
            watch.Start();
            Console.WriteLine("parallel mode: ");
            // test short routes in parallel mode
            routesP.ExecuteParallel = true;
            for (int i = 0; i < measureRounds; i++)
            {
                List<Link> links2 = routesP.FindShortestRouteBetween("Lyon", "Berlin", TransportModes.Rail);

            }
            watch.Stop();
            Console.WriteLine("Elapsed Time: " + watch.ElapsedMilliseconds + "ms");
            
            
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
