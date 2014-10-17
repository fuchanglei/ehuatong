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
using WpfApplication1.Refer;

namespace WpfApplication1
{
    /// <summary>
    /// References.xaml 的交互逻辑
    /// </summary>
    public partial class References : Window
    {
        private Dictionary<int, GroupBox> myDictionary = new System.Collections.Generic.Dictionary<int, GroupBox>();
        private int Comboxindex=0;
        public int result = 0;
        public References()
        {
            InitializeComponent();
            myDictionary.Add(0,this.book);
            myDictionary.Add(1, this.journal);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {  
            //this.com1.ItemsSource=

            this.com1.DisplayMemberPath = "Type";
            this.com1.SelectedValuePath = "suoxie";
            this.com1.SelectedIndex = 0;
            this.lauageCombobox.SelectedIndex = 0;
            List<ReferenceList> referlist = new List<ReferenceList>();
            for (int c = 0; c < MainWindow.tree5_sel.Refernumber.Count; c++)
            {
                ReferenceList aa = new ReferenceList()
                {
                     num="["+(c+1).ToString()+"]",
                     context=MainWindow.tree5_sel.Refer[c].Context
                };
                referlist.Add(aa);
            }
            this.listref.ItemsSource = referlist;

        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            ReferencesInfo newnone = new ReferencesInfo();
            if (this.check1.IsChecked == false)
            {
                if (Comboxindex == 0)
                {
                    //newnone.Type = this.com1.SelectedItem as ReferencesType;
                    //newnone.Author = this.book_author.Text;
                    // newnone.Title = this.book_title.Text;
                    // newnone.Year = this.book_Year.Text;
                    // newnone.Address = this.book_address.Text;
                    // newnone.Publishers = this.book_publishers.Text;
                    newnone.Context = book_author.Text + "." + book_title.Text + (this.com1.SelectedItem as ReferencesType).Suoxie + "." + this.book_address.Text + ":" + book_publishers.Text + "," + this.book_Year.Text;
                }
                if (Comboxindex == 1)
                {
                    // newnone.Type = this.com1.SelectedItem as ReferencesType;
                    // newnone.Author = this.journal_author.Text;
                    //  newnone.Title = this.journal_title.Text;
                    // newnone.Journal_qikantitle = this.journal_qikantitle.Text;
                    // newnone.Year = this.journal_Year.Text;
                    //  newnone.Month = this.journal_Month.Text;
                    // newnone.Page = this.journal_page.Text;
                    newnone.Context = this.journal_author.Text + "." + this.journal_title.Text + (this.com1.SelectedItem as ReferencesType).Suoxie + "." + this.journal_qikantitle.Text + "," + this.journal_Year.Text + "(" + this.journal_Month.Text + ")" + ":" + this.journal_page.Text;
                }
            }
            else
            {
                newnone.Context = "同" + (this.listref.SelectedItem as ReferenceList).num;
             }
            MainWindow.tree5_sel.Refer.Add(newnone);
            result = MainWindow.tree5_sel.Refer.Count;
            MainWindow.tree5_sel.Refernumber.Add(result);
            result = MainWindow.tree5_sel.Refernumber.Count;  //sasdffdsaffafasfasdfsdafdsafsdaf
            this.Close();
          
        }

        private void com1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            myDictionary[Comboxindex].Visibility = Visibility.Collapsed;
            Comboxindex = this.com1.SelectedIndex;
            myDictionary[Comboxindex].Visibility = Visibility.Visible;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            result = 0;
            this.Close();
        }
        private void check1_Click(object sender, RoutedEventArgs e)
        {
            if (this.check1.IsChecked == false)
            {
                this.listref.Visibility = Visibility.Collapsed;
                this.addref.Visibility = Visibility.Visible;

            }
            else
            {
                this.listref.Visibility = Visibility.Visible;
                this.addref.Visibility = Visibility.Collapsed;

            }
        }
    }
}
