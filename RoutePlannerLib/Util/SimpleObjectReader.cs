using System;
using System.Collections.Generic;
using System.Linq;
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
                    string[] splits = line.Split(null);
                    string[] splits2 = splits[2].Split('.');
                    string className = splits2[splits2.Length - 1];
                    string namespaceName = null;
                    for (int i = 0; i < splits2.Length - 1; ++i)
                    {
                        namespaceName += splits2[i];
                        if (i != (splits2.Length - 2))
                        {
                            namespaceName += ".";
                        }
                    }
                    Console.WriteLine(className);
                    Console.WriteLine(namespaceName);
                    o = Activator.CreateInstance(namespaceName, className);
                }
                else if (line.Contains("="))
                {
                    
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
