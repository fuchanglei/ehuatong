using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// data.xaml 的交互逻辑
    /// </summary>
    public partial class data : Window
    {
        public string result;
        private openexcel open_excel;
        //private StreamWriter writeData;
        System.Data.DataTable mytable;
        private string datatype;
        private List<int> selectUid = new List<int>();//保存多选ID   
        private List<int> allUid = new List<int>();//保存用户ID 
        ObservableCollection<dataTem> Row_data =new ObservableCollection<dataTem>();
       //private List<dataTem> list = new List<dataTem>();//列表源数据   
        public data()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string rows = string.Empty;
            foreach (int c in selectUid)
            {
                rows =rows + "," + mytable.Rows[0][c].ToString();
            }
            rows=rows.Substring(1);
            StreamWriter sw = new StreamWriter("F:/3.Echarts/3.Echarts/data.ini", false, Encoding.Default);
            if (this.title_textbox.Text == "")
            {
                sw.WriteLine(" ");
            }
            else
            sw.WriteLine(this.title_textbox.Text);
            sw.WriteLine(datatype);
            sw.WriteLine(rows);
            if (this.danwei_textbox.Text != "")
            {
                sw.WriteLine(this.danwei_textbox.Text);
            }
            else
            {
                sw.WriteLine(" ");
            }
            for (int i = 1; i < mytable.Rows.Count; i++)
            {
                string data = string.Empty;
                foreach (int c in selectUid)
                {
                    data = data + "," + mytable.Rows[i][c].ToString();
                }
                sw.WriteLine(data.Substring(1));
                
            }
            sw.Flush();
            sw.Close();
            result = "ok";
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            result = "cancel";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboBox1.Items.Add("F:\\3.Echarts\\3.Echarts\\data.xlsx");
            this.comboBox1.Items.Add("D:\\3.Echarts\\3.Echarts\\data.xlsx1111");
            //this.comboBox1.Text = "F:\\3.Echarts\\3.Echarts\\data.xlsx";
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void comboBox1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show(comboBox1.Text);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            open_excel = new openexcel(this.comboBox1.Text);
            this.comboBox2.ItemsSource = open_excel.getGetOleDbSchemaTable();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
           mytable=open_excel.getexcel(this.comboBox2.Text);
          
           for (int c = 1; c < mytable.Columns.Count; c++)
           {
               dataTem row_title = new dataTem() 
               {
                  id=c,
                  rowname=mytable.Rows[0][c].ToString(),
                  ischeck=false
               };
               Row_data.Add(row_title); 
           }
           for (int i = 1; i < mytable.Rows.Count; i++)
           {
               datatype = datatype + "," + mytable.Rows[i][0].ToString();

           }
           datatype = datatype.Substring(1);
           this.listView1.ItemsSource = Row_data;

        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int uid = Convert.ToInt32(cb.Tag.ToString()); //获取该行id   
            if (cb.IsChecked == true)
            {
                selectUid.Add(uid);  //如果选中就保存id   
            }
            else
            {
                selectUid.Remove(uid);   //如果选中取消就删除里面的id   
            }
        }  
        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {

                selectUid = Row_data.Select(l => l.id).ToList();
                foreach (dataTem c in Row_data)
                {
                    c.ischeck = true;
                }
            }
            else
            {
                selectUid.Clear();
                foreach (dataTem c in Row_data)
                {
                    c.ischeck = false;
                }
            }
            //selectUid = allUid;
        }  
    }
}
