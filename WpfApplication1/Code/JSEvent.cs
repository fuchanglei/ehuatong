using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.ObjectModel;
using WpfApplication1.htmlcss;
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
            openFileDialog1.InitialDirectory = MainWindow.tree5_sel.mediaPath;
            openFileDialog1.Filter = filter;//"mp4文件|*.mp4";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {   

                filePath = MainWindow.tree5_sel.href + "\\dialogs\\video\\" + openFileDialog1.SafeFileName;
                File.Copy(openFileDialog1.FileName, filePath, true);
                title newmedia = new title()
                {
                    date = DateTime.Now.ToString(),
                    title_name = Path.GetFileName(filePath),
                    context = filePath
                };
                MainWindow.tree5_sel.media.Add(newmedia);
                if (File.Exists(MainWindow.tree5_sel.mediaPath + "\\" + openFileDialog1.SafeFileName)==false)
                {
                    ThreadPool.QueueUserWorkItem(status => copy_files.AddiDissertationMedia(openFileDialog1.FileName, MainWindow.tree5_sel.mediaPath + "\\" + openFileDialog1.SafeFileName));
                }
            }
            return filePath;
        }
        public outline getsecid(ObservableCollection<outline> dd,ArrayList secid)
        {
            int i = int.Parse(secid[0].ToString());
            if (secid.Count == 1)
                return dd[i-1];
            else
            {
                secid.RemoveAt(0);
                return getsecid(dd[i-1].children,secid);
                
            }
        }
        public void MN_opensection(string secid)
        {
            string[] cc = secid.Split('.');
            ArrayList dd=new ArrayList();
            
            for(int i = 0; i < cc.Length; i++)
            {
                dd.Add(cc[i]);
            }
            dd[0] = int.Parse(cc[0]) + 8;
            getsecid(MainWindow.tree5_sel.outlines, dd).isselected = true;
           // MainWindow.js_getdata();
        }
        public void Mn_OpenimageInwindow(string path)
        {
            System.Diagnostics.Process.Start(path); //打开此文件。
        }
        public string MN_OpenPic(string filter)
        {  
            insertPic aa = new insertPic(filter);
            aa.ShowDialog();
            
           // aa.insertpic = new insertPic.myevent();
            return aa.reslut;
        }
        public void MN_updateMsoCaption_id()
        {
           string cc= MainWindow.invoker.InvokeScript("getContent").ToString();
           MainWindow.tree6_sel.context = cc;
           pictureandchart_title pt = new pictureandchart_title(MainWindow.tree6_sel);
           pt.updatetitle(pt.chapter);
           MainWindow.invoker.InvokeScript("setContent", MainWindow.tree6_sel.context);
        }

           // invoker = new WebbrowserScriptInvoker();
        
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
        public string MN_insertMsoCaption(string tile,string type)
        {
            return htmlstyle.insert_msocaption(tile, tile, type);
        }
        public MusicFile MN_OpenMusicFile()
        {
            string filePath = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = MainWindow.tree5_sel.mediaPath;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "mp3文件|*.mp3";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                Mp3Info info = Mp3FileInfo.GetMp3FileInfo(filePath);
                title newmedia = new title()
                {
                    date = DateTime.Now.ToString(),
                    title_name = info.Title,
                    context = filePath
                };
                MainWindow.tree5_sel.media.Add(newmedia);
                if (File.Exists(MainWindow.tree5_sel.mediaPath + "\\" + openFileDialog1.SafeFileName) == false)
                {
                    ThreadPool.QueueUserWorkItem(status => copy_files.AddiDissertationMedia(openFileDialog1.FileName, MainWindow.tree5_sel.mediaPath + "\\" + openFileDialog1.SafeFileName));
                }
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
        public int MN_InsertReferences()
        {
            References aa = new References();
            aa.ShowDialog();
            return aa.result;

        }
        public void MN_updaterefer_id()
        {
            string cc = MainWindow.invoker.InvokeScript("getContent").ToString();
            MainWindow.tree6_sel.context = cc;
            updateReferid update = new updateReferid();
            update.updateReferidS(MainWindow.tree5_sel.outlines);
            MainWindow.invoker.InvokeScript("setContent", MainWindow.tree6_sel.context);
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
