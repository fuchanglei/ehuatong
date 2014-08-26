using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

namespace WpfApplication1
{
   public class article
    {
      // private  string xml_context;
       private  string xml_style;
       private string cc;   //存储节点的类别，根据类别进行显示
       private string _id;
            
       private  XmlNode root_style;
       public article(string id,string name)
       {
           cc = name;
           _id = id;
           xml_style = MainWindow.idd_href + "/idis.xml";
           XmlDocument doc_style = new XmlDocument();
           doc_style.Load(xml_style);
           root_style = doc_style.DocumentElement;
          
       }
       public article(string name)
       {
           cc = name;
           xml_style = MainWindow.idd_href + "/idis.xml";
           XmlDocument doc_style = new XmlDocument();
           doc_style.Load(xml_style);
           root_style = doc_style.DocumentElement;

       }
       public string getcontext()
       {
           string html = string.Empty;      
           XmlNodeList ccwww = root_style.SelectNodes(cc);
           foreach (XmlNode ccd in ccwww)
           {
               if (((XmlElement)ccd).GetAttribute("id") == _id)
               {
                   html = ccd.InnerXml.ToString();
                   break;
               }
           }
           return html;
       }
       public string getcontext_comm()
       {
           string html = string.Empty;
           XmlNode ccwww = root_style.SelectSingleNode(cc);
           html = ccwww.InnerXml.ToString();       
           return html;
       }
    }
}
