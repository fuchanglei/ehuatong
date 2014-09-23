using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;
using WpfApplication1.htmlcss;

namespace WpfApplication1
{
   public class exporttohtml
    {
       private static string xml_context;
       private static XmlDocument doc_contex;
       private static XmlNode root_contex;
       private static string xml_webcon;
       private static StreamWriter sws;
       static exporttohtml()
       {
           xml_context = MainWindow.idd_href + "\\idis.xml";
           xml_webcon = MainWindow.idd_href + "\\webTemplate.xml";
           doc_contex = new XmlDocument();
           doc_contex.Load(xml_context);
           root_contex = doc_contex.DocumentElement;
       }
       private static void createhtml(outline outline,string dir)
       {
           FileStream fi;
           StreamWriter sw;
           if (outline.children.Count==0)
           {
               
               if (outline.type==outlinetype.common)
               {
                   article dd = new article(outline.nodename);
                   //FileStream fi = new FileStream(dir + "\\" + "section" + ((XmlElement)root).GetAttribute("id") + ".html", FileMode.Create);
                   fi = new FileStream(dir + "\\" + outline.nodename+".html", FileMode.Create);
                   sw = new StreamWriter(fi, Encoding.Default);
                   sw.WriteLine("<!doctype html>");
                   sw.WriteLine("<html lang=\"en\">");
                   sw.WriteLine("<head>");
                   sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"/>");
                   //sw.WriteLine("<title>" + tree5_sel.Name + "</title>");
                   sw.WriteLine("</head>");
                   sw.WriteLine("<body>");
                   sw.WriteLine(dd.getcontext_comm());
                   sw.WriteLine("</body>");
                   sw.WriteLine("</html>");
                   sw.Flush();
                   sw.Close();
                   fi.Close();
                   //create_leftnode(root_body,doc,outl.href,outl.Name1,outl.type);
                   create_leftnode(outline,sws);
               }
               else
               {
                   article dd = new article(outline.secid, "Papersection/" +outline.nodename);
                   fi = new FileStream(dir + "\\" +outline.nodename + outline.secid + ".html", FileMode.Create);
                   sw = new StreamWriter(fi, Encoding.Default);
                   sw.WriteLine("<!doctype html>");
                   sw.WriteLine("<html lang=\"en\">");
                   sw.WriteLine("<head>");
                   sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"/>");
                   //sw.WriteLine("<title>" + tree5_sel.Name + "</title>");
                   sw.WriteLine("</head>");
                   sw.WriteLine("<body>");
                   sw.WriteLine(htmlstyle.write_secid(outline.secid, outline.nodename));
                   sw.WriteLine(htmlstyle.wreit_title(outline.Name1, outline.nodename));
                   sw.WriteLine(gethtmlcode(dd.getcontext()));
                   sw.WriteLine("</body>");
                   sw.WriteLine("</html>");
                   sw.Flush();
                   sw.Close();
                   fi.Close();
                   create_leftnode(outline,sws);

               }
           }
           else
           {
               article dd = new article(outline.secid, "Papersection/" + outline.nodename);
               fi = new FileStream(dir + "\\" + outline.nodename + outline.secid + ".html", FileMode.Create);
               sw = new StreamWriter(fi, Encoding.Default);
               sw.WriteLine("<!doctype html>");
               sw.WriteLine("<html lang=\"en\">");
               sw.WriteLine("<head>");
               sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"/>");
               //sw.WriteLine("<title>" + tree5_sel.Name + "</title>");
               sw.WriteLine("</head>");
               sw.WriteLine("<body>");
               sw.WriteLine(htmlstyle.write_secid(outline.secid, outline.nodename));
               sw.WriteLine(htmlstyle.wreit_title(outline.Name1, outline.nodename));
               sw.WriteLine(gethtmlcode(dd.getcontext()));
               sw.WriteLine("</body>");
               sw.WriteLine("</html>");
               sw.Flush();
               sw.Close();
               fi.Close();
               create_leftnode(outline, sws);
               foreach (outline xm in outline.children)
               {
                   createhtml(xm, dir);
               }
               
           }
       }
       private static string gethtmlcode(string xmlcode)
       {
           string html = xmlcode.Replace(MainWindow.idd_href + "\\", "");
           html = html.Replace("dialogs\\video\\", "");
           html = html.Replace("&amp;", "&");
           return html;
       }
       private static void create_leftnode(outline outl,StreamWriter sw)
       {
           string htmlname;
               if (outl.type == outlinetype.empty || outl.type == outlinetype.common)
               {
                    htmlname = outl.nodename + ".html";
               }
               else
               {
                   htmlname = outl.nodename + outl.secid + ".html";
               }
               //XmlElement xe1 = doc_outline.CreateElement("li");
               //xe1.SetAttribute("id", "section" + outl.secid);
               string html = string.Empty;
               if (outl.type == outlinetype.Section1)
               {
                   html = "<li id=\"" + htmlname + "\" style=\"margin-left:0.3cm;\">";
                  // xe1.SetAttribute("style", "margin-left:0.3cm;");
               }
               else if (outl.type == outlinetype.Section2)
               {
                  // xe1.SetAttribute("style", "margin-left:0.4cm;");
                   html = "<li id=\"" + htmlname + "\" style=\"margin-left:0.6cm;\">";
               }
               else
                   html = "<li id=\"" + htmlname + "\">";
               //XmlElement xe2 = doc.CreateElement("a");
               //xe2.SetAttribute("href","../center/"+htmlname);
               // xe2.InnerText=
              string html_result = html+"<a href=\"../center/" + htmlname + "\">" + outl.Name1 + "</a></li>";
              sw.WriteLine(html_result);

        

               


          
           
       }
       public static void export(iDissertation tree5_sel,string todire,ObservableCollection<outline> outlines)
       {  
           string title_c,title_e;
           /* FileStream fi = new FileStream(MainWindow.idd_href + "\\" + tree5_sel.Name + ".html", FileMode.Create);
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
                html = html.Replace("&amp;","&");
                sw.WriteLine(html);
            }
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
            sw.Flush();
            sw.Close();
            * left_Paper.htm
            fi.Close();*/
           XmlDocument doc = new XmlDocument();
           doc.Load(xml_webcon);
           XmlNode coverC = doc.DocumentElement.SelectSingleNode("cover").SelectSingleNode("ArticleTitle");
           title_c = coverC.InnerText;
           coverC = doc.DocumentElement.SelectSingleNode("coverE").SelectSingleNode("ArticleTitleE");
           title_e = coverC.InnerText;
           File.Copy("index_Paper.htm", todire + "\\index_Paper.htm", true);
           Directory.CreateDirectory(todire + "\\center");
           copy_files.copyfile(tree5_sel.href, todire + "\\center");
           // Directory.CreateDirectory(newtitle.href + "\\video");
           Directory.CreateDirectory(todire + "\\head");
           File.Copy("head_Paper.htm", todire + "\\head\\head_Paper.htm", true);
           sws = new StreamWriter(todire + "\\head\\head_Paper.htm", true, Encoding.GetEncoding("gb2312"));
           sws.WriteLine("<p align=\"left\">"+title_e+"</p>");
           sws.WriteLine("<p>"+title_c+"</p>");
           sws.WriteLine("</body>");
           sws.WriteLine("</html>");
           sws.Flush();
           sws.Close();
           Directory.CreateDirectory(todire + "\\images");
           Directory.CreateDirectory(todire + "\\left");
           File.Copy("left_Paper.htm", todire + "\\left\\left_Paper.htm", true);
           sws = new StreamWriter(todire + "\\left\\left_Paper.htm", true, Encoding.GetEncoding("utf-8"));
           copy_files.copyfile(tree5_sel.href, todire + "\\center");
           foreach(outline outl in outlines)
           {
               if (outl.type == outlinetype.empty)
               {
                   //create_leftnode(root_body,doc,outl.href,outl.Name1,outl.type);
                   create_leftnode(outl,sws);
               }
               else if (outl.type == outlinetype.common)
               {
                   if (outl.nodename != "Catalog")
                   {
                       createhtml(outl, todire + "\\center");
                      // create_leftnode(outl);

                   }
                   
                      
               }
               else
               {
                   createhtml(outl, todire + "\\center");
               }
           }
           sws.WriteLine("</ul>");
           sws.WriteLine("<p><script type=\"text/javascript\">initiate();</script></p>");
           sws.WriteLine("</body>");
           sws.WriteLine("</html>");
           sws.Flush();
           sws.Close();
           //doc_outline.Save(todire + "\\left\\index_Paper.htm");
           //createhtml(outline, todire + "\\center");

           
       }
    }
    
}
