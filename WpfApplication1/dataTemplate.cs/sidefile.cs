using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApplication1
{  
    public class sidefiles : INotifyPropertyChanged
        {
            public string ID;
            private string _file_name;
            public string file_name
            {
                get
                {
                    return _file_name;
                }
                set
                {
                    if (_file_name != value)
                    {
                        _file_name = value;
                        OnPropertyChanged("file_name");
                    }
                }
            }
          
            public ImageSource image { get; set; }
           // public string tool_name { get; set; }
            public object obj;
            public ObservableCollection<sidefiles> items { get; set; }
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

        }
            #endregion
    
  public  class sidefile
    {

    }
}
