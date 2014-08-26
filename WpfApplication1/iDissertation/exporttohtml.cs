using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace WpfApplication1
{
   public class exporttohtml
    {
       private static string xml_style;
       static exporttohtml()
       {
           xml_style = MainWindow.idd_href + "\\idis.xml";
       }
       public static void export(iDissertation tree5_sel,string todire)
       {
           FileStream fi = new FileStream(MainWindow.idd_href + "\\" + tree5_sel.Name + ".html", FileMode.Create);
           StreamWriter sw = new StreamWriter(fi, Encoding.Default);
           sw.WriteLine("<!doctype html>");
           sw.WriteLine("<html lang=\"en\">");
           sw.WriteLine("<head>");
           sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"/>");
           sw.WriteLine("<title>" + tree5_sel.Name + "</title>");
           sw.WriteLine("</head>");
           sw.WriteLine("<body>");
           XmlDocument ep = new XmlDocument();
           ep.Load(xml_style);
           XmlNodeList context = ep.DocumentElement.ChildNodes;
           foreach (XmlNode xm in context)
           {
               
               string html = ((XmlElement)xm).InnerXml.ToString().Replace(tree5_sel.href+"\\", "");
               html = html.Replace("dialogs\\video\\","");
               sw.WriteLine(html);
           }
           sw.WriteLine("</body>");
           sw.WriteLine("</html>");
           sw.Flush();
           sw.Close();
           fi.Close();
           copy_files.copyfile(tree5_sel.href,todire);
           
       }
    }
    
}
