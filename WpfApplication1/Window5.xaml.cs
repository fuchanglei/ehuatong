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
    public partial class Window5 : Window
    {
        private string name=string.Empty;
        private int type;
        public void getname(string temp,int i)
        {
            name = temp;
            type =i;
        }
     
        public Window5()
        {
            InitializeComponent();
            
           
            
        }
        public delegate void myevent(object sender,textEventArgs e);
        public event myevent getdata;
       // public event myevent getnote;
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
            getdata(this, E);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Window1 w1 = new Window1();
            switch (type)
            { 
                case 1:
                textBlock1.Text = "章节名：";
                this.Title = "修改章节名";
                break;
                case 2:
                textBlock1.Text = "小节名：";
                this.Title = "修改小节名";
                break;
                case 3:
                textBlock1.Text = "章节名：";
                this.Title = "修改小节名";
                break;
                case 4:
                textBlock1.Text = "章节：";
                this.Title = "新建章节";
                break;
                case 5:
                textBlock1.Text = "小节名：";
                this.Title = "新建小节";
                break;
                default:
                break;
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
