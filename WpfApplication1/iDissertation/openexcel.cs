using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;

namespace WpfApplication1
{
   class openexcel
    {
       private string _excel_filename;
       public string _sheetname;
       private OleDbConnection objConn;
       public openexcel(string excel_filename)
       {
           _excel_filename = excel_filename;
           objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + _excel_filename + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=NO;IMEX=1\"");
       }
       public List<string> getGetOleDbSchemaTable()
       {
           List<string> li = new List<string>();
           //objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + _excel_filename + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1\"");
           objConn.Open();
          System.Data.DataTable _Table = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
          for (int i = 0; i != _Table.Rows.Count; i++)
          {
              li.Add(_Table.Rows[i]["Table_Name"].ToString());
          }
           objConn.Close();
           return li;
       }
       public System.Data.DataTable getexcel(string sheename)
        {
            System.Data.DataTable mytable = new System.Data.DataTable();
            //openFileDialog1.Title = "选择文件";
           // openFileDialog1.Filter = "Excel文件|*.xls|txt文件|*.txt";
           // openFileDialog1.ShowDialog();
           //OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + _excel_filename + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=NO\"");
            objConn.Open();
            string sql = "select * from [" + sheename + "]";
            DataSet ds = new DataSet();
            // OleDbConnection conn = new OleDbConnection(objConn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, objConn);
            adapter.Fill(ds, "table1");
            mytable = ds.Tables["table1"];
            objConn.Close();
            return mytable;
        }
    }
}
