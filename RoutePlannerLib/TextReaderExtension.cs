using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    class TextReaderExtension
    {
        public static IEnumerable<string> GetSplitteLines(this string text, char ch)
        {
            var parts = text.Split(new char[] { ch });
            var result = new StringBuilder();
            foreach (var part in parts)
                yield return part;
        }
    }
}
