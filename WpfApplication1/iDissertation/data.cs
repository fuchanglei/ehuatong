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
    class dataTem : INotifyPropertyChanged
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
        private bool _ischeck;
        public bool ischeck
        {
            get { return _ischeck; }
            set {
                if (_ischeck != value)
                {
                    _ischeck = value;
                    OnPropertyChanged("ischeck");
                }
            }
        }
        #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string propertyName)
            {
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        
            #endregion
    }
}
