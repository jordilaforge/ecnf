using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    static class TextReaderExtension
    {
        public static IEnumerable<string[]> GetSplittedLines(this TextReader reader,char ch)
        {
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line.Split(ch);
            }

        }
    }
}
