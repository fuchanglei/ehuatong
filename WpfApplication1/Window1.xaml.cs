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

namespace WpfApplication1
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private string name=string.Empty;
        private int type;
        public void getname(string temp,int i)
        {
            name = temp;
            type =i;
        }
     
        public Window1()
        {
            InitializeComponent();
            
           
            
        }
        public delegate void myevent(object sender,textEventArgs e);
        public event myevent getdata;
        public event myevent getnote;
        public class textEventArgs : EventArgs
        {
            private string m_textbox;
            public string textbox
            {
                get
                {
                    return m_textbox;
                }
            }
            public textEventArgs(string mm)
            {
                m_textbox = mm;
            }
        
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
        
           // name = textBlock1.Text.Trim();
            textEventArgs E = new textEventArgs(textBox1.Text.Trim());
            //close_window();
            if (type == 0)
            {
                if (getdata != null)
                {
                   // textEventArgs E = new textEventArgs(textBox1.Text.Trim());
                    getdata(this, E);
                }
            }
            if (type == 1)
            {
                if (getnote != null)
                {
                    //textEventArgs E = new textEventArgs(textBox1.Text.Trim());
                    getnote(this, E);
                }
            }

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Window1 w1 = new Window1();
            if (type == 0)
            {
                //w1.Title = "重命名专题";
                textBlock1.Text = "专题名或者目录名：";
            }
            if (type == 1)
            {
                //w1.Title = "重命名笔记";
                textBlock1.Text = "笔记名：";
            }
            textBox1.Text = name;
            button1.IsEnabled = false;

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            button1.IsEnabled = true;
        }

        
    }
}
