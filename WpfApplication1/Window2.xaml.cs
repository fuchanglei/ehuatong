using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {   
        private ObservableCollection<LVData> LVDatas = new ObservableCollection<LVData>();
        public Window2()
        {
           
            InitializeComponent();
            textBox2.Text = "C:\\Users\\fuchanglei\\Documents\\fu_changlei@126.com\\";
            LVDatas.Add(new LVData { Name = "图标1", Pic = @"images/user_messages.ico" });
            LVDatas.Add(new LVData { Name = "图标2", Pic = @"images/my files.ico" });
            LVDatas.Add(new LVData { Name = "图标3", Pic = @"images/user_called_messages.ico" });
            LVDatas.Add(new LVData { Name = "图标4", Pic = @"images/attachment_s.ico" });
            LVDatas.Add(new LVData { Name = "图标5", Pic = @"images/user_messages.ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/zoom.ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/zoom.ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/zoom.ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/zoom.ico" });
            listView1.ItemsSource = LVDatas;
            button1.IsEnabled = false;
            
            
        }
        public delegate void myevent(object sender, Window1.textEventArgs e1,IconSelect e2);
        public event myevent creattitle;
        
       

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
           Window1.textEventArgs E1 = new Window1.textEventArgs(textBox1.Text.Trim());
           IconSelect E2;
           if (listView1.SelectedItem == null)
           {
                E2 = new IconSelect((LVData)listView1.Items[0]);
           }
           else
               E2 = new IconSelect((LVData)listView1.SelectedItem);
           if (creattitle != null)
           {
               creattitle(this, E1, E2);
           }
           this.Close();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text == "")
            {
                button1.IsEnabled = false;

            }
            //    return;
            else
            {
                button1.IsEnabled = true;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
