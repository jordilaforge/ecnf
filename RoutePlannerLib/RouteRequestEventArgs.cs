using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs : EventArgs
    {
        public String FromCity { get; private set; }
        public String ToCity { get; private set; }
        public TransportModes Mode { get; private set; }

        public RouteRequestEventArgs(String fromCity, String toCity, TransportModes mode)
        {
            this.FromCity = fromCity;
            this.ToCity = toCity;
            this.Mode = mode;
        }



        
    }
}
