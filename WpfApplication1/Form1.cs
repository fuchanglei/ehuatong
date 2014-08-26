using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WpfApplication1
{  
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.SmallIcon;
        }
        //private PropertyNodeItem newbq;
        //public Window1 w1 = new Window1();
        
        public delegate void myevent(object sender,ListviewText e);
        public event myevent getbq;
        public class ListviewText:EventArgs
        {
          private string m_textbox;
            public string textbox
            {
                get
                {
                    return m_textbox;
                }
            }
            public ListviewText(string mm)
            {
                m_textbox = mm;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            foreach (ListViewItem li in listView1.SelectedItems)
            {
                listView1.Items.Remove(li);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                ;
            }
            else
            {
                listView1.Items.Insert(0, textBox1.Text);
                listView1.Items[0].Selected = true;

            }
            ListviewText E = new ListviewText(listView1.SelectedItems[0].Text.Trim());
            if (getbq != null)
            {
                getbq(this, E);
            }
            textBox1.Text = "";
           

        }
    }
}
