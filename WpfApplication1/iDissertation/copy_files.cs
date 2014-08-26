using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WpfApplication1
{
   public class copy_files
    {
       private string _from;
       private string _to;
       public copy_files(string from,string to)
       {
           _from = from;
           _to = to;
       }
       public static void copyfile(string from,string to)
       {
           DirectoryInfo dirinfo = new DirectoryInfo(from);
           foreach (FileSystemInfo fi in dirinfo.GetFileSystemInfos())
           {
               
               if (fi is DirectoryInfo)
               {
                   string currentdir = to + "\\" + Path.GetFileName(fi.Name);
                   Directory.CreateDirectory(currentdir);
                   copyfile(fi.FullName,currentdir);
               }
               else
               File.Copy(fi.FullName,to+"\\"+fi.Name,true);
           }
       }
       public static void DeleteDir(string aimPath)
       {
           try
           {
               // 检查目标目录是否以目录分割字符结束如果不是则添加之
               if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                   aimPath += Path.DirectorySeparatorChar;
               // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
               // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
               // string[] fileList = Directory.GetFiles(aimPath);
               string[] fileList = Directory.GetFileSystemEntries(aimPath);
               // 遍历所有的文件和目录
               foreach (string file in fileList)
               {
                   // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件
                   if (Directory.Exists(file))
                   {
                       DeleteDir(aimPath + Path.GetFileName(file));
                   }
                   // 否则直接Delete文件
                   else
                   {
                       File.Delete(aimPath + Path.GetFileName(file));
                   }
               }
               //删除文件夹
               System.IO.Directory.Delete(aimPath,true);
           }
           catch
           {
              ;
           }

       }
    }
}
