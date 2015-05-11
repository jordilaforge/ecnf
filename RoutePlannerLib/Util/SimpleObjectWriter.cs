using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

            stream.WriteLine("Instance of " + o.GetType().FullName, CultureInfo.InvariantCulture);

            foreach (var prop in o.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(false).Any(a => a is XmlIgnoreAttribute))
                {
                    break;
                }
                if (prop.PropertyType == typeof(string))
                {
                    stream.WriteLine(prop.Name + "=\"" + prop.GetValue(o, null) + "\"", CultureInfo.InvariantCulture);
                }
                else if (prop.PropertyType == typeof(int))
                {
                    stream.WriteLine(prop.Name + "=" + prop.GetValue(o, null), CultureInfo.InvariantCulture);
                }
                else if (prop.PropertyType == typeof(double))
                {
                    stream.WriteLine(prop.Name + "=" + prop.GetValue(o, null), CultureInfo.InvariantCulture);
                }
                else
                {
                    stream.WriteLine("Location is a nested object...", CultureInfo.InvariantCulture);
                    Next(prop.GetValue(o, null));
                }
            }
            stream.WriteLine("End of instance", CultureInfo.InvariantCulture);
            Console.WriteLine(stream);
        }
    }
}
