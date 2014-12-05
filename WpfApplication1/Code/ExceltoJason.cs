using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WpfApplication1;

namespace WpfApplication1.Code
{
   public class ExceltoJason
    {
       private FileStream fi;
       private StreamWriter sw;
       private string _Filename;
       private string _excelPath;
       private openexcel open_excel;
       public List<string> Header_name=new List<string>();
       public string resultss;
       public ExceltoJason(string filename,string excelPath)
       {
           _Filename = filename;
           _excelPath = excelPath;
           fi = new FileStream(_Filename,FileMode.Create);
           sw = new StreamWriter(fi, Encoding.UTF8);
           resultss = string.Empty;
       }
       public void WriteJason()
       {
           resultss = "";
           open_excel = new openexcel(_excelPath);
           System.Data.DataTable mytable = open_excel.getexcel("Sheet1$");
           int Ccount = mytable.Columns.Count;
           int Rcount = mytable.Rows.Count;
           for (int c = 0; c < Ccount; c++)
           {
               Header_name.Add(mytable.Columns[c].ColumnName);
               resultss = resultss + "," + mytable.Columns[c].ColumnName;
           }
           resultss = resultss.Substring(1);
           mytable.Rows.Remove(mytable.Rows[0]);
           string result = JsonConvert.SerializeObject(mytable, new DataTableConverter());
 
           sw.Write(result);
           sw.Close();
           sw.Dispose();
           resultss ="#filename=" +Path.GetFileName(_Filename) +"&HeaderName="+resultss;
       }
    }
}
