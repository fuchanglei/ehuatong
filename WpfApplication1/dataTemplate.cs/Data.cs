using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace WpfApplication1
{
    public enum Nodeetype
    {  
      one=0,
      subject=1,
      Directory=2,
      note=3,
      none=4
   
    }
    public class PropertyNodeItem:INotifyPropertyChanged
    {
        private string icon;
        public string Icon 
        { get
          {
           return icon;
           }
            set
            {
                if (this.icon != value)
                {
                    icon = value;
                    OnPropertyChanged("Icon");
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
        public string Name { get; set; }
        private PropertyNodeItem p;
        public PropertyNodeItem parent
         {
             get
             {
                 return this.p;
             }
             set
             {
                 if (p != value)
                 {
                     p = value;
                     OnPropertyChanged("parent");
                 }
             }
        }
        public Nodeetype node_lev { get; set; }
        public ObservableCollection<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem()
        {
            Children = new ObservableCollection<PropertyNodeItem>();


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

    }
    
   #endregion
   public class Data
    {
        private static Data mInstance = new Data();
        public static Data Instance
        {
            get { return mInstance; }
        }
        private ObservableCollection<PropertyNodeItem> GenerateTreeViewItems1()
        {
            ObservableCollection<PropertyNodeItem> itemObservableCollection1 = new ObservableCollection<PropertyNodeItem>();
            PropertyNodeItem node1 = new PropertyNodeItem()
            {
                DisplayName = "@我的消息",
                Name = "This is the discription of Node1. This is a folder1111111.",
                Icon = @"images/user_messages.ico",
                parent=null,
                node_lev=Nodeetype.none

            };
            PropertyNodeItem node1_1 = new PropertyNodeItem()
            {
                DisplayName = "提到我的消息",
                Name = "This is the discription of Node1. This is a folder1111.",
                Icon = @"images/user_called_messages.ico",
                parent=node1,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node1_2 = new PropertyNodeItem()
            {
                DisplayName = "笔记被修改的消息",
                Name = "This is the discription of Node1. This is a folde ddd.",
                Icon = @"images/user_modified_messages.ico",
                parent = node1,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node1_3 = new PropertyNodeItem()
            {
                DisplayName = "评论消息",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/user_comment_messages.ico",
                parent = node1,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node2 = new PropertyNodeItem()
            {
                DisplayName = "我的评论",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/favorites.ico",
                parent=null,
                node_lev = Nodeetype.none

            };
            PropertyNodeItem node3 = new PropertyNodeItem()
            {
                DisplayName = "快速搜索",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent=null,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_1 = new PropertyNodeItem()
            {
                DisplayName = "通过属性搜索(个人笔记）",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent = node3,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_2 = new PropertyNodeItem()
            {
                DisplayName = "通过创建时间",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent = node3,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_3 = new PropertyNodeItem()
            {
                DisplayName = "通过修改时间",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent = node3,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_4 = new PropertyNodeItem()
            {
                DisplayName = "通过访问时间",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent = node3,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_1_1 = new PropertyNodeItem()
            {
                DisplayName = "无标记笔记",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent=node3_1,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_1_2 = new PropertyNodeItem()
            {
                DisplayName = "一星",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent = node3_1,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_2_1 = new PropertyNodeItem()
            {
                DisplayName = "今日",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent = node3_1,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_3_1 = new PropertyNodeItem()
            {
                DisplayName = "今日",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent=node3_3,
                node_lev = Nodeetype.none
            };
            PropertyNodeItem node3_4_1 = new PropertyNodeItem()
            {
                DisplayName = "一个月内",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/attributes.ico",
                parent = node3_1,
                node_lev = Nodeetype.none
            };
            node1.Children.Add(node1_1);
            node1.Children.Add(node1_2);
            node1.Children.Add(node1_3);
            node3_1.Children.Add(node3_1_1);
            node3_1.Children.Add(node3_1_2);
            node3_2.Children.Add(node3_2_1);
            node3_3.Children.Add(node3_3_1);
            node3_4.Children.Add(node3_4_1);
            node3.Children.Add(node3_1);
            node3.Children.Add(node3_2);
            node3.Children.Add(node3_3);
            node3.Children.Add(node3_4);
            itemObservableCollection1.Add(node1);
            itemObservableCollection1.Add(node2);
            itemObservableCollection1.Add(node3);
            //this.tvProperties.ItemsSource = itemObservableCollection;

            return itemObservableCollection1;
        }
        private ObservableCollection<PropertyNodeItem> GenerateTreeViewItems2()
        {
            ObservableCollection<PropertyNodeItem> itemObservableCollection2 = new ObservableCollection<PropertyNodeItem>();
            PropertyNodeItem node1 = new PropertyNodeItem()
            {
                DisplayName = "我的专题集",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/my files.ico",
                parent=null,
                node_lev=Nodeetype.one
               

            };
            PropertyNodeItem node1_1 = new PropertyNodeItem()
            {
                DisplayName = "个人专题1",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/group_unclassed.ico",
                parent=node1,
                node_lev=Nodeetype.subject
               

            };
            PropertyNodeItem dire_1 = new PropertyNodeItem()
            {
                DisplayName = "我的目录",
                Name = "我的目录",
                Icon = @"images/Chat Folder.ico",
                parent = node1_1,
                node_lev = Nodeetype.Directory


            };
            PropertyNodeItem node1_2 = new PropertyNodeItem()
            {
                DisplayName = "个人专题2",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/group_unclassed.ico",
                parent = node1,
                node_lev=Nodeetype.subject
                

            };
            PropertyNodeItem node1_3 = new PropertyNodeItem()
            {
                DisplayName = "已删除",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/group_deleted.ico",
                parent = node1,
                node_lev = Nodeetype.none
                
              
            };

            PropertyNodeItem node1_4 = new PropertyNodeItem()
            {
                DisplayName = "标签",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/tag.ico",
                parent = node1,
                node_lev = Nodeetype.none
                


            };
            PropertyNodeItem node1_4_1 = new PropertyNodeItem()
            {
                DisplayName = "C#",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = "",
                parent = node1_4,
                node_lev=Nodeetype.note

            };
            node1_4.Children.Add(node1_4_1);
           // node1_1.Children.Add(dire_1);
            node1.Children.Add(node1_1);
            node1.Children.Add(node1_2);
            node1.Children.Add(node1_3);
            node1.Children.Add(node1_4);
            PropertyNodeItem node2= new PropertyNodeItem()
            {
                DisplayName = "点击显示全部标签",
                Name = "This is the discription of Node2. This is a folder.", 
                Icon = "",
                parent=null,
                node_lev = Nodeetype.none
            };

            itemObservableCollection2.Add(node1);
            itemObservableCollection2.Add(node2);
            node1_1.Children.Add(dire_1);
            return itemObservableCollection2;
        }
        private ObservableCollection<PropertyNodeItem> GenerateTreeViewItems3()
        {
            ObservableCollection<PropertyNodeItem> itemObservableCollection3 = new ObservableCollection<PropertyNodeItem>();
            PropertyNodeItem node1 = new PropertyNodeItem()
            {
                DisplayName = "团队专题1",
                Name = "This is the discription of Node2. This is a folder.",
                Icon = @"images/my files.ico",
                parent=null,

            };
            PropertyNodeItem node1_1 = new PropertyNodeItem()
            {
                DisplayName = "个人笔记",
                Name = "This is the discription of Node2. This is a folder.",
                Icon = @"images/grouptag.ico",
                parent=node1,

            };

            PropertyNodeItem node1_2 = new PropertyNodeItem()
            {
                DisplayName = "已删除",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/group_deleted.ico",
                parent = node1,

            };
            node1.Children.Add(node1_1);
            node1.Children.Add(node1_2);

            PropertyNodeItem node2 = new PropertyNodeItem()
            {
                DisplayName = "团队专题2",
                Name = "This is the discription of Node2. This is a folder.",
                Icon = @"images/my files.ico",
                parent=null,

            };
            PropertyNodeItem node2_1 = new PropertyNodeItem()
            {
                DisplayName = "个人笔记",
                Name = "This is the discription of Node2. This is a folder.",
                Icon = @"images/grouptag.ico",
                parent=node2,

            };

            PropertyNodeItem node2_2 = new PropertyNodeItem()
            {
                DisplayName = "已删除",
                Name = "This is the discription of Node2. This is a folder.",
                Icon = @"images/group_deleted.ico",
                parent = node2,

            };
            node2.Children.Add(node2_1);
            node2.Children.Add(node2_2);
            // node2.Children.Remove(node2_1);
            itemObservableCollection3.Add(node1);
            itemObservableCollection3.Add(node2);
            //this.tree3.ItemsSource = itemObservableCollection3;
            
            return itemObservableCollection3;
        }
        public ObservableCollection<PropertyNodeItem> TreeViewItems1
        {
            get
            {
                if (mTreeViewItems1 == null)
                    mTreeViewItems1 = GenerateTreeViewItems1();
                return mTreeViewItems1;
            }
        
        }
        public ObservableCollection<PropertyNodeItem> TreeViewItems2
        {
            get
            {
                if (mTreeViewItems2 == null)
                    mTreeViewItems2 = GenerateTreeViewItems2();
                return mTreeViewItems2;
            }
        
        }
        public ObservableCollection<PropertyNodeItem> TreeViewItems3
        {
            get
            {
                if (mTreeViewItems3 == null)
                    mTreeViewItems3 = GenerateTreeViewItems3();
                return mTreeViewItems3;
            }
           
        }
        private ObservableCollection<PropertyNodeItem> mTreeViewItems1 = null;
        private ObservableCollection<PropertyNodeItem> mTreeViewItems2 = null;
        private ObservableCollection<PropertyNodeItem> mTreeViewItems3 = null;
    }
}
