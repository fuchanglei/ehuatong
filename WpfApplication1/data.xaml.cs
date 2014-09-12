using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
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
        private StreamWriter writeData;
        public data()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
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
            this.comboBox1.Items.Add("F:\\3.Echarts\\3.Echarts\\data.xlsx1111");
            //this.comboBox1.Text = "F:\\3.Echarts\\3.Echarts\\data.xlsx";
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //openexcel open_excel=
           // open_excel = new openexcel(this.comboBox1.Text);
           // MessageBox.Show(comboBox1.Text);
            //comboBox1.pr
            //this.comboBox2.ItemsSource = open_excel.getGetOleDbSchemaTable();
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
           System.Data.DataTable mytable=open_excel.getexcel(this.comboBox2.Text);
           List<dataTem> Row_data = new List<dataTem>();
           for (int c = 1; c < mytable.Columns.Count; c++)
           {
               dataTem row_title = new dataTem() 
               {
                  id=c,
                  rowname=mytable.Rows[0][c].ToString()
               };
               Console.WriteLine(mytable.Rows[0][c].ToString());
               Row_data.Add(row_title);
           }
           this.listView1.ItemsSource = Row_data;

        }
    }
}
