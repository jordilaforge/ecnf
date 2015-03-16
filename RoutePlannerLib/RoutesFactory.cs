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

            return Create(cities,Properties.Settings1.Default.RouteAlgorithm);
        }

        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            Type t=null;
            foreach (var at in a.GetTypes())
            {
                if (at.FullName.Equals(algorithmClassName))
                {
                    t = at;
                }
            }
            if (t == null || !(t.IsClass))
            {
                return null;
            }
            else
            {
                return Activator.CreateInstance(t, cities) as IRoutes;
            }
            
        }
    }
}