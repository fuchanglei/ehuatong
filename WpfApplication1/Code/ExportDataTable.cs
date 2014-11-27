using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WpfApplication1.Code
{
  public interface ExportDataTable
    {
      void ExportDataTable_file(string fileName, DataTable dt);
    }
}
