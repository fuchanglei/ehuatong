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
using System.IO;

namespace WpfApplication1
{
    /// <summary>
    /// improtData.xaml 的交互逻辑
    /// </summary>
    public partial class improtData : Window
    {
        public improtData()
        {
            InitializeComponent();
        }
        public delegate void myevent(object sender, Window5.textEventArgs e1,Window5.textEventArgs e2);
        public event myevent getdata;
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox2.Text == "")
            {
                this.textBox2.BorderBrush = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("Red");
            }
            else
            {
                string tag = (textBox1.Text == "") ? System.IO.Path.GetFileName(textBox2.Text.Trim()) : textBox1.Text;
               // if (textBox1.Text == "")
               // {
               //     tag = System.IO.Path.GetFileNameWithoutExtension(textBox2.Text.Trim());
               // }
               // else
                //    tag = textBox1.Text;
                Window5.textEventArgs E1 = new Window5.textEventArgs(tag);
                Window5.textEventArgs E2 = new Window5.textEventArgs(textBox2.Text);
                if (getdata != null)
                    getdata(this,E1,E2);
                this.Close();
            }
               
                //this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
            op.InitialDirectory = @"c:\";
            op.RestoreDirectory = true;
            op.Filter = "所有文件(*.*)|*.*";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = op.FileName;
            }
        }
    }
}
