using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
   public class template
    {
       private static template _getTemplate = new template();
       public static template getTemplate
       {
           get
           {
               return _getTemplate;
           }
       
       }
       private List<string> getTemplatedata()
       {
           List<string> cc = new List<string>();
           cc.Add("中国科学院");
           cc.Add("北京大学");
           cc.Add("南京大学");
           cc.Add("清华大学");
           return cc;
       }
       public List<string> mTemplate
       {
           get
           {
               if (_mTemplate == null)
                   _mTemplate = getTemplatedata();
                   return _mTemplate;
               
           }
       }
       private List<string> _mTemplate = null;
    }
}
