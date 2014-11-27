using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DT=System.Data;
using EXCEL=Microsoft.Office.Interop.Excel;

namespace WpfApplication1.Code
{
    public class DataTableToExcel : ExportDataTable
    {
        public void ExportDataTable_file(string fileName, DT.DataTable dt)
        {
            EXCEL.Application objExcel = null;
            EXCEL.Workbook objWorkbook = null;
            EXCEL._Worksheet objsheet = null;
            try
            {
                objExcel = new Microsoft.Office.Interop.Excel.Application();
                objWorkbook = objExcel.Workbooks.Add(Missing.Value);
                objsheet = (EXCEL.Worksheet)objWorkbook.ActiveSheet;
                objExcel.Visible = false;
                objExcel.DisplayAlerts = false;

                objsheet.Cells.NumberFormat = "@";
                int displayColumnsCount = 1;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    objExcel.Cells[1, displayColumnsCount] = dt.Columns[i].ColumnName.Trim();
                    displayColumnsCount++;
                }
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    displayColumnsCount = 1;
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        objExcel.Cells[row + 2, displayColumnsCount] = dt.Rows[row][col].ToString().Trim();
                        displayColumnsCount++;
                    }
                    
                }
                objWorkbook.SaveAs(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, EXCEL.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch
            {
                MessageBox.Show("保存未成功！", "温馨提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            { 
            if(objWorkbook!=null)
                objWorkbook.Close(Missing.Value, Missing.Value, Missing.Value);
            if(objExcel.Workbooks!=null)
                objExcel.Workbooks.Close();
            if(objExcel!=null)
                objExcel.Quit();
            }
        }
        public void KillProcess(string ProcessName)
        {
        System.Diagnostics.Process myproc=new System.Diagnostics.Process();
          try
         {
            foreach(System.Diagnostics.Process thisproc  in  System.Diagnostics.Process.GetProcessesByName(ProcessName))
           {
           if(!thisproc.CloseMainWindow())
            {
            thisproc.Kill();
            } 
          }
        }
    catch(Exception ex)
    {
     throw new Exception("",ex);
     }
     }
    }
}
