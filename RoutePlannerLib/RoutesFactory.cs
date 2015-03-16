using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            string s = ConfigurationManager.AppSettings.Get("countoffiles");
            string configvalue1 = ConfigurationManager.AppSettings["a"];
            string configvalue2 = ConfigurationManager.AppSettings["b"];
            if (configvalue1 == null)
            {
                Console.WriteLine("is null asdasd");
            }
            Console.WriteLine("GefundenerRouteAlgo: "+configvalue1);
            return null;
        }
        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            return null;
        }
    }
}