using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WpfApplication1
{
    class pictureandchart_title
    {
       // private int count;
        public outline chapter;
        private  outline getchapter(outline sel)
        {
            if (sel.type==outlinetype.Chapter)
                return sel;
            else
                return getchapter(sel.parent);
        }
        public pictureandchart_title(outline sel)
        {
            chapter = getchapter(sel);
        }
        public void updatetitle(outline ch,int cou)
        {
            XmlDocument doc = new XmlDocument();
            if (ch.context == null)
            {
                article dd = new article(ch.secid,"Papersection/" + ch.nodename);
                ch.context = dd.getcontext();
                doc.LoadXml(ch.context);
            }
            else
            {
                doc.LoadXml(ch.context.Replace("&", "&amp;"));
            }
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(ch.context);
            XmlNodeList xml = doc.DocumentElement.SelectNodes("p");
            foreach (XmlNode xm in xml)
            {
                if (((XmlElement)xm).GetAttribute("name") == "msocaption")
                {
                    cou++;
                    ((XmlElement)xm).SetAttribute("id",cou.ToString());

                    xm.FirstChild.InnerText = "图" + chapter.secid.ToString() + "-" + cou.ToString() + " ";
                    
                }
            }
            ch.context = doc.InnerXml.Replace("&amp;", "&");
            foreach (outline ou in ch.children)
            {
                updatetitle(ou, cou);
            }
        }
    }
}
