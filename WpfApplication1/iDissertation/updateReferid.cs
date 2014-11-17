using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
   public class updateReferid
    {   
       private static int referStartNumber;
       private List<int> cd=new List<int>();
       public updateReferid()
       {
           referStartNumber = 0;
           if (MainWindow.tree5_sel.Refernumber.Count == 0)
               cd.Add(0);
           else
               cd = MainWindow.tree5_sel.Refernumber;
           MainWindow.tree5_sel.Refernumber = new List<int>();
       }
       public  void updateReferidS(ObservableCollection<outline> cc)
       {
           for (int i = 8; i < cc.Count; i++)
           {
               if (cc[i].type != outlinetype.common)
               {
                   refer_number_updata(cc[i]);
               }
               else
                   break;
           
           }
       }
       public  void refer_number_updata(outline charp)
       {
           if (charp.children.Count == 0)
           {
               update(charp);
           }
           else
           {
               update(charp);
               foreach (outline a in charp.children)
               {
                   refer_number_updata(a);
               }
           }
       }
       public  void update(outline ch)
       {
           
           XmlDocument doc = new XmlDocument();
           if (ch.context == null)
           {
               article dd = new article(ch.secid, "Papersection/" + ch.nodename);
               ch.context = dd.getcontext();
               doc.LoadXml(ch.context);
           }
           else
           {
               doc.LoadXml(ch.context.Replace("&", "&amp;"));
           }
          XmlNode xml = doc.DocumentElement.FirstChild;
          do
          {
              XmlNodeList sups = xml.SelectNodes("span/span/sup");
              if (sups.Count > 0)
              {
                  foreach (XmlNode cc in sups)
                  {   
                      if (cc.Attributes.Count != 0)
                      {   
                          referStartNumber++;
                          //refer_context=MainWindow.tree5_sel.Refer[MainWindow.tree5_sel.Refernumber[referStartNumber - 1] - 1].Context;
                          MainWindow.tree5_sel.Refernumber.Add(cd[int.Parse(((XmlElement)cc).GetAttribute("id"))-1]);
                          ((XmlElement)cc).SetAttribute("id",referStartNumber.ToString());
                          ((XmlElement)cc).SetAttribute("onClick", "window.external.MN_showreferinfo(\"" + referStartNumber.ToString() + "\")");
                          cc.InnerText = "[" + referStartNumber + "]";
                      }
                  }
              }
              xml=xml.NextSibling;
          } while (xml != null);
          ch.context = doc.InnerXml.Replace("&amp;", "&");
       }
    }
}
