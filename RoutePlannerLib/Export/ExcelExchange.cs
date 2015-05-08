using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export
{
    public class ExcelExchange
    {
        public void WriteToFile(String fileName, City from, City to, List<Link> links)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("Excel is not properly installed!!");
                return;
            }


            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            
            //Titel
            xlWorkBook = xlApp.Workbooks.Add();
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "From";
            xlWorkSheet.Cells[1, 2] = "To";
            xlWorkSheet.Cells[1, 3] = "Distance";
            xlWorkSheet.Cells[1, 4] = "Transport Mode";
            
            //Data
            for (int i = 0; i < links.Count; i++)
            {
                xlWorkSheet.Cells[i + 2, 1] = links[i].FromCity.Name.ToString();
                xlWorkSheet.Cells[i + 2, 2] = links[i].ToCity.Name.ToString();
                xlWorkSheet.Cells[i + 2, 3] = links[i].Distance.ToString();
                xlWorkSheet.Cells[i + 2, 4] = links[i].TransportMode.ToString();
            }

            Microsoft.Office.Interop.Excel.Range formatRange;
            formatRange = xlWorkSheet.get_Range("a1","d1");
            formatRange.EntireRow.Font.Bold = true;
            //Get the borders collection.
            var borders = formatRange.Borders;
            //Set the thin lines style.
            borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders.Weight = 2d;
            //font size
            formatRange.Font.Size = 14;
            //auto fit cells
            formatRange.EntireColumn.AutoFit();
            
            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            xlApp.DisplayAlerts = true;
            xlWorkBook.Close(true);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
