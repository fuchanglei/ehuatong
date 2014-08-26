using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WpfApplication1
{
   public class Savecontext
    {
       private string doc;
       private  XmlDocument doc_tem;
       private  XmlNode root_tem;
       public Savecontext()
       {
           doc = MainWindow.idd_href + "\\webTemplate.xml";
           doc_tem = new XmlDocument();
           doc_tem.Load(doc);
           root_tem = doc_tem.DocumentElement;

       }
       public void save(XmlNode xe)
       { 
         
       }
       
       
    }
}
