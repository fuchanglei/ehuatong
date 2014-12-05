using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace WpfApplication1.Code
{
    class DataTableToTxt:ExportDataTable
    {  
        public void ExportDataTable_file(string fileName,DataTable dt)
        {
            string col = string.Empty;
            int Colcount = dt.Columns.Count;
            StreamWriter sw = new StreamWriter(fileName,false,Encoding.Default);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                col = col+" "+dt.Columns[i].ColumnName;
            }
            sw.WriteLine(col.Substring(1));
            foreach (DataRow dr in dt.Rows)
            {
                col = string.Empty;
                for (int c = 0; c < Colcount; c++)
                {
                    col = col + " " + dr[c].ToString();
                }
                sw.WriteLine(col.Substring(1));
            }
            sw.Flush();
            sw.Close();
        }
    }
}
