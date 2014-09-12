using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApplication1
{
     class dataTem
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _rowname;
        public string rowname
        {
            get { return _rowname; }
            set { _rowname = value; }
        }
    }
}
