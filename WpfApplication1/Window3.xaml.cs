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
    public partial class Window3 : Window
    {
        private ObservableCollection<LVData> LVDatas = new ObservableCollection<LVData>();
        public Window3()
        {

            InitializeComponent();
            textBox2.Text = "C:\\Users\\fuchanglei\\Documents\\fu_changlei@126.com\\";
            LVDatas.Add(new LVData { Name = "图标1", Pic = @"images/folder favorits blue.ico" });
            LVDatas.Add(new LVData { Name = "图标2", Pic = @"images/folder documents blue.ico" });
            LVDatas.Add(new LVData { Name = "图标3", Pic = @"images/folder web yellow.ico" });
            LVDatas.Add(new LVData { Name = "图标4", Pic = @"images/folder web green.ico" });
            LVDatas.Add(new LVData { Name = "图标5", Pic = @"images/folder image blue.ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/folder .ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/folder documents red.ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/folder close blue.ico" });
            LVDatas.Add(new LVData { Name = "图标6", Pic = @"images/folder close green.ico" });
            listView1.ItemsSource = LVDatas;
            
            button1.IsEnabled = false;
            
            
        }
        public delegate void myevent(object sender, Window1.textEventArgs e1, IconSelect e2);
        public event myevent creattitle;
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window1.textEventArgs E1 = new Window1.textEventArgs(textBox1.Text.Trim());
            IconSelect E2 = new IconSelect((LVData)listView1.SelectedItem);
            if (creattitle != null)
            {
                creattitle(this, E1, E2);
            }
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // public delegate void myevent(object sender, Window1.textEventArgs e1,IconSelect e2);

    }
}
