using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
       {
        private string id;
        public RouteRequestWatcher(string ID, Routes pub)
        {
            id = ID;
            // Subscribe to the event using C# 2.0 syntax
            pub.RouteRequestEvent += LogRouteRequest;
        }

        public RouteRequestWatcher()
        {
            
        }

        // Define what actions to take when the event is raised.
        public void LogRouteRequest(object sender, RouteRequestEventArgs e)
        {
            Console.WriteLine(id + " received this message: {0}", e.FromCity);
        }

        public object GetCityRequests(string p)
        {
            throw new NotImplementedException();
        }


       }
}
