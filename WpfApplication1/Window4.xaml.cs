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
    /// Window4.xaml 的交互逻辑
    /// </summary>
   
    public partial class Window4 : Window
    {
        
        
        public Window4()
        {
            InitializeComponent();
            //icons[0] = @"images/folder documents blue.ico";
            comboBox1.ItemsSource = template.getTemplate.mTemplate;
            comboBox1.Text = comboBox1.Items[0].ToString();
            button1.IsEnabled = false;
        }
        public class idssSelect : EventArgs
        {
            private LVData m_newtitle;
            public LVData newtitle
            {
                get
                {
                    return m_newtitle;
                }
            }
            private iDissType _type;
            public iDissType type
            {
                get
                {
                    return _type;
                }
            }
            public idssSelect(LVData mm, iDissType newtype)
            {
                m_newtitle = mm;
                _type = newtype;
            }

        }
        public int type;
        public void gettype(string name)
        {
            int c =int.Parse(name.Replace("m",""));
            type = c;
        }
        //定义各种idd类型的所对应的图标
        public static string[] icons = { @"images/folder web blue.ico", @"images/folder web green.ico", @"images/folder web yellow.ico", @"images/folder .ico", @"images/address book.ico", @"images/folder favorits blue.ico", @"images/folder favorits green.ico", @"images/folder favorits yellow.ico" };
        public delegate void myevent(object sender, Window1.textEventArgs e1, idssSelect e2);
        public event myevent createidss;
        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox2.Text=="")
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            LVData lv = new LVData()
            {
                Name=((iDissType)type).ToString(),
                Pic=icons[type]
            };
            Window1.textEventArgs E1 = new Window1.textEventArgs(textBox2.Text.Trim());
            // E2 = new IconSelect(lv);
            idssSelect E2 = new idssSelect(lv, (iDissType)type);
            if (createidss != null)
            {
                this.createidss(this,E1,E2);
            }
            this.Close();
        }

    }
}
