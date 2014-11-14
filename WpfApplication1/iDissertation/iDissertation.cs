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
       private iDissertation _parent;
       public iDissertation parent
       {
         get{return _parent;}
         set{ _parent=value; }
       }
       private ObservableCollection<title> _data;
       public ObservableCollection<title> data
       {
           get { return _data; }
           set { _data = value; }
       }
       private ObservableCollection<title> _article;
       public ObservableCollection<title> article
       {
           get { return _article; }
           set { _article = value; }
       }
       private ObservableCollection<title> _tools;
       public ObservableCollection<title> tools
       {
           get { return _tools; }
           set { _tools = value; }
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
       private ObservableCollection<title> getiDissertationData(XmlNode nodetype)
       {
               ObservableCollection<title> result = new ObservableCollection<title>();
               XmlNodeList datalist = nodetype.SelectSingleNode("Data").ChildNodes;
               foreach (XmlNode xm in datalist)
               {
                   title cc = new title() { 
                   title_name=xm.InnerText,
                   context=((XmlElement)xm).GetAttribute("href"),
                   date = ((XmlElement)xm).GetAttribute("datatime")
                   };
                   result.Add(cc);
               }
               return result;
       }
       
       public static ObservableCollection<title> getiDissertationArticle(string dirinfo)
       {
           ObservableCollection<title> result = new ObservableCollection<title>();
          // XmlNode cc = nodetype.SelectSingleNode("article");
          // string dirinfo = ((XmlElement)nodetype).GetAttribute("articleDir");
           if (dirinfo != "")
           {
               result = copy_files.Getfiles(dirinfo);
           }
          // string dirinfo=((XmlElement)(nodetype.SelectSingleNode("article"))
           return result;
       }

       private ObservableCollection<iDissertation> getidisser_data()  //初始化idis项
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
                   href = (((XmlElement)xm).GetAttribute("href")),
                   data = getiDissertationData(xm),
                   article = getiDissertationArticle(((XmlElement)xm).GetAttribute("articleDir")),
                    tools=getiDissertationArticle(((XmlElement)xm).GetAttribute("toolsDir"))
                   // parent=null
               };
               iDissertation node_Article = new iDissertation()
               {

                   icon = @"images/address book.ico",
                   Name = "文献",
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   nodetype = iDissType.nonode,
                   parent=node,
                   
                   
                   // parent=null
               };
               iDissertation node_Data= new iDissertation()
               {

                   icon = @"images/finder.ico",
                   Name = "数据",
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   nodetype = iDissType.nonode,
                   parent = node,
                   data=node.data
                   // parent=null
               };
               iDissertation node_tools = new iDissertation()
               {

                   icon = @"images/trash.ico",
                   Name = "工具",
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   nodetype = iDissType.nonode,
                   parent = node,
                   
                   // parent=null
               };
               //node.Children.Add(node_tools);
               node.Children.Add(node_Article);
               node.Children.Add(node_Data);
               node.Children.Add(node_tools);
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
          // newtitle.article=
           iDissertation node_Article = new iDissertation()
           {

               icon = @"images/address book.ico",
               Name = "文献",
               //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
               nodetype = iDissType.nonode,
               parent=newtitle
               // parent=null
           };
           iDissertation node_Data = new iDissertation()
           {

               icon = @"images/finder.ico",
               Name = "数据",
               //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
               nodetype = iDissType.nonode,
               parent = newtitle,
               data=newtitle.data
               // parent=null
           };
           iDissertation node_tools = new iDissertation()
           {

               icon = @"images/trash.ico",
               Name = "工具",
               //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
               nodetype = iDissType.nonode,
               parent = newtitle,
               data=newtitle.data
               // parent=null
           };
           newtitle.Children.Add(node_Article);
           newtitle.Children.Add(node_Data);
           newtitle.Children.Add(node_tools);
           idisser_data.idisser.TreeViewItems4.Add(newtitle);
           XmlElement xe1 = doc.CreateElement("item");//创建一个节点
           XmlElement xe_data = doc.CreateElement("Data");
          // XmlElement xe_article = doc.CreateElement("article");
           xe1.SetAttribute("id", (_TreeViewItems4.Count+1).ToString());//设置该节点id属性
           xe1.SetAttribute("name", newtitle.Name);//设置该节点name属性
           xe1.SetAttribute("type", ((int)newtitle.nodetype).ToString());//设置该节点type属性
           xe1.SetAttribute("href",newtitle.href);//设置节点的href
           xe1.SetAttribute("toolsDir","D:\\tools");
           xe1.AppendChild(xe_data);
          // xe1.AppendChild(xe_article);
           root.AppendChild(xe1);
           doc.Save("iDissertation.xml");
           newtitle.article = null;
           newtitle.tools = getiDissertationArticle("D:\\tools");
          // cf = new copy_files(System.Environment.CurrentDirectory.ToString()+"\\"+((int)newtitle.nodetype).ToString(), newtitle.href);
           copy_files.copyfile(System.Environment.CurrentDirectory.ToString() + "\\" + ((int)newtitle.nodetype).ToString(), newtitle.href);
           Savexml sa = new Savexml(newtitle.href);
           sa.init_idis();
           //Directory.CreateDirectory(newtitle.href);
       }
       public void deletiDissertationData(string nodename,title deleteone)
       {
           XmlNodeList xmlnodes = root.SelectNodes("item");
           foreach (XmlNode xm in xmlnodes)
           {
               if (((XmlElement)xm).GetAttribute("name") == nodename)
               {
                   XmlNodeList xml = xm.SelectSingleNode("Data").ChildNodes;
                   foreach (XmlNode xx in xml)
                   {
                       if (xx.InnerText == deleteone.title_name)
                       {
                           xm.SelectSingleNode("Data").RemoveChild(xx);
                           break;
                       }
                   }
                   break;
               }
               
           }
           doc.Save("iDissertation.xml");
       }
       public void AddiDissertationData_article(string nodename,title newnone)
       {
           XmlNodeList xmlnodes = root.SelectNodes("item");
           foreach (XmlNode xm in xmlnodes)
           {
               if (((XmlElement)xm).GetAttribute("name") == nodename)
               {
                   XmlElement xe = doc.CreateElement("dateitem");
                   xe.SetAttribute("href",newnone.context);
                   xe.SetAttribute("datatime", newnone.date);
                   xe.InnerText=newnone.title_name;
                   xm.SelectSingleNode("Data").AppendChild(xe);
                   doc.Save("iDissertation.xml");
               }
               break;
           }
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
       public void TreeViewItems4_modify_article_dir(iDissertation newtitle, string newname)
       {
           foreach (XmlNode xm in root.ChildNodes)
           {
               if (((XmlElement)xm).GetAttribute("name") == newtitle.Name)
                   ((XmlElement)xm).SetAttribute("articleDir", newname);
           }
           doc.Save("iDissertation.xml");
           newtitle.article = getiDissertationArticle(newname);
       }
       private ObservableCollection<iDissertation> _TreeViewItems4 = null;
   }
    
}
