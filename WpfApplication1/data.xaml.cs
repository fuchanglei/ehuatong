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
        private string excel_name;
        private string sheet_name;
        ObservableCollection<dataTem> Row_data =new ObservableCollection<dataTem>();
       //private List<dataTem> list = new List<dataTem>();//列表源数据   
        public data()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {  
                string rows = string.Empty;
                string title = string.Empty;
                string danwei = string.Empty;
                foreach (int c in selectUid)
                {
                    rows = rows + "," + mytable.Columns[c].ColumnName;
                }
                rows = rows.Substring(1);
                // StreamWriter sw = new StreamWriter("F:/3.Echarts/3.Echarts/data.ini", false, Encoding.Default);
                if (this.title_textbox.Text == "")
                {
                    title = " ";
                }
                else
                    title = this.title_textbox.Text;
                // sw.WriteLine(datatype);
                //sw.WriteLine(rows);
                if (this.danwei_textbox.Text != "")
                {
                    //sw.WriteLine(this.danwei_textbox.Text);
                    danwei = this.danwei_textbox.Text;
                }
                else
                {
                    // sw.WriteLine(" ");
                    danwei = " ";
                }
                result = "#title=" + title + "&datatype=" + datatype + "&rows=" + rows + "&danwei=" + danwei + "&data=";

                for (int i = 0; i < mytable.Rows.Count; i++)
                {
                    string data = string.Empty;
                    foreach (int c in selectUid)
                    {
                        data = data + "," + mytable.Rows[i][c].ToString();
                    }
                    data = data.Substring(1);
                    // sw.WriteLine(data.Substring(1));

                    result = result + data + "@@";

                }
            }
            catch
            {
                ;
            }
            //result = "#title="+this.title_textbox.Text+"&datatype="+datatype+"&rows="+rows+",";
            //sw.Flush();
           // sw.Close();
            //result = "ok";
           // return result;
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            result = "cancel";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.comboBox1.ItemsSource = MainWindow.tree5_sel.data;
            this.comboBox1.SelectedValuePath = "context";
            this.comboBox1.DisplayMemberPath = "title_name";
           
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedItem.ToString());
            if (excel_name != comboBox1.SelectedItem.ToString())
            {
                open_excel = new openexcel(comboBox1.SelectedValue.ToString());
                this.comboBox2.ItemsSource = open_excel.getGetOleDbSchemaTable();
               // this.comboBox2.Text = comboBox2.Items[0].ToString();
            }
            excel_name = comboBox1.SelectedItem.ToString();
            
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // result = "cancel";
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sheet_name != this.comboBox2.SelectedItem.ToString())
            {
                mytable = open_excel.getexcel(this.comboBox2.SelectedItem.ToString());
            }
            for (int c = 1; c < mytable.Columns.Count; c++)
            {
                dataTem row_title = new dataTem()
                {
                    id = c,
                    rowname = mytable.Columns[c].ColumnName,
                    ischeck = false
                };
                Row_data.Add(row_title);
            }
            for (int i = 0; i < mytable.Rows.Count; i++)
            {
                datatype = datatype + "," + mytable.Rows[i][0].ToString();

            }
            datatype = datatype.Substring(1);
            this.listView1.ItemsSource = Row_data;
            sheet_name = this.comboBox2.SelectedItem.ToString();
        }  
    }
}
