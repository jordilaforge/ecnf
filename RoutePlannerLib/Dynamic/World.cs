using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Dynamic
{
    public class World : DynamicObject
    {
        private Cities cities;

        public World(Cities cities)
        {
            this.cities = cities;
        }

        public override bool TryInvokeMember(
        InvokeMemberBinder binder, object[] args,
        out object result)
        {
            Console.WriteLine(binder.Name + " method was called");
            var city = this.cities.FindCity(binder.Name);
            if (city == null)
            {
                result = "The city \""+binder.Name+"\" does not exist!";
            }
            else
            {
                result = city;
            }
            return true;
        }
    }
}
