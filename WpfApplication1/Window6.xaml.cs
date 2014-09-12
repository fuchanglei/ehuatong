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
    /// Window6.xaml 的交互逻辑
    /// </summary>
    public partial class Window6 : Window
    {
        public Window6()
        {
            InitializeComponent();
        }
        public delegate void myevent(object sender,Window5.textEventArgs e);
        public event myevent getdata;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window5.textEventArgs E = new Window5.textEventArgs(textBox1.Text.Trim());
            getdata(this, E);
            this.Close();
        }

        
    }
}
