using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApplication1.Refer;

namespace WpfApplication1.htmlcss
{
   public class htmlstyle
    {
       public static string write_secid(string secid,string nodetype)
       {
           string html_secid;
           switch (nodetype)
           { 
               case "Chapter":
                   html_secid = "<papersectionid><span style=\"margin-top:10.0pt;margin-right:0cm;margin-bottom:10.0pt;margin-left:0cm;text-align:center;text-indent:0cm;line-height:20.0pt;page-break-after:avoid;font-size:14.0pt;font-family:Times New Roman;font-weight:normal;\">" + secid + "</span></papersectionid>";
                   break;
               case "Section1":
                   html_secid = "<span style=\"margin-top:1.25pt;margin-bottom:15.6pt;margin-left:1cm;text-align:justify;text-justify:inter-ideograph;line-height:125%;mso-pagination:lines-together;page-break-after:avoid;mso-outline-level:2;font-size:14.0pt;mso-bidi-font-size:16.0pt;font-family:Times New Roman;\">"+secid+"</span></papersectionid>";
                   break;
               default:
                   html_secid = "<papersectionid><span style=\"margin-top:15.6pt;margin-bottom:1.25pt;margin-left:1cm;text-align:justify;text-justify:inter-ideograph;line-height:125%;page-break-after:avoid;font-size:12.0pt;font-family:Times New Roman;\">"+secid+"</span></papersectionid>";
                   break;
           }
           
           //string html_title = "<papersectiontitle><span style=\"margin-top:10.0pt;margin-right:0cm;margin-bottom:10.0pt;margin-left:0cm;text-align:center;text-indent:0cm;line-height:20.0pt;page-break-after:avoid;font-size:14.0pt;font-family:黑体;font-weight:normal;\">"+title+"</span></papersectiontitle>";
           return html_secid;
       }
       public static string wreit_title(string title,string nodetype)
       {
           string html_title;
           switch (nodetype)
           {
               case "Chapter":
                   html_title = "<papersectiontitle><span style=\"margin-top:10.0pt;margin-right:0cm;margin-bottom:10.0pt;margin-left:0cm;text-align:center;text-indent:0cm;line-height:20.0pt;page-break-after:avoid;font-size:14.0pt;font-family:黑体;font-weight:normal;\">" + title + "</span></papersectiontitle>";
                   break;
               case "Section1":
                   html_title = "<papersectiontitle><span style=\"margin-top:1.25pt;margin-right:1cm;margin-bottom:15.6pt;text-align:justify;text-justify:inter-ideograph;line-height:125%;mso-pagination:lines-together;page-break-after:avoid;mso-outline-level:2;font-size:14.0pt;mso-bidi-font-size:16.0pt;font-family:黑体;\">"+title+"</span></papersectiontitle>";
                   break;
               default:
                   html_title = "<papersectiontitle><span style=\"margin-top:15.6pt;margin-bottom:1.25pt;text-align:justify;text-justify:inter-ideograph;line-height:125%;page-break-after:avoid;font-size:12.0pt;font-family:黑体;\"> "+title+"</span> </papersectiontitle>";
                   break;
           }
           return html_title;

       }
       public static string insert_msocaption(string number, string name,string type)
       {
           string htmlcode=string.Empty;
           if(type=="image")
           htmlcode = "<p name='msocaption' style='font-size=10.0pt' align=center style='text-align:center'><head><span style='font-family:黑体;'></span></head><span style='font-family:黑体;'>"+name+"</span></p>";
           else
           htmlcode = "<p name='msocaption' style='font-size=10.0pt' align=center style='text-align:center'><head><span style='font-family:黑体;'></span></head><span style='font-family:黑体;'>werwewe</span></p>";
           return htmlcode;
       }
       private static string PadRightEx(string str, int totalByteCount, char c)
       {
           Encoding coding = Encoding.GetEncoding("gb2312");
           int dcount = 0;
           foreach (char ch in str.ToCharArray())
           {
               if (coding.GetByteCount(ch.ToString()) == 2)
                   dcount++;
           }
           string w = str.PadRight(totalByteCount - dcount*2, c);
           return w;
       }
       public static string get_CatalogOutlineString(outline cc)
       {
           string html_filePath = "section"+cc.secid+".html";
           string htmlcode=string.Empty;
           switch (cc.type)
               { 
                   case outlinetype.Section1:
                       htmlcode = "<CatalogSection Level=\"2\"><p onclick='window.external.MN_opensection(\"" + cc.secid + "\")' html_filePath=\""+html_filePath+"\" style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;margin-left:10.0pt;margin-bottom:.0001pt;text-align:justify;text-justify:inter-ideograph;line-height:20.0pt;font-size:12.0pt;font-family:宋体;cursor:pointer;'>" +
                               "" + PadRightEx(cc.secid + "  " + cc.Name1, 70, '.') + "" +
                               "</p></CatalogSection>";
                   break;
               case outlinetype.Section2:
                   htmlcode = "<CatalogSection Level=\"3\"><p onclick='window.external.MN_opensection(\"" + cc.secid + "\")' html_filePath=\"" + html_filePath + "\" style='margin-top:0cm;margin-right:0cm;margin-bottom:0cm;margin-left:20.0pt;margin-bottom:.0001pt;text-align:justify;text-justify:inter-ideograph;line-height:20.0pt;font-size:12.0pt;font-family:宋体;cursor:pointer;'>" +
                           ""+PadRightEx(cc.secid+"  "+cc.Name1, 72,'.')+""+ 
                           "</p></CatalogSection>";
                   break;
               case outlinetype.Chapter:
                   htmlcode = "<CatalogSection Level=\"1\"><p onclick='window.external.MN_opensection(\"" + cc.secid + "\")' html_filePath=\"" + html_filePath + "\" style='margin:0cm;margin-bottom:.0001pt;text-align:justify;text-justify:inter-ideograph;line-height:20.0pt;font-size:12.0pt;font-family:宋体;cursor:pointer;'>" +
                           ""+PadRightEx(cc.secid+"  "+cc.Name1,74,'.')+""+ 
                           "</p></CatalogSection>";
                   break;
               default:
                   html_filePath =cc.nodename+".html";
                   htmlcode = "<CatalogSection Level=\"1\"><p onclick='window.external.MN_opensection(\"" + cc.nodename + "\")' html_filePath=\"" + html_filePath + "\" style='margin:0cm;margin-bottom:.0001pt;text-align:justify;text-justify:inter-ideograph;line-height:20.0pt;font-size:12.0pt;font-family:宋体;cursor:pointer;'>" +
                           ""+PadRightEx(cc.Name1, 74,'.')+""+ 
                           "</p></CatalogSection>";
                   break;
               }
           return htmlcode;
       }
       public static string write_CatalogOutline(outline ch)
       {
           string htmlcode = string.Empty;
           htmlcode = get_CatalogOutlineString(ch);
           if (ch.children.Count == 0)
               return htmlcode;
           else
           {
               foreach (outline ca in ch.children)
               {
                    htmlcode=htmlcode+write_CatalogOutline(ca);
               }
               return htmlcode;
           }
       }
       public static string wirite_Catalog(string type)
       {   

           string htmlcode="<div style='width:600px;margin-left:140px;'>";
           IEnumerable<List<Picture_ChartInfo>> result = null;
           switch(type)
           {
               case "1":
           //MainWindow.tree5_sel.outlines.Select
                   //MainWindow.tree5_sel.outlines.Select
                   htmlcode += "<CatalogName>"+
                  "<h1 align=\"center\" style=\"margin-top:3.9pt;margin-right:0cm;margin-bottom:3.9pt;margin-left:0cm;text-align:center;text-indent:.05pt\">" +
	              "<span style=\"font-family:黑体;\">目录</span></h1>"+
	              "</CatalogName>";
                   foreach(outline ch in MainWindow.tree5_sel.outlines)
                   {
                       htmlcode += write_CatalogOutline(ch);
                   }
           break;
               case "2":
           result = MainWindow.tree5_sel.outlines.Select((x) => x.CatalogFig);

           break;
               case "3":
           htmlcode += "<CatalogName>" +
           "<h1 align=\"center\" style=\"margin-top:3.9pt;margin-right:0cm;margin-bottom:3.9pt;margin-left:0cm;text-align:center;text-indent:.05pt\">" +
           "<span style='font-family:黑体;'>图目录</span></h1>" +
           "</CatalogName><catalogsection>";
                   result = MainWindow.tree5_sel.outlines.Select((x)=>x.CatalogFig);
                   foreach (List<Picture_ChartInfo> cc in result)
               {
               if (cc != null)
               {   
                   foreach (Picture_ChartInfo i in cc)
                   {
                       htmlcode = htmlcode + "<p style=\"cursor:pointer;\"><a image_path='"+i.path+"' ondblclick='window.external.Mn_OpenimageInwindow(\"" + i.path + "\")'>" + PadRightEx(i.title, 65, '-') + "</a><button onclick='window.external.MN_opensection(\"" + i.ownsection + "\")'>" + i.ownsection + "</button></p>";
                       //Console.WriteLine(i.title.PadRight(20, '-'));
                   }
               }
           }
                   htmlcode += "</catalogsection>";
           break;
       }
           MainWindow.tree6_sel.context = htmlcode + "</div>";
           return htmlcode+"</div>";
       
       }
       public static string writeRefer(List<int> number,List<ReferencesInfo> context)
       {
           string htmlcode = string.Empty;
          // string cc = string.Empty;
           string title = "<refertitle><h1 style=\"text-align:center\"><span style=\"font-family:黑体;text-align:center\">参考文献</span></h1></refertitle>";
           for(int i=0;i<number.Count;i++)
           {
               htmlcode = htmlcode + "<refermeta><p style=\"margin-right:1cm;margin-bottom:8pt;margin-top:8pt;margin-left:1cm;line-height:125%;\">"+
               "<refernumber><span style=\"font-size:10.5pt;font-family:Times New Roman;\">["+(i+1)+"] </span>"+
               "</refernumber><refertext><span style=\"font-family:Times New Roman;\">"+context[number[i]-1].Context+"</span></refertext>"+
			   "</p></refermeta>";
           }
           return title+htmlcode;
           
       }

    }
}
