using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
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
        public string MN_InsertChart()
        {
            data cc = new data();
            cc.ShowDialog();
            return cc.result;
        }
        public string MN_InsertAlbum()
        {
            string uuid = "images";
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "jpg文件|*.jpg";
            string srcFilepath = "";
            string dstFilePath = "";
            string fileNames = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               
                    string rootpath = MainWindow.tree5_sel.href + "\\dialogs\\Album\\" + uuid + "\\";
                    //Code.IOManage.FileIO.FileHelper.DefaultHelper.Mkdir(rootpath);
                    for (int i = 0; i < openFileDialog1.FileNames.Length;i++ )
                    {
                        srcFilepath = openFileDialog1.FileNames[i];
                        dstFilePath = rootpath + openFileDialog1.SafeFileNames[i];
                        File.Copy(srcFilepath, dstFilePath, true);
                        fileNames += openFileDialog1.SafeFileNames[i] + ",";
                    }

                    return MainWindow.tree5_sel.href + @"\dialogs\Album\index.html#UUID=" + uuid + "&imgs=" + fileNames.Substring(0, fileNames.Length - 1);
                
               
            }
            return @"dialogs\Album\index.html";
        }

        public MusicFile MN_OpenMusicFile()
        {
            // string filepath = MN_OpenPic("mp3文件|*.mp3");

            string filePath = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "mp3文件|*.mp3";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                Mp3Info info = Mp3FileInfo.GetMp3FileInfo(filePath);

                if (true)//MainWindow.CurIsIdissertation)
                {
                    filePath = MainWindow.tree5_sel.href + "\\dialogs\\music\\" + openFileDialog1.SafeFileName;
                    File.Copy(openFileDialog1.FileName, filePath, true);
                }
                else
                {
                    filePath = openFileDialog1.FileName;
                }
                MusicFile mf = new MusicFile()
                {
                    FileName = info.Title,
                    FilePath = filePath,
                    AlbumTitle = info.Album,
                    Author = info.Artist
                };
                return mf;
            }
            return null;
        }

       
    }
[System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public class MusicFile
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public string AlbumTitle { get; set; }
            public string Author { get; set; }
        }
}
