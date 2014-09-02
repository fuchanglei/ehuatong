using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Xml;


namespace WpfApplication1
{   
    
    public enum iDissType
    {
        kaitibaogao = 0,
        zhongqi= 1,
        biyelunwen = 2,
        qikanlunwen=3,
        yanjiuzongsu=4,
        zikeshenqing=5,
        shekeshenqing=6,
        yanjiubaogao=7,
        nonode=8
    }
   public class iDissertation:INotifyPropertyChanged
    {
       public string icon { get;set;}
       private string _Name;
       public string Name
       {
           get
            {
                return _Name;
           }
           set
           {
               if (this._Name != value)
               {
                   _Name = value;
                   OnPropertyChanged("Name");
               }
           }
       }
       public iDissType nodetype { get; set; }
       //public iDissertation parent { get;set;}
       public string href { get; set; }
       public ObservableCollection<iDissertation> Children { get; set; }
       public iDissertation()
       {
           Children = new ObservableCollection<iDissertation>();
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
   public class idisser_data {
       private static idisser_data _idisser = new idisser_data();
       //private copy_files cf;
       public static idisser_data idisser
       {
           get { return _idisser; }
       }
       private static XmlDocument doc = new XmlDocument();
       private static XmlNode root;
       static idisser_data()
       {
           doc.Load("iDissertation.xml");
           root = doc.DocumentElement;
       }
       private ObservableCollection<iDissertation> getidisser_data()
       {
           ObservableCollection<iDissertation> item = new ObservableCollection<iDissertation>();
           //XmlNode root = doc.DocumentElement;
           XmlNodeList xmllist = root.ChildNodes;
           foreach (XmlNode xm in xmllist)
           {
               iDissertation node= new iDissertation()
               {
                   
                   icon = Window4.icons[int.Parse(((XmlElement)xm).GetAttribute("type"))],
                   Name = (((XmlElement)xm).GetAttribute("name")),
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   href = (((XmlElement)xm).GetAttribute("href"))
                   // parent=null
               };
               iDissertation node_Article = new iDissertation()
               {

                   icon = @"images/address book.ico",
                   Name = "文献",
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   nodetype = iDissType.nonode,
                  
                   // parent=null
               };
               iDissertation node_Data= new iDissertation()
               {

                   icon = @"images/finder.ico",
                   Name = "数据",
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   nodetype = iDissType.nonode,

                   // parent=null
               };
               node.Children.Add(node_Article);
               node.Children.Add(node_Data);
               item.Add(node);
           }
           return item;
       }
      
       public ObservableCollection<iDissertation> TreeViewItems4
       {
           get
           {
               if (_TreeViewItems4 == null)
                   _TreeViewItems4 = getidisser_data();
               return _TreeViewItems4;
           }
           set
           {
               if (_TreeViewItems4 != value)
                   _TreeViewItems4 = value;
           }

       }
       public void TreeViewItems4_add(iDissertation newtitle)   //增点一个idd项
       {
           Directory.CreateDirectory(newtitle.href);
           Directory.CreateDirectory(newtitle.href+"\\music");
          // Directory.CreateDirectory(newtitle.href + "\\video");
           Directory.CreateDirectory(newtitle.href + "\\picture");
           Directory.CreateDirectory(newtitle.href + "\\music");
           idisser_data.idisser.TreeViewItems4.Add(newtitle);
           XmlElement xe1 = doc.CreateElement("item");//创建一个节点
           xe1.SetAttribute("id", (_TreeViewItems4.Count+1).ToString());//设置该节点id属性
           xe1.SetAttribute("name", newtitle.Name);//设置该节点name属性
           xe1.SetAttribute("type", ((int)newtitle.nodetype).ToString());//设置该节点type属性
           xe1.SetAttribute("href",newtitle.href);//设置节点的href
           root.AppendChild(xe1);
           doc.Save("iDissertation.xml");
          // cf = new copy_files(System.Environment.CurrentDirectory.ToString()+"\\"+((int)newtitle.nodetype).ToString(), newtitle.href);
           copy_files.copyfile(System.Environment.CurrentDirectory.ToString() + "\\" + ((int)newtitle.nodetype).ToString(), newtitle.href);
           Savexml sa = new Savexml(newtitle.href);
           sa.init_idis();
           //Directory.CreateDirectory(newtitle.href);
       }
       public void TreeViewItems4_delete(iDissertation newtitle)
       {

           try
           {
               foreach (XmlNode xm in root.ChildNodes)
               {
                   if (((XmlElement)xm).GetAttribute("name") == newtitle.Name)
                       root.RemoveChild(xm);
               }
               doc.Save("iDissertation.xml");
               //TreeViewItems4.Remove(newtitle);
               idisser_data.idisser.TreeViewItems4.Remove(newtitle);
               copy_files.DeleteDir(newtitle.href);
           }
           catch
           {
               ;
           }
           
       }
       public void TreeViewItems4_modify(iDissertation newtitle,string newname)
       {
           foreach (XmlNode xm in root.ChildNodes)
           {
               if (((XmlElement)xm).GetAttribute("name") == newtitle.Name)
                   ((XmlElement)xm).SetAttribute("name",newname);
           }
           doc.Save("iDissertation.xml");
           

         
       }
       private ObservableCollection<iDissertation> _TreeViewItems4 = null;
   }
    
}
