using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace WpfApplication1
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)] 
    public class JSEvent
    {
        public string MN_OpenFile(string filter)
        {
            string filePath = "";
            OpenFileDialog openFileDialog1=new OpenFileDialog();
            openFileDialog1.Multiselect=false;
            openFileDialog1.Filter = filter;//"mp4文件|*.mp4";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = MainWindow.tree5_sel.href + "\\dialogs\\video\\" + openFileDialog1.SafeFileName;
                File.Copy(openFileDialog1.FileName, filePath, true);
            }
            return filePath;
        }
        public string MN_OpenPic(string filter)
        {
            string filePath = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = filter;//图片格式

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = MainWindow.tree5_sel.href + "\\picture/" + openFileDialog1.SafeFileName;
                File.Copy(openFileDialog1.FileName, filePath, true);
                //System.Drawing.Bitmap pic = new System.Drawing.Bitmap(filePath);
                
            }
            return filePath;
        }
    }
}
