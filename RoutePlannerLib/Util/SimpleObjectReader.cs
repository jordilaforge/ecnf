using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectReader
    {
        private System.IO.StringReader stream;

        public SimpleObjectReader(System.IO.StringReader stream)
        {
            this.stream = stream;
        }

        public object Next()
        {
            object o =null;
            String line;
            while ((line = stream.ReadLine()) != null)
            {
                if (line.Contains("Instance of"))
                {
                    line = line.Remove(0, "Instance of ".Length);
                    Assembly ass = Assembly.Load("RoutePlannerLib");
                    o = ass.CreateInstance(line);
                }
                else if (line.Contains("=\""))
                {
                    Regex regex = new Regex("\"(.*)\"");
                    var v = regex.Match(line);
                    String cName = (v.Groups[1].ToString());
                    var t = o.GetType();
                    var prop = t.GetProperty("Name");
                    prop.SetValue(o, cName);
                }
                else if (line.Contains("=")&&!(line.Contains("\""))&&!(line.Contains(".")))
                {
                    int integer = Convert.ToInt32(line.Substring(line.LastIndexOf('=') + 1));
                    var t = o.GetType();
                    var prop = t.GetProperty("Population");
                    prop.SetValue(o, integer);
                }
            }
            
            //stream.ReadLine();
            //Regex regex = new Regex("\"(.*)\"");
            //var v = regex.Match(stream.ReadLine());
            //String cName = (v.Groups[1].ToString());
            //Console.WriteLine(cName);
            //var v2 = regex.Match(stream.ReadLine());
            //String cCountry = (v2.Groups[1].ToString());
            //Console.WriteLine(cCountry);
            //var v3 = stream.ReadLine();
            //int population = Convert.ToInt32(v3.Substring(v3.LastIndexOf('=') + 1));
            //Console.WriteLine(population);
            //stream.ReadLine();
            //stream.ReadLine();
            //stream.ReadLine();
            //var v4 = stream.ReadLine();
            //double longitude = Convert.ToDouble(v4.Substring(v4.LastIndexOf('=') + 1));
            //Console.WriteLine(longitude);
            //var v5 = stream.ReadLine();
            //double latitude = Convert.ToDouble(v5.Substring(v5.LastIndexOf('=') + 1));
            //Console.WriteLine(latitude);
            //stream.ReadLine();
            //stream.ReadLine();
            //City c = new City(cName, cCountry, population, latitude, longitude);
            //return c;
            return o;
        }
    }
}
