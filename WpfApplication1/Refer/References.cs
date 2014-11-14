using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.Refer
{
    public class ReferencesType
    {
        public string Type { get; set; }
        public string Suoxie { get; set; }
    }
    public class ReferenceList
    {
        public string num { get; set; }
        public string context { get; set; }
    }
   public class ReferencesInfo
   {
       private ReferencesType _Type;
       public ReferencesType Type
       {
           get { return _Type; }
           set { _Type = value; }
       }
       public string Author { get; set; }
       public string Title { get; set; }
       public string Year { get; set; }
       public string Address { get; set; }
       public string Publishers { get; set; }
       public string Journal_qikantitle { get; set; }
       public string Month { get; set; }
       public string Page { get; set; }
       public string Context { get; set; }
       private static ReferencesInfo _Instance=new ReferencesInfo(); 
       public static ReferencesInfo Instance
       {
           get { return _Instance; }
            }
       public List<ReferencesType> initReferencesType()
       {
           List<ReferencesType> type = new List<ReferencesType>();
           ReferencesType book = new ReferencesType()
           {
                Type="图书",
                Suoxie="[M]"
           };
           ReferencesType journal = new ReferencesType()
           {
               Type = "期刊论文",
               Suoxie = "[J]"
           };
           type.Add(book);
           type.Add(journal);
           return type;
       }
       public List<ReferencesType> ReferencesTypes
       {
           get
           {
               if (_ReferencesTypes == null)
                   _ReferencesTypes = initReferencesType();
               return _ReferencesTypes;
           }
       }
       private List<ReferencesType> _ReferencesTypes = null;
   }
   public class References
   {
   }
}
