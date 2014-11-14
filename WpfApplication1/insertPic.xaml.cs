using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// insertPic.xaml 的交互逻辑
    /// </summary>
    public partial class insertPic : Window
    {
        private string filter;
        public string reslut;
        private string pic_title;
        private bool issave=true;
        public insertPic(string fi)
        {
            InitializeComponent();
            filter = fi;
        }
        private string filePath;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = filter;//图片格式
            openFileDialog1.InitialDirectory = MainWindow.tree5_sel.mediaPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = MainWindow.tree5_sel.href + "\\picture\\" + openFileDialog1.SafeFileName;

                if (File.Exists(MainWindow.tree5_sel.mediaPath + "\\" + openFileDialog1.SafeFileName))
                    issave = false;
                else
                {
                    
                    File.Copy(openFileDialog1.FileName, MainWindow.tree5_sel.mediaPath + "\\" + openFileDialog1.SafeFileName, true);
                    issave = true;
                }
                File.Copy(openFileDialog1.FileName, filePath, true);
                this.image1.Source =new BitmapImage(new Uri(openFileDialog1.FileName));
            }
            else
                filePath = string.Empty;
            fileath.Text = openFileDialog1.FileName;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          //System.Windows.MessageBox.Show(this.selctpic.SelectedValue.ToString());
            if (fileath.Text == "")
            {
                reslut = string.Empty;
            }
            else
            {
                reslut = filePath;
                if (picTitle.Text == "")
                    pic_title = System.IO.Path.GetFileNameWithoutExtension(fileath.Text);
                else
                    pic_title = picTitle.Text;
                    reslut = reslut + "@@@" +pic_title;  
            }
            if (issave)
            {
                title newmedia = new title()
                {
                    date = DateTime.Now.ToString(),
                    title_name = System.IO.Path.GetFileName(fileath.Text),
                    context = fileath.Text
                };
                MainWindow.tree5_sel.media.Add(newmedia);
               
                //ThreadPool.QueueUserWorkItem(new WaitCallback(idisser_data.AddiDissertationMedia), (object)newmedia);
            }
            File.Copy(fileath.Text, filePath, true);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            reslut = "";
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.selctpic.ItemsSource = MainWindow.tree5_sel.media;
            this.selctpic.SelectedValuePath = "context";
            this.selctpic.DisplayMemberPath = "title_name";
        }

        public Boolean IsImage(string path)   //判断选中的文件是否为图片类型文件
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void selctpic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filep = this.selctpic.SelectedValue.ToString();
            filePath = MainWindow.tree5_sel.href + "\\picture\\" +System.IO.Path.GetFileName(filep);
            if(IsImage(filep))
            {
            this.fileath.Text = filep;
            this.image1.Source = new BitmapImage(new Uri(filep));
            }
            else
               System.Windows.Forms.MessageBox.Show("请选中图片文件");

        }

        private void chechkbox1_Click(object sender, RoutedEventArgs e)
        {
            if (chechkbox1.IsChecked == true)
                this.selctpic.IsEnabled = true;
            else
                this.selctpic.IsEnabled = false;
        }

        
    }
}
