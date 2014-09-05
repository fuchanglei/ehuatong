using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Window_Tag.xaml 的交互逻辑
    /// </summary>
    public partial class Window_Tag : Window
    {
        private Tag_data _listView1Select;
        private ObservableCollection<Tag> Init_tree = new ObservableCollection<Tag>();
        private ObservableCollection<Tag_data> _listviewItems=new ObservableCollection<Tag_data>();
        private bool isselect = true;
        private Tag _TagSelect;
        public Window_Tag()
        {
            InitializeComponent();
            initTag();
        }
        public delegate void myevent(object sender, ListviewText e);
        public event myevent getbq;
        public class ListviewText : EventArgs
        {
            private ObservableCollection<Tag_data> _listview;
            public ObservableCollection<Tag_data> m_listview
            {
                get
                {
                    return _listview;
                }
            }
            public ListviewText(ObservableCollection<Tag_data> mm)
            {
                _listview = mm;
            }
        }
        private void initTag()
        {
           //  = new ObservableCollection<Tag>();
            Tag root1 = new Tag()
            {
                
                DisplayName = "编程",
                isedit = Visibility.Collapsed,
                isshow = Visibility.Visible,
                parent = null
            };
            Tag root1_1 = new Tag()
            {
                
                DisplayName = "java",
                isedit = Visibility.Collapsed,
                isshow = Visibility.Visible,
                parent = root1
            };
            Tag root1_1_1 = new Tag()
            {
                
                DisplayName = "多线程",
                isedit = Visibility.Collapsed,
                isshow = Visibility.Visible,
                parent = root1_1
            }; 
            Tag root2 = new Tag()
            {
                
                DisplayName = "硬件",
                isshow = Visibility.Visible,
                isedit = Visibility.Collapsed,
                parent = null
            };
            root1_1.Children.Add(root1_1_1);
            root1.Children.Add(root1_1);
            Init_tree.Add(root1);
            Init_tree.Add(root2);
            //return Init_tree;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            this.treeView1.ItemsSource = Init_tree;
            this.listView1.ItemsSource = _listviewItems;
        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _TagSelect = treeView1.SelectedItem as Tag;
           // MessageBox.Show(_TagSelect.DisplayName);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            ListviewText E = new ListviewText(_listviewItems);
            if (getbq != null)
            {
                getbq(this, E);
            }
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        internal static class Utils
        {
            public static T FindVisualParent<T>(DependencyObject obj) where T : class
            {
                while (obj != null)
                {
                    if (obj is T)
                        return obj as T;

                    obj = VisualTreeHelper.GetParent(obj);
                }
                return null;
            }
        }
        private void textbox_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Point ps = e.GetPosition(treeView1);
            HitTestResult result = VisualTreeHelper.HitTest(treeView1, ps);
            if (result == null)
                return;
            TreeViewItem selectItem = Utils.FindVisualParent<TreeViewItem>(result.VisualHit);
            if (selectItem != null)
                selectItem.IsSelected = true;
           // Tag cc = selectItem as Tag;
          // selectItem.GetType()
           // object cc1 =ConvertselectItem;

           // string cc=((Tag)cc1).DisplayName;
          // MessageBox.Show(cc);
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (isselect)
            {
                if (_listView1Select != null)
                    _listView1Select.isvisibli = Visibility.Collapsed;
                _listView1Select = this.listView1.SelectedItem as Tag_data;
                _listView1Select.isvisibli = Visibility.Visible;
            }
           
         
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("dasdasd");
            isselect = false;
            _listviewItems.Remove(_listView1Select);
            isselect = true;
           
        }
        private string getTag(Tag select)
        {
            if (select.parent != null)
                return getTag(select.parent) + "->"+select.DisplayName;
            else
                return select.DisplayName;
        }
        private void MenuItem_Click_addTag(object sender, RoutedEventArgs e)
        {
            _TagSelect = treeView1.SelectedItem as Tag;
            string name = getTag(_TagSelect);
            Tag_data select_tag = new Tag_data()
            {
                name=name,
                isvisibli=Visibility.Collapsed
              
            };
            _listviewItems.Add(select_tag);
        }
        private void MenuItem_Click_CreateTag(object sender, RoutedEventArgs e)
        {
            
            Tag newnone = new Tag()
            {
                 DisplayName="双击修改",
                  parent=_TagSelect,
                  isedit=Visibility.Visible,
                  isshow=Visibility.Collapsed
            };
            _TagSelect.Children.Add(newnone);
        }
        private void MenuItem_Click_DeleteTag(object sender, RoutedEventArgs e)
        {
            
            _TagSelect = treeView1.SelectedItem as Tag;
            if (_TagSelect.parent != null)
                _TagSelect.parent.Children.Remove(_TagSelect);
            else
                Init_tree.Remove(_TagSelect);
              
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _TagSelect.isedit = Visibility.Collapsed;
            _TagSelect.isshow = Visibility.Visible;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            _TagSelect = treeView1.SelectedItem as Tag;
            if (_TagSelect != null)
            {
                string name = getTag(_TagSelect);
                Tag_data select_tag = new Tag_data()
                {
                    name = name,
                    isvisibli = Visibility.Collapsed

                };
                _listviewItems.Add(select_tag);
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _TagSelect.isshow = Visibility.Collapsed;
            _TagSelect.isedit = Visibility.Visible;
        }
        
    }
}
