using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
           string htmlcode = "<p name='msocaption' style='font-size=10.0pt' align=center style='text-align:center'><head><span style='font-family:黑体;'></span></head><span style='font-family:黑体;'>关联数据相关文献检索情况</span></p>";
           return htmlcode;
       }

    }
}
