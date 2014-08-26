using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WpfApplication1
{
   public class title:INotifyPropertyChanged
    {
        public string ID;
        private string Title_name;
        public string title_name {
            get
            {
                return Title_name;  
            }
            set
            {
                if (Title_name != value)
                {
                    Title_name = value;
                    OnPropertyChanged("title_name");
                }
            }
        }
        private string Date;
        public string date {
            get
            {
                return Date;
            }
            set
            {
                if (Date != value)
                {
                    Date = value;
                    OnPropertyChanged("date");
                }
            
            }
        }
        private string Context;
        public string context
        { 
            get
            {
             return this.Context;
            }
            set
            {
             if(Context!=value)
             {
               Context=value;
               OnPropertyChanged("context");
             }
            }
        }
        public string image { get; set; }
        public object obj;
        public ObservableCollection<title> title_item { get; set; }
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
    }
     

