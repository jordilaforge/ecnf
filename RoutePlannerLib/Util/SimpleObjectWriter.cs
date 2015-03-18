using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter
    {
        private StringWriter stream;

        public SimpleObjectWriter(StringWriter stream)
        {
            this.stream = stream;
        }



        public void Next(City c)
        {

            stream.WriteLine("Instance of "+c.GetType().FullName);
            stream.WriteLine("Name=" +"\"" +c.Name+"\"");
            stream.WriteLine("Country=" + "\"" + c.Country + "\"");
            stream.WriteLine("Population=" + c.Population);
            stream.WriteLine("Location is a nested object...");
            stream.WriteLine("Instance of " + c.Location.GetType().FullName);
            stream.WriteLine("Name=" + "\"" + c.Location.Name + "\"");
            stream.WriteLine("Longitude=" + c.Location.Longitude);
            stream.WriteLine("Latitude=" + c.Location.Latitude);
            stream.WriteLine("End of instance");
            stream.WriteLine("End of instance");
            Console.WriteLine(stream);
        }
    }
}
