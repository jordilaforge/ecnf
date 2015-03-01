using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs : EventArgs
    {
        public String FromCity { get; set; }
        public String ToCity { get; set; }
        public TransportModes Mode { get; set; }

        public RouteRequestEventArgs(String fromCity, String toCity, TransportModes mode)
        {
            this.FromCity = fromCity;
            this.ToCity = toCity;
            this.Mode = mode;
        }



        
    }
}
