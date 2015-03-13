using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
       {
        Dictionary<string, int> CityRequests;
        public RouteRequestWatcher()
        {
            CityRequests = new Dictionary<string,int>();
        }


        // Define what actions to take when the event is raised.
        public void LogRouteRequests(object sender, RouteRequestEventArgs e)
        {
            if (!(CityRequests.ContainsKey(e.ToCity)))
            {
                CityRequests[e.ToCity] = 1;
            }
            else
            {
                int number;
                CityRequests.TryGetValue(e.ToCity, out number);
                number++;
                CityRequests[e.ToCity] = number;
            }
        }

        public int GetCityRequests(string city)
        {
            int number;
            CityRequests.TryGetValue(city, out number);
            return number;
        }


       }
}
