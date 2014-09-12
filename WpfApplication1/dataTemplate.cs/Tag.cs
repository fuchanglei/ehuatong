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
   public class Tag:INotifyPropertyChanged
    {

       private string _Icon;
       public string Icon
       {
           get
           {
               return _Icon;
           }
           set
           {
               if (_Icon != value)
               {
                   _Icon = value;
                   OnPropertyChanged("Icon");
               }
           }
       }
        private Visibility _isedit;
        public Visibility isedit
        {
            get
            {
                return _isedit;
            }
            set
            {
                if (_isedit != value)
                {
                   _isedit= value;
                    OnPropertyChanged("isedit");
                }
            }
        }
        private string dispalyname;
        public string DisplayName 
        { get
          {
           return dispalyname;
          }
            set
            {
                if (dispalyname != value)
                {
                    dispalyname = value;
                    OnPropertyChanged("DisplayName");
                }
            }
          }
        private Visibility _isshow;
        public Visibility isshow
        {
            get
            {
                return this._isshow;
            }
            set
            {
                if (_isshow != value)
                {
                    _isshow = value;
                    OnPropertyChanged("isshow");
                }
            }
        }
        private Tag _parent;
        public Tag parent
         {
             get
             {
                 return this._parent;
             }
             set
             {
                 if (_parent != value)
                 {
                     _parent = value;
                     OnPropertyChanged("parent");
                 }
             }
        }
        public object obj {get;set; }
        public ObservableCollection<Tag> Children { get; set; }
        public Tag()
        {
            Children = new ObservableCollection<Tag>();
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
   public class Tag_data:INotifyPropertyChanged
   {
       private Visibility _isvisiblil;
       
       public Visibility isvisibli 
       {

           get
           {
               return this._isvisiblil;
           }
           set
           {
               if (_isvisiblil != value)
               {
                   _isvisiblil = value;
                   OnPropertyChanged("isvisibli");
               }
           }
       }
       private string _name;
       public string name
       {

           get
           {
               return this._name;
           }
           set
           {
               if (_name!= value)
               {
                   _name = value;
                   OnPropertyChanged("name");
               }
           }
       }
      // public ObservableCollection<Tag_data> title_item { get; set; }
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
