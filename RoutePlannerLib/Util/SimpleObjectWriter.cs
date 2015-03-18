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



        public void Next(object o)
        {

            stream.WriteLine("Instance of "+o.GetType().FullName);

            foreach (var prop in o.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    stream.WriteLine(prop.Name+"=\""+prop.GetValue(o,null)+"\"");
                }
                else if (prop.PropertyType == typeof(int))
                {
                    stream.WriteLine(prop.Name +"="+ prop.GetValue(o,null));
                }
                else if (prop.PropertyType == typeof(double))
                {
                    stream.WriteLine(prop.Name + "=" + prop.GetValue(o,null));
                }
                else
                {
                    stream.WriteLine("Location is a nested object...");
                    Next(prop.GetValue(o, null));
                }
            }
            stream.WriteLine("End of instance");
            Console.WriteLine(stream);
        }
    }
}
