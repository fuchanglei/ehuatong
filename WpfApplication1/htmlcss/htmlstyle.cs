using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApplication1.Refer;

namespace WpfApplication1.htmlcss
{
   public class htmlstyle
    {
       public static string writeChart(string secid,string title)
       {
           string html = "";
           return html;
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
       public static string wirite_Catalog(string type)
       {   

           string htmlcode=string.Empty;
           IEnumerable<List<Picture_ChartInfo>> result = null;
           switch(type)
           {
               case "1":
           //MainWindow.tree5_sel.outlines.Select
           result = MainWindow.tree5_sel.outlines.Select((x)=>x.CatalogFig);
           break;
               case "2":
           result = MainWindow.tree5_sel.outlines.Select((x) => x.CatalogFig);
           break;
               case "3":
                    result = MainWindow.tree5_sel.outlines.Select((x)=>x.CatalogFig);
           break;
       }
           foreach (List<Picture_ChartInfo> cc in result)
           {
               if (cc != null)
               {
                   foreach (Picture_ChartInfo i in cc)
                   {
                       htmlcode = htmlcode + "<p><a ondblclick='window.external.Mn_OpenimageInwindow(\"" + i.path + "\")'>" + PadRightEx(i.title, 20, '-') + "</a><button onclick='window.external.MN_opensection(\"" + i.ownsection + "\")'>" + i.ownsection + "</button></a></p>";
                       //Console.WriteLine(i.title.PadRight(20, '-'));
                   }
               }
           }
           return htmlcode;
       
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
