using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading;
using System.Xml;
using WpfApplication1.Refer;


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
       private ObservableCollection<outline> _outlines;
       public ObservableCollection<outline> outlines
       {
           get { return _outlines; }
           set { _outlines= value; }
       }
       private ObservableCollection<title> _data;
       public ObservableCollection<title> data
       {
           get { return _data; }
           set { _data = value; }
       }
       public string articlePath { get; set; }
       private ObservableCollection<title> _article;
       public ObservableCollection<title> article
       {
           get { return _article; }
           set { _article = value; }
       }
       private ObservableCollection<title> _tools;
       public string toolsPath { get; set; }
       public ObservableCollection<title> tools
       {
           get { return _tools; }
           set { _tools = value; }
       }
       public string mediaPath { get; set; }
       private ObservableCollection<title> _media;
       public ObservableCollection<title> media
       {
           get { return _media; }
           set { _media = value; }
       }
       public List<ReferencesInfo> Refer { get; set; }
       public List<int> Refernumber { get; set; }
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
       public static List<int> refercount;
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
       public static ObservableCollection<title> getiDissertations(string dirinfo)
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
       public static List<ReferencesInfo> initRefer(string path)
       {
           refercount = new List<int>();
           int number = 0;
           List<ReferencesInfo> refer = new List<ReferencesInfo>();
           XmlDocument refer_doc = new XmlDocument();
           refer_doc.Load(path+"\\idis.xml");
           XmlNodeList refer_context = refer_doc.DocumentElement.SelectSingleNode("Refer").SelectNodes("refermeta");
           foreach (XmlNode xm in refer_context)
           { 
               number++;
               refercount.Add(number);
               ReferencesInfo aa = new ReferencesInfo() { 
                Context=xm.SelectSingleNode("p/refertext").InnerText
               };
               refer.Add(aa);  
           }
           return refer;
       }
       private ObservableCollection<iDissertation> getidisser_data()  //初始化idis项
       {
           ObservableCollection<iDissertation> item = new ObservableCollection<iDissertation>();
           XmlNodeList xmllist = root.ChildNodes;
           foreach (XmlNode xm in xmllist)
           {   

               iDissertation node= new iDissertation()
               {
                   icon = Window4.icons[int.Parse(((XmlElement)xm).GetAttribute("type"))],
                   Name = (((XmlElement)xm).GetAttribute("name")),
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   href = (((XmlElement)xm).GetAttribute("href")),
                   data = getiDissertations(((XmlElement)xm).GetAttribute("href") + "\\data"),
                   articlePath=((XmlElement)xm).GetAttribute("articleDir"),
                   article = getiDissertations(((XmlElement)xm).GetAttribute("articleDir")),
                   toolsPath=((XmlElement)xm).GetAttribute("toolsDir"),
                   tools = getiDissertations(((XmlElement)xm).GetAttribute("toolsDir")),
                   mediaPath=((XmlElement)xm).GetAttribute("mediaDir"),
                   media = getiDissertations(((XmlElement)xm).GetAttribute("mediaDir")),
                   Refer = initRefer(((XmlElement)xm).GetAttribute("href")),
                   outlines=null
               };
               node.Refernumber = refercount;
               iDissertation node_Article = new iDissertation()
               {
                   icon = @"images/address book.ico",
                   Name = "文献",
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   nodetype = iDissType.nonode,
                   parent=node,
                   href = ((XmlElement)xm).GetAttribute("articleDir")
                   // parent=null
               };
               iDissertation node_Data= new iDissertation()
               {
                   icon = @"images/finder.ico",
                   Name = "数据",
                   nodetype = iDissType.nonode,
                   parent = node,
                   href=((XmlElement)xm).GetAttribute("href") + "\\data",
                   data=node.data
                  
               };
               iDissertation node_tools = new iDissertation()
               {
                   icon = @"images/trash.ico",
                   Name = "工具",
                   nodetype = iDissType.nonode,
                   href = ((XmlElement)xm).GetAttribute("toolsDir"),
                   parent = node,
                   // parent=null
               };
               iDissertation node_media = new iDissertation()
               {

                   icon = @"images/music cd.ico",
                   Name = "多媒体",
                   href = ((XmlElement)xm).GetAttribute("mediaDir"),
                   //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
                   nodetype = iDissType.nonode,
                   parent = node,
                   // parent=null
               };
               //node.Children.Add(node_tools);
               node.Children.Add(node_Article);
               node.Children.Add(node_Data);
               node.Children.Add(node_tools);
               node.Children.Add(node_media);
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
           Directory.CreateDirectory(newtitle.href + "\\data");
           newtitle.Refernumber = new List<int>();
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
               //data=newtitle.data
               // parent=null
           };
           iDissertation node_media = new iDissertation()
           {

               icon = @"images/music cd.ico",
               Name = "多媒体",
               //nodetype=(iDissType)(int.Parse(((XmlElement)xm.SelectSingleNode("type")).InnerText)
               nodetype = iDissType.nonode,
               parent = newtitle,
               // parent=null
           };
           newtitle.Children.Add(node_Article);
           newtitle.Children.Add(node_Data);
           newtitle.Children.Add(node_tools);
           newtitle.Children.Add(node_media);
           idisser_data.idisser.TreeViewItems4.Add(newtitle);
           copy_files.copyfile(System.Environment.CurrentDirectory.ToString() + "\\" + ((int)newtitle.nodetype).ToString(), newtitle.href);
           Savexml sa = new Savexml(newtitle.href);
           sa.init_idis();
           ThreadPool.QueueUserWorkItem(new WaitCallback(add_item_in_iDissertationxml),(object)newtitle);
           //Directory.CreateDirectory(newtitle.href);
       }
       private void add_item_in_iDissertationxml(object newone)
       {
           iDissertation newtitle = newone as iDissertation;
           XmlElement xe1 = doc.CreateElement("item");//创建一个节点
           XmlElement xe_data = doc.CreateElement("Data");
           // XmlElement xe_article = doc.CreateElement("article");
           xe1.SetAttribute("id", (_TreeViewItems4.Count + 1).ToString());//设置该节点id属性
           xe1.SetAttribute("name", newtitle.Name);//设置该节点name属性
           xe1.SetAttribute("type", ((int)newtitle.nodetype).ToString());//设置该节点type属性
           xe1.SetAttribute("href", newtitle.href);//设置节点的href
           xe1.SetAttribute("toolsDir", "D:\\tools");
           xe1.AppendChild(xe_data);
           // xe1.AppendChild(xe_article);
           root.AppendChild(xe1);
           doc.Save("iDissertation.xml");
           newtitle.article = null;
           newtitle.tools = getiDissertations("D:\\tools");
       }
       
       
       public void AddiDissertationData_article(string path,title newnone)
       {
           iDissertation selectnode = MainWindow.tree5_sel;
           string nodename = selectnode.parent.Name;
           File.Copy(path, selectnode.href+"\\"+newnone.title_name, true);
           }
       public void TreeViewItems4_delete(object deleteone)
       {
           iDissertation newtitle = deleteone as iDissertation;
           try
           {
               foreach (XmlNode xm in root.ChildNodes)
               {
                   if (((XmlElement)xm).GetAttribute("name") == newtitle.Name)
                       root.RemoveChild(xm);
               }
               doc.Save("iDissertation.xml");
               //TreeViewItems4.Remove(newtitle);
               ThreadPool.QueueUserWorkItem(new WaitCallback(copy_files.DeleteDir),(object)newtitle.href);
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
       public void TreeViewItems4_modify_dir(iDissertation newtitle, string newname)
       {
          
           foreach (XmlNode xm in root.ChildNodes)
           {
               if (((XmlElement)xm).GetAttribute("name") == newtitle.parent.Name)
               {
                   switch (newtitle.Name)
                   { 
                       case "文献":
                           ((XmlElement)xm).SetAttribute("articleDir", newname);
                            newtitle.parent.articlePath = newname;
                            newtitle.href = newname;
                           break;
                       case "工具":
                           ((XmlElement)xm).SetAttribute("toolsDirDir", newname);
                           newtitle.parent.toolsPath = newname;
                           newtitle.href = newname;
                           break;
                       case "多媒体":
                           ((XmlElement)xm).SetAttribute("mediaDir", newname);
                           newtitle.parent.mediaPath = newname;
                           newtitle.href = newname;
                           break;
                       default:
                           break;
                   }
                  
               }
           }
           doc.Save("iDissertation.xml");
          
       }
       private ObservableCollection<iDissertation> _TreeViewItems4 = null;
   }
    
}
