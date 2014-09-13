using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Xml;
//using Code.WebbrowserInteropr;

namespace WpfApplication1
{
    public enum outlinetype
    {   
        common=0,
        Chapter = 1,
        Section1=2,
        Section2=3,
        empty=4,
    }

 public class outline : INotifyPropertyChanged  //目录数据类型
    {
       public string secid { get; set; }
       private string _Name;
       public string Name1 {
           get
           {
               return _Name;
           }
           set
           {
               if (_Name != value)
               {
                   _Name = value;
                   OnPropertyChanged("Name");
               }
           }
       }
       public string nodename{get;set;}
       private outline _parent;
       public outline parent
       {
           get
           {
               return this._parent;
           }
           set
           {
               if (_parent!= value)
               {
                   _parent = value;
                   OnPropertyChanged("parent");
               }
           }
       }
       public string toollip { get; set; }
       //public string nodetype { get; set; }
       private outlinetype _type;
       public outlinetype type
       {
           get
           {
               return this._type;
           }
           set
           {
               if (_type != value)
               {
                   _type = value;
                   OnPropertyChanged("type");
               }
           }
       }
       private string _href;
       public string href
       {
           get
           {
               return this._href;
           }
           set
           {
               if (_href != value)
               {
                   _href = value;
               }
           }
       }
       public ObservableCollection<outline> children{ get; set; }
       public outline()
       {
           children = new ObservableCollection<outline>();
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
   public class outline_Data {
       private XmlDocument doc_outline;
       private XmlDocument doc_contex;
       private XmlDocument doc_tem;
       private XmlNode root_outline;
       private XmlNode root_contex;
       private XmlNode root_tem;
       private string outline_xml;
       private string idis_xml;
       private string tem_xml;
      // private static outline_Data mInstance;
       public static outline_Data Instance { get; set; }
       private outline getnodes(XmlNode parent, outline parentnode) //parent 是xml节点的父节点parentnode是父节点在tree中的父节点
       {
           //
           outline aa = new outline()
           {
               secid = ((XmlElement)parent).GetAttribute("id"),
               Name1 = ((XmlElement)parent).GetAttribute("id")+" "+((XmlElement)parent).GetAttribute("sec-title"),
               nodename=parent.Name,
               toollip=((XmlElement)parent).GetAttribute("sec-title"),
               href =((XmlElement)parent).GetAttribute("href"),
               parent=parentnode,
               type = (outlinetype)((int)parentnode.type+1)
              // nodetype=parent.Name
               
               
           };
           
           if (parent.HasChildNodes == false)
           {
              
               return aa;
           }
           else
           {
              // Console.WriteLine(aa.secid + " " + aa.Name);
               XmlNodeList ac = parent.ChildNodes;
               foreach (XmlNode acs in ac)
               {
                   aa.children.Add(getnodes(acs,aa));
               }
               return aa;
           }
          
       }
       public outline_Data(bool a)
       {
           outline_xml = MainWindow.idd_href + "\\iDisseration_outline.xml";
           doc_outline = new XmlDocument();
           doc_outline.Load(outline_xml);
           root_outline = doc_outline.DocumentElement;
       }
       public outline_Data()
       {
           outline_xml = MainWindow.idd_href + "\\iDisseration_outline.xml";
           idis_xml = MainWindow.idd_href + "\\idis.xml";
           tem_xml = MainWindow.idd_href + "\\webTemplate.xml";
           doc_outline = new XmlDocument();
           doc_outline.Load(outline_xml);
           root_outline = doc_outline.DocumentElement;
           doc_contex = new XmlDocument();
           doc_contex.Load(idis_xml);
           root_contex = doc_contex.DocumentElement.SelectSingleNode("Papersection");
           doc_tem = new XmlDocument();
           doc_tem.Load(tem_xml);
           root_tem = doc_tem.DocumentElement.SelectSingleNode("Papersection");
       }
       public static string getparent(outline sel)
       {
           if (sel.parent == null)
               return sel.nodename;
           else
              return getparent(sel.parent)+"/"+sel.nodename;
             
       }  //获取选中节点的父类
       private ObservableCollection<outline> GenerateTreeViewItems1()
       {
           ObservableCollection<outline> cc = new ObservableCollection<outline>();
           XmlDocument doc = new XmlDocument();
           doc.Load(MainWindow.idd_href+"\\iDisseration_outline.xml");
           //Console.WriteLine("Begin reading xml");
          // XmlElement rootElem = doc.DocumentElement;
           //XmlNodeList section = rootElem.GetElementsByTagName("section");
           //List<outline> cc = new List<outline>();
           XmlNode root = doc.DocumentElement;
           XmlNodeList xmllist = root.ChildNodes;
           foreach (XmlNode xm in xmllist)
           {
               if (xm.Name.Contains("Chapter")==false)
               {
                   outline newnoe = new outline()
                   {
                       secid = ((XmlElement)xm).GetAttribute("id"),
                       Name1 = xm.InnerText,
                       nodename=xm.Name,
                       toollip = xm.InnerText,
                       type = (outlinetype)Enum.Parse(typeof(outlinetype), ((XmlElement)xm).GetAttribute("nodetype")),
                       href= ((XmlElement)xm).GetAttribute("href"),
                       parent=null

                   };
                   
                   cc.Add(newnoe);
                   // Console.WriteLine(xm.Name);
                  // Console.WriteLine(xm.InnerText);
               }
               else
               {
                   outline chapter = new outline()
                   {
                       secid = ((XmlElement)xm).GetAttribute("id"),
                       Name1 = ((XmlElement)xm).GetAttribute("id")+" "+((XmlElement)xm).GetAttribute("sec-title"),
                       nodename=xm.Name,
                       href = ((XmlElement)xm).GetAttribute("href"),
                       toollip=((XmlElement)xm).GetAttribute("sec-title"),
                       type=outlinetype.Chapter,
                       parent=null
                   };
                   cc.Add(chapter);
                   //Console.WriteLine(xm.Name);
                   XmlNodeList xml = xm.ChildNodes;

                   foreach (XmlNode xms in xml)
                   {
                       string title = ((XmlElement)xms).GetAttribute("sec-title");
                       outline aa = new outline()
                       {
                           secid = ((XmlElement)xms).GetAttribute("id"),
                           Name1 = ((XmlElement)xms).GetAttribute("sec-title"),
                           nodename=xms.Name,
                           href =((XmlElement)xms).GetAttribute("href"),
                           toollip=title,
                           type=outlinetype.Section1,
                           parent=chapter
                       };
                       chapter.children.Add(getnodes(xms,chapter));
                  }
               }
           }
           return cc;
       }
       public ObservableCollection<outline> TreeViewItems1
       {   
           get
           {
               if (mTreeViewItems1 == null)
                   mTreeViewItems1 = GenerateTreeViewItems1();
               return mTreeViewItems1;
           }
           set
           {
               if (mTreeViewItems1 != value)
                   mTreeViewItems1 = value;
           }
          
       }
       private XmlNode getselectnode(outline ss) //获取outline选中的节点
       {
           XmlNode select=null;
           XmlNodeList xmllist = root_outline.SelectNodes(getparent(ss));
           foreach (XmlNode ses in xmllist)
           {
               if (((XmlElement)ses).GetAttribute("id") == ss.secid)
               {
                   select = ses;
                   break;
               }
           }
           return select;
       }
       private XmlNode get_contexnode(string name, string id)  //获取与内容节点
       {
           XmlNode node = null;
           XmlNodeList context = root_contex.SelectNodes(name);
           foreach (XmlNode xm in context)
           {
               if (((XmlElement)xm).GetAttribute("id") == id)
               {
                   node = xm;

                   break;
               }
           }
           return node;
       }
       private XmlNode get_temnode(string name, string id)  //获取Tem节点
       {
           XmlNode node = null;
           XmlNodeList context = root_tem.SelectNodes(name);
           foreach (XmlNode xm in context)
           {
               if (((XmlElement)xm).GetAttribute("id") == id)
               {
                   node = xm;

                   break;
               }
           }
           return node;
       }
       public XmlNode getlasnode(outline select)  //获取outline选中节点的最后一个子节点
       {
           if (select.children.Count == 0)
           {
               return get_contexnode(select.nodename, select.secid);

           }
           else
           {
               return getlasnode(select.children[select.children.Count-1]);
               //return get_contexnode(select.children[select.children.Count - 1].nodename, select.children[select.children.Count - 1].secid);
           }
       }
       public XmlNode getlasnode_tem(outline select)  //获取tem中节点的最后一个子节点
       {
           if (select.children.Count == 0)
           {
               return get_temnode(select.nodename, select.secid);

           }
           else
           {
               return getlasnode_tem(select.children[select.children.Count - 1]);
               //return get_contexnode(select.children[select.children.Count - 1].nodename, select.children[select.children.Count - 1].secid);
           }
       }     
       public void outline_node_add(outline select,string newname)  //增加小节
       {
           
         //  string cc = getparent(select);
           //root_outline = doc.DocumentElement;
           XmlNode a = getselectnode(select);
           XmlNode b = getlasnode(select);
           XmlNode c = getlasnode_tem(select);
           XmlNodeList xmllist = a.ChildNodes;
           string id=select.secid+"."+(xmllist.Count+1).ToString();
           string newname_add =id+" "+newname;
           string href = select.href + "/" + id;
           string type = ((outlinetype)((int)select.type + 1)).ToString();
           string nodename = type;
           outline newone = new outline()
           {
               Name1 = newname_add,
               toollip = newname_add,
               parent = select,
               secid=id,
               nodename=nodename,
               href=href,
               type = (outlinetype)((int)select.type + 1)
           };
           select.children.Add(newone);
           XmlElement xe1 = doc_outline.CreateElement(nodename);//创建一个节点
           xe1.SetAttribute("id", id);//设置该节点id属性
           xe1.SetAttribute("sec-title", newname);//设置该节点name属性
           xe1.SetAttribute("nodetype", type);//设置该节点type属性
           xe1.SetAttribute("href", href);//设置节点的href
           a.AppendChild(xe1);
           doc_outline.Save(outline_xml);
           XmlDocument doc = new XmlDocument();
           doc.Load( MainWindow.idd_href + "/webTemplateStyle.xml");
           XmlNode style= doc.DocumentElement.SelectSingleNode(type);
           XmlElement xe2 = doc_contex.CreateElement(nodename);
           xe2.SetAttribute("id",id);
           xe2.InnerXml = style.InnerXml.Replace(")#@paper_id@#)", id);
           xe2.InnerXml = xe2.InnerXml.Replace(")#@paper_title@#)", newname);
           root_contex.InsertAfter(xe2,b);
           doc_contex.Save(idis_xml);
           XmlElement xe3 = doc_tem.CreateElement(nodename);
           XmlElement xe3_title = doc_tem.CreateElement("papersectiontitle");
           XmlElement xe3_text = doc_tem.CreateElement("papersectiontext");
           xe3.SetAttribute("id", id);
           xe3.AppendChild(xe3_title);
           xe3.AppendChild(xe3_text);
           root_tem.InsertAfter(xe3,c);
           doc_tem.Save(tem_xml);
       }
       private void delete_contex(string name,string id)  //删除内容
       {
           XmlNodeList context = root_contex.SelectNodes(name);
           foreach (XmlNode xm in context)
           {
               if (((XmlElement)xm).GetAttribute("id") == id)
               {
                   root_contex.RemoveChild(xm);
                   doc_contex.Save(idis_xml);
                   break;
               }
           }
           XmlNodeList tem = root_tem.SelectNodes(name);
           foreach (XmlNode xm in tem)
           {
               if (((XmlElement)xm).GetAttribute("id") == id)
               {
                   //xm.RemoveAll();
                   root_tem.RemoveChild(xm);
                   doc_tem.Save(tem_xml);
                   break;
               }
           }
       }
       
       private void updateid_context(string name,string id,string newid) //更新idis和tem
       { 
           XmlNodeList context = root_contex.SelectNodes(name);
           foreach (XmlNode xm in context)
               {
                   if (((XmlElement)xm).GetAttribute("id") == id)
                   {    
                       ((XmlElement)xm).SetAttribute("id",newid);
                       xm.SelectSingleNode("papersectionid").InnerText=newid;
                       doc_contex.Save(idis_xml);
                       break;
                   }
               }
           XmlNodeList tem = root_tem.SelectNodes(name);
           foreach (XmlNode xm in tem)
           {
               if (((XmlElement)xm).GetAttribute("id") == id)
               {   
                   ((XmlElement)xm).SetAttribute("id", newid);
                   doc_tem.Save(tem_xml);
                   break;
               }
           }

       }
       private void updateid(XmlNode ss,string oldid,string newid)  //更新id
       {
           
           Regex r = new Regex(oldid);
           XmlElement xe = (XmlElement)ss;
           string id=xe.GetAttribute("id");
           string _newid = r.Replace(id,newid,1);//只替换一次
           if (ss.HasChildNodes == false)
           {
               updateid_context(ss.Name,id,_newid);
               xe.SetAttribute("id",_newid);
               doc_outline.Save(outline_xml);
           }
           else
           {
               XmlNodeList xmlist = ss.ChildNodes;
               foreach (XmlNode xn in xmlist)
               {
                   updateid(xn,id,_newid);
               }
           }

       }
       private void upadatexml(XmlNode select,string oldid)
       {
           //XmlNode ss = select.NextSibling;
           string id = ((XmlElement)select).GetAttribute("id");
           ((XmlElement)select).SetAttribute("id", oldid);
           updateid_context(select.Name, id, oldid);
           foreach (XmlNode ccs in select.ChildNodes)
           {
               updateid(ccs, id, oldid);
           }
           if (select.NextSibling == null || ((XmlElement)select.NextSibling).GetAttribute("nodetype") == "common")
               return;
           else
           {
               //string cc = ((XmlElement)select).GetAttribute("id");
               //  doc_outline.Save(outline_xml);
               upadatexml(select.NextSibling, id);
           }
       }
       private void delet_outline(XmlNode deletenode) //删除outline节点同时删除idis和temp相对应的节点
       {
           if (deletenode.HasChildNodes == false)
           {
               deletenode.ParentNode.RemoveChild(deletenode);
               delete_contex(deletenode.Name,((XmlElement)deletenode).GetAttribute("id"));
           }
           else
           {
               foreach (XmlNode xm in deletenode.ChildNodes)
               {
                   //deletenode.RemoveChild(xm);
                   delet_outline(xm);
               }
               deletenode.ParentNode.RemoveChild(deletenode);
               delete_contex(deletenode.Name, ((XmlElement)deletenode).GetAttribute("id"));
           }
       }
       public void outline_node_delet(outline select) //删除节点
       {
           XmlNode b = getselectnode(select);
           XmlNode b_next = b.NextSibling;
           string oldid = ((XmlElement)b).GetAttribute("id");
           delet_outline(b);
           //root_outline.RemoveChild(b);
          //delete_contex(b.Name, oldid);
           if (b_next != null && ((XmlElement)b_next).GetAttribute("nodetype")!="common")
           {
               upadatexml(b_next, oldid);
           }
           doc_outline.Save(outline_xml);
       }
       public void outline_node_modify(outline select, string newname)  //修改outline的接点名称
       {
           XmlNode a = getselectnode(select);
           string id = select.secid;
           XmlNode contex_node = get_contexnode(select.nodename,select.secid);
           contex_node = contex_node.SelectSingleNode("papersectiontitle");
           string context = contex_node.InnerText;
           ((XmlElement)contex_node).InnerXml = contex_node.InnerXml.Replace(context," "+newname);
            doc_contex.Save(idis_xml);
           ((XmlElement)a).SetAttribute("sec-title",newname);
           doc_outline.Save(outline_xml);
           
       }
       public outline outline_chapter_add(outline select, string newname)  //增加章节
       {
           doc_outline = new XmlDocument();
           doc_outline.Load(outline_xml);
           root_outline = doc_outline.DocumentElement;
           doc_contex = new XmlDocument();
           doc_contex.Load(idis_xml);
           root_contex = doc_contex.DocumentElement.SelectSingleNode("Papersection");
           string id = (int.Parse(select.secid) + 1).ToString();
           string href = id;
           string newname_add = id + " " + newname;
           string nodename = "Chapter";
           string type = "Chapter";
           outline newone = new outline()
           {
               Name1 = newname_add,
               toollip = newname_add,
               parent = null,
               secid = id,
               nodename = nodename,
               href = href,
               type = outlinetype.Chapter
           };
           XmlElement xe1 = doc_outline.CreateElement(nodename);//创建一个节点
           xe1.SetAttribute("id", id);//设置该节点id属性
           xe1.SetAttribute("sec-title", newname);//设置该节点name属性
           xe1.SetAttribute("nodetype", type);//设置该节点type属性
           xe1.SetAttribute("href", href);//设置节点的href
           XmlNode bb = getselectnode(select);
           root_outline.InsertAfter(xe1,bb);
           doc_outline.Save(outline_xml);
           XmlDocument doc = new XmlDocument();
           doc.Load(MainWindow.idd_href + "/webTemplateStyle.xml");
           XmlNode style = doc.DocumentElement.SelectSingleNode(type);
           XmlElement xe2 = doc_contex.CreateElement(nodename);
           xe2.SetAttribute("id",id);
           xe2.InnerXml = style.InnerXml.Replace(")#@paper_id@#)",id);
           xe2.InnerXml = xe2.InnerXml.Replace(")#@paper_title@#)",newname);
          // xe2.SelectSingleNode("papersectionid").InnerText = id;
          // xe2.SelectSingleNode("papersectiontitle").InnerText = " " + newname;
           //root_contex.InsertAfter(xe2);
           root_contex.AppendChild(xe2);
           doc_contex.Save(idis_xml);
           XmlElement xe3 = doc_tem.CreateElement(nodename);
           XmlElement xe3_title = doc_tem.CreateElement("papersectiontitle");
           XmlElement xe3_text = doc_tem.CreateElement("papersectiontext");
           xe3.SetAttribute("id", id);
           xe3.AppendChild(xe3_title);
           xe3.AppendChild(xe3_text);
           root_tem.AppendChild(xe3);
           doc_tem.Save(tem_xml);
           return newone;

       }
       private ObservableCollection<outline> mTreeViewItems1 = null;
       
   }
}

