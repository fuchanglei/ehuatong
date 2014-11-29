using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WpfApplication1
{
    class pictureandchart_title
    {
        private int cou=0;
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
            chapter.CatalogFig=new List<Picture_ChartInfo>();
        }
        public void updatetitle(outline ch)
        {
            try
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
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(ch.context);
                XmlNodeList xml = doc.DocumentElement.SelectNodes("p");
                foreach (XmlNode xm in xml)
                {
                    if (((XmlElement)xm).GetAttribute("name") == "msocaption")
                    {
                        cou++;
                        Picture_ChartInfo newone = null;
                        ((XmlElement)xm).SetAttribute("id", cou.ToString());
                        xm.FirstChild.InnerText = "图" + chapter.secid.ToString() + "-" + cou.ToString() + " ";
                        XmlNode xms = xm.PreviousSibling.FirstChild.FirstChild.SelectSingleNode("img");
                        XmlNode xmt = xm.PreviousSibling.FirstChild.FirstChild;
                        if (xms == null && xmt == null)
                        {
                            newone = new Picture_ChartInfo()
                            {
                                ownsection = ch.secid,
                                title = xm.InnerText,
                                path = ""
                            };
                        }
                        else
                        {
                          xms=(xms == null?xmt:xms);
                            newone = new Picture_ChartInfo()
                            {
                                ownsection = ch.secid,
                                title = xm.InnerText,
                                path = ((XmlElement)xms).GetAttribute("src")
                            };
                        }
                        chapter.CatalogFig.Add(newone);
                    }
                }
                ch.context = doc.InnerXml.Replace("&amp;", "&");
                foreach (outline ou in ch.children)
                {
                    updatetitle(ou);
                }
            }
            catch
            {
                ;
            }
            //    ;
        }
     
    
    }
}
