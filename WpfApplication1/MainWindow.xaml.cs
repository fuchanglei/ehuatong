using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using WebBrowserEXT;
using System.Threading;
using Code.WebbrowserInteropr;


namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string item_Directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\iDissertation";  //当前idss文件所在目录
       // public List<PropertyNodeItem> itemlist3 = new List<PropertyNodeItem>();
        //title listdata=new title();
        ObservableCollection<title> itemlist = new ObservableCollection<title>();
        private outline_Data select_tree5;
        public static string idd_href;
        public Data gettreeview = new Data();
        AdornerLayer mAdornerLayer = null;
        DateTime mStartHoverTime = DateTime.MinValue;
        TreeViewItem mHoveredItem = null;
        public static iDissertation tree5_sel;
        private bool isdrag = false;
        private string file_size;  //文件大小
        private string newname=string.Empty;
        private int count;   //当前目录组下笔记的数目
        public title cc = new title();
        public outline tree6_sel =new outline();
        public string savecontext;
        //public bool issave;
        private int last;
        private bool isselect = true;
        private string filename;
        private bool issave;
        private bool isdelete=true;
        public bool web_show = false;
        ObservableCollection<sidefiles> ss = new ObservableCollection<sidefiles>();
        private Tag_data _listView1Select;
        private ObservableCollection<Tag_data> _listviewItems = new ObservableCollection<Tag_data>();
        ContextMenu c1;
        ContextMenu c2;
        ContextMenu c3;
        ContextMenu c4;
        ContextMenu c5;
        WebbrowserScriptInvoker invoker;
        public MainWindow()
        {
            InitializeComponent();
            AttachEvent();
            showcotext();
            last= ((PropertyNodeItem)tree2.Items[0]).Children.Count;
            //TreeViewItem cc = this.tree2.Items[0] as TreeViewItem;
            //cc.IsExpanded = true;
            listView2.ItemsSource = ss;
            invoker = new WebbrowserScriptInvoker(webBrowser1);
            wrapPanel1.Visibility = Visibility.Collapsed;
           // listView2.ItemsSource = ss.items;
            c1= cireateMenu1();
            c2 = cireateMenu2();
            c3 = cireateMenu3();
            c4 = cireateMenu4();
            c5 = cireateMenu5();
            if(!Directory.Exists(item_Directory))
            {
                Directory.CreateDirectory(item_Directory);
            }
           // this.listViewTag.ItemsSource = _listviewItems;
            WebBrowserSecurity.setIEInternetSecurity();
            this.listViewTag.ItemsSource = _listviewItems;
            Window_Tag www = new Window_Tag();
            this.treeView1.ItemsSource = www.Init_tree;
            this.webBrowser1.Navigate("file:///F:/ueditor1_3_6-src_tofuchangli/ueditor1_3_6-src/index.html");
            this.webBrowser1.ObjectForScripting = new JSEvent();
        }
        private ContextMenu cireateMenu1()
       {
            ContextMenu con1 = new ContextMenu();
            MenuItem m1 = new MenuItem();
            m1.Header = "新建专题";
            m1.Click += MenuItem_Click;
            con1.Items.Add(m1);
            return con1;
      }
        private ContextMenu cireateMenu2()
        {
            ContextMenu con2 = new ContextMenu();
            MenuItem m1 = new MenuItem();
            m1.Header = "新建目录";
            m1.Click += create_dire;
            con2.Items.Add(m1);
            MenuItem m2 = new MenuItem();
            m2.Header = "新建笔记";
            m2.Click += note_add_Click;
            con2.Items.Add(m2);
            MenuItem m3 = new MenuItem();
            m3.Header = "删除专题";
            m3.Click += MenuItem_Click_1;
            con2.Items.Add(m3);
            MenuItem m4 = new MenuItem();
            m4.Header = "修改专题名称";
            m4.Click += modify_title;
            con2.Items.Add(m4);
            return con2;
        }
        private ContextMenu cireateMenu3()
        {
            ContextMenu con3 = new ContextMenu();
            MenuItem m1 = new MenuItem();
            m1.Header = "新建目录";
            m1.Click += create_dire;
            MenuItem m2 = new MenuItem();
            m2.Header = "新建笔记";
            m2.Click += note_add_Click;
            MenuItem m3 = new MenuItem();
            m3.Header = "修改目录名称";
            m3.Click += modify_title;
            MenuItem m4 = new MenuItem();
            m4.Header = "删除目录";
            m4.Click += MenuItem_Click_1;
            con3.Items.Add(m1);
            con3.Items.Add(m2);
            con3.Items.Add(m4);
            con3.Items.Add(m3);
            return con3;
        }
        private ContextMenu cireateMenu4()
        {
            ContextMenu c4 = new ContextMenu();
            MenuItem m1 = new MenuItem();
            m1.Header = "删除";
            m1.Click += MenuItem_Click_1;
            c4.Items.Add(m1);
            return c4;
        }
        private ContextMenu cireateMenu5()
        {
            ContextMenu con1 = new ContextMenu();
            MenuItem m1 = new MenuItem();
            m1.Header = "新建小结";
            m1.Click += add_child;
            MenuItem m2 = new MenuItem();
            m2.Header = "插入新章节";
            m2.Click += add_charpt;
            MenuItem m3 = new MenuItem();
            m3.Header = "修改提名";
            m3.Click += modify_outline;
            MenuItem m4 = new MenuItem();
            m4.Header = "删除";
            m4.Click += delet_outline;
           // m4.Click += remove_item;
            con1.Items.Add(m1);
            con1.Items.Add(m2);
            con1.Items.Add(m3);
            con1.Items.Add(m4);
            return con1;
        }  //outline 操作菜单
        private void add_child(object sender, RoutedEventArgs e)
        {
            // cc = treeView1.SelectedItem as outline;
            Window5 w5 = new Window5();
            w5.getname(newname, 5);
            w5.getdata += new Window5.myevent(m_window5_outline_add);
            w5.Show();
          // tree6_sel.children.Add(newone);
        }  //增加小节
        private void add_charpt(object sender, RoutedEventArgs e)
        {
            Window5 w5 = new Window5();
            w5.getname(newname, 4);
            w5.getdata += new Window5.myevent(m_window5_outline_addchapter);
            w5.Show();

        }   //增加章节
        private void modify_outline(object sender, RoutedEventArgs e)  //修改outline提名
        {
            string[] ss = tree6_sel.Name1.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Window5 w5 = new Window5();
            w5.getname(ss[1],(int)tree6_sel.type);
            w5.getdata += new Window5.myevent(m_window5_outline_modify);
            w5.Show();
        }
        private void delet_outline(object sender, RoutedEventArgs e)//删除提名
        {
            outline_Data delet_outline = new outline_Data();
            delet_outline.outline_node_delet(tree6_sel);
            issave = false;
            this.tree6.ItemsSource = delet_outline.TreeViewItems1;
            
        }
        private void m_window5_outline_modify(object sender, Window5.textEventArgs e)  
        {
            newname =e.textbox.Replace(" ","");
            //tree6_sel.Name1 = newname;
           // this.tree6.ItemsSource = outline_Data.Instance.TreeViewItems1;
           outline_Data modify_outline = new outline_Data();
            modify_outline.outline_node_modify(tree6_sel,newname);
           // select_tree5.outline_node_modify
            issave = false;
           this.tree6.ItemsSource = modify_outline.TreeViewItems1;
            
        }
        private int getindex(outline cc)
        {
            int i = 0;
            foreach (outline cv in this.tree6.Items)
            {
                i++;
                if (cv.Name1 == cc.Name1)
                    break;
            }
            return i;
        }
        private void m_window5_outline_addchapter(object sender, Window5.textEventArgs e)
        {
            //获取事件传递过来的数据
            newname = e.textbox;
            outline_Data add_section = new outline_Data();
            outline sel = (outline)tree6.SelectedItem;
            int c = getindex(sel);
            outline aa=add_section.outline_chapter_add(sel, newname);
            //outline_Data.Instance.TreeViewItems1.Insert(c, aa);

            ObservableCollection<outline> cc = tree6.ItemsSource as ObservableCollection<outline>;
            cc.Insert(c, aa);
            
        }
        private void m_window5_outline_add(object sender, Window5.textEventArgs e)
        {
            //获取事件传递过来的数据
            newname = e.textbox;
            outline_Data add_section = new outline_Data();
            outline sel = (outline)tree6.SelectedItem;
            add_section.outline_node_add(sel, newname);
           
        }
        private void AttachEvent()
        {
            listView1.PreviewMouseMove +=OnPreviewMouseMove;
            listView1.QueryContinueDrag += OnQueryContinueDrag;
            
        }
        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                return;
           System.Windows.Point pos = e.GetPosition(listView1);
            HitTestResult result = VisualTreeHelper.HitTest(listView1, pos);
            if (result == null)
                return;
            ListViewItem listitem= Utils.FindVisualParent<ListViewItem>(result.VisualHit);
            if (listitem == null || !(listView1.SelectedItem is title))
                return;
            DragDropAdorner adorner = new DragDropAdorner(listitem);
           // mAdornerLayer=
            mAdornerLayer = AdornerLayer.GetAdornerLayer(mTopLevelGrid);
            mAdornerLayer.Add(adorner);
            cc = listitem.Content as title;
            object db = (object)cc.title_name;
           // DataObject db = new DataObject((Object)cc.title_name);
            System.Windows.DragDrop.DoDragDrop(listView1, db, DragDropEffects.Copy);
            mStartHoverTime = DateTime.MinValue;
            mHoveredItem = null;
            mAdornerLayer.Remove(adorner);
            mAdornerLayer = null;
        }
        private void showcotext()
        {  
            //this.webBrowser1.DocumentCompleted
           // ObservableCollection<title> itemlist = new ObservableCollection<title>();
            //title
            title id1 = new title()
            {    ID="1",
                title_name = "saaeqw",
                date = "2014-04-05",
                context = "a的点对点的熬到asadadasddddddq34455",
                image = "/WpfApplication1;component/Images/add.ico",
            };
            title id2 = new title()
            { 
                ID="2",
                title_name = "的受委屈",
                date = "2014-04-05",
                context = "豆丁网对点的熬到asadadasddddddq3445",
                image = "/WpfApplication1;component/Images/attributes.ico",
            };
           
           itemlist.Add(id1);
            //ltitle_item.Add(id1);
           // listdata.title_item.Add(id2);
           itemlist.Add(id2);
            this.listView1.ItemsSource = itemlist;
        }
        private void OnQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            mAdornerLayer.Update();
        }
        private void savexml(outline sel, string html)
        {
            //outline sel = tree6.SelectedItem as outline;
            if (sel.nodename != "Cover" || sel.nodename != "Statement")
            {
                //string html = invoker.InvokeScript("getContent").ToString();
                if (sel.type == outlinetype.common)
                {
                    Savexml cxml = new Savexml(sel.nodename, html, sel.secid);
                    cxml.savexml(false);
                    //ThreadPool.QueueUserWorkItem(status => cxml.savetem());
                }
                else
                {
                    Savexml sxml = new Savexml("Papersection/" + sel.nodename, html, sel.secid);
                    sxml.savexml(false);
                  // ThreadPool.QueueUserWorkItem(status => sxml.savetem());
                }
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {    
            /*
            */
            Window2 w2 = new Window2();
            w2.creattitle+=new Window2.myevent(createnewtitle);
            w2.Show();
        }
        private void createnewtitle(object sender,Window1.textEventArgs e1,IconSelect e2)
        {
            Nodeetype nodelve=Nodeetype.subject;
            PropertyNodeItem item = this.tree2.SelectedItem as PropertyNodeItem;
            if (item.node_lev == Nodeetype.one)
            {
                nodelve = Nodeetype.subject;
            }
            else
                nodelve = Nodeetype.Directory;
            PropertyNodeItem node_item = new PropertyNodeItem()
            {
                DisplayName =e1.textbox,
                Name =e1.textbox,
                Icon = e2.newtitle.Pic,
                parent = item,
                node_lev=nodelve
            };
            item.Children.Insert(0, node_item);
        
        }

        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("哒哒哒!");
        }

        private void add_node(object sender, RoutedEventArgs e)
        {

            PropertyNodeItem item = this.tree3.SelectedItem as PropertyNodeItem;
            PropertyNodeItem node_item = new PropertyNodeItem()
            {
                DisplayName = "我的主题集",
                Name = "This is the discription of Node1. This is a folder.",
                Icon = @"images/my files.ico",
            };
            // int i=itemlist3.
            //itemlist3[1].Children.Add(node_item);
            // item.Children.Add(node_item);
            //itemlist3.Add(item);
           // this.tree3.ItemsSource = itemlist3;
            MessageBox.Show(item.DisplayName + "111");
        }

        private void StackPanel_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            MessageBox.Show("111");
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void tree2_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
             web_show = true;
             PropertyNodeItem sel = (PropertyNodeItem)tree2.SelectedItem;
             newname = sel.DisplayName;
            this.tree6.Visibility = Visibility.Hidden;
            listView1.Visibility = Visibility.Visible;
            PropertyNodeItem cc = (PropertyNodeItem)tree2.SelectedItem;
            if (cc.node_lev ==Nodeetype.one)
            {
                //MessageBox.Show(tree2.SelectedItem.ToString());
                //MessageBox.Show(cc.DisplayName);
                this.tree2.ContextMenu = c1;
            }
            else if (cc.node_lev==Nodeetype.subject)
            {
                this.tree2.ContextMenu = c2;
            }
            else if (cc.node_lev == Nodeetype.Directory)
            {
                this.tree2.ContextMenu = c3;
            }
            else if (cc.node_lev == Nodeetype.note)
            {
                this.tree2.ContextMenu = c4;
            }
            else
                this.tree2.ContextMenu = null;
                 //web_show =false;
                //invoker.InvokeScript("setContent", "sadfasdfasfaf1111111");
        }

        private void tree3_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
             //PropertyNodeItem cc = (PropertyNodeItem)tree3.SelectedItem;
          //  MessageBox.Show(tree2.SelectedItem.ToString());
           // MessageBox.Show(cc.DisplayName);
        }

        private void tvProperties_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PropertyNodeItem cc = (PropertyNodeItem)tvProperties.SelectedItem;
            MessageBox.Show(cc.DisplayName);
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //title c = (title)listView1.SelectedItem;
            //MessageBox.Show(c.title_name);
            if (web_show)
           {
               this.webBrowser1.Navigate("file:///F:/ueditor1_3_6-src_tofuchangli/ueditor1_3_6-src/index.html");
                web_show = false;
            }
            cc = (title)listView1.SelectedItem;
            if (isdrag == true)
                return;
            textBox2.Text = cc.title_name;
            isdrag = false;
            if (invoker.WaitWebPageLoad() == true)
            {
                invoker.InvokeScript("setContent", "sadfasdfasfaf1111111fu"); 
            } 

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

        private void OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            System.Windows.Point ps = e.GetPosition(tree2);
            HitTestResult result = VisualTreeHelper.HitTest(tree2, ps);
            if (result == null)
                return;
            TreeViewItem selectItem = Utils.FindVisualParent<TreeViewItem>(result.VisualHit);
            if (selectItem != null)
                selectItem.IsSelected = true;

            e.Effects = DragDropEffects.Copy;
            isdrag = true;

        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            //MessageBox.Show(e.Data.GetData(typeof(string)));
            System.Windows.Point ps = e.GetPosition(tree2);
            HitTestResult result = VisualTreeHelper.HitTest(tree2,ps);
            if (result == null)
                return;
            TreeViewItem selectedItem = Utils.FindVisualParent<TreeViewItem>(result.VisualHit);
            if (selectedItem == null)
                return;
            PropertyNodeItem parent = selectedItem.Header as PropertyNodeItem;
            cc=(title)listView1.SelectedItem;
            string data = e.Data.GetData(typeof(string)) as string;
            //isdrag = true;
            PropertyNodeItem newitem = new PropertyNodeItem()
            {
                DisplayName =data,
                Name = data,
                Icon = "",
                parent=parent,
            };
            if (parent != null && newitem != null)
            {
                parent.Children.Add(newitem);
                itemlist.Remove(cc);
                label4.Content = (count-1).ToString()+"条";
            }
            isdrag = false;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            PropertyNodeItem cc = (PropertyNodeItem)tree2.SelectedItem;
            PropertyNodeItem parent = cc.parent;
            if (parent == null)
                return;
            parent.Children.Remove(cc);
      
        }
       
        private void modify_title(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.getname(newname,0);
            w1.getdata += new Window1.myevent(m_window1_SelectionChanged);
            w1.Show();
        }
        private void create_dire(object sender, RoutedEventArgs e)
        {
            Window3 w3 = new Window3();
            w3.creattitle += new Window3.myevent(createnewtitle);
            w3.Show();            
        }
       
        private void m_window1_SelectionChanged(object sender, Window1.textEventArgs e)
        {
            //获取事件传递过来的数据
             newname = e.textbox;
             PropertyNodeItem sel = (PropertyNodeItem)tree2.SelectedItem;
             sel.DisplayName = newname;
        }
        private void modify_idd_title(object sender, RoutedEventArgs e)  //修改idd名称
        {
            Window1 w1 = new Window1();
            w1.getname(tree5_sel.Name, 0);
            w1.getdata += new Window1.myevent(m_window1_idd_NameChanged);
            w1.Show();
        }
        private void saveashtml(object sender, RoutedEventArgs e)  //导出为html包
        {
            System.Windows.Forms.FolderBrowserDialog folder1 = new System.Windows.Forms.FolderBrowserDialog();
            folder1.Description = "选择保存路径";
            folder1.ShowNewFolderButton = true;
            if (folder1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                ThreadPool.QueueUserWorkItem(status => exporttohtml.export(tree5_sel,folder1.SelectedPath));
            }
            

        }
        private void m_window1_idd_NameChanged(object sender, Window1.textEventArgs e)
        {
            //获取事件传递过来的数据
            newname = e.textbox;
          
            iDissertation sel = (iDissertation)tree5.SelectedItem;
            idisser_data.idisser.TreeViewItems4_modify(sel,newname);
            sel.Name= newname;
        }
        private void delete_idd_title(object sender, RoutedEventArgs e)
        {
               isselect = false;
               issave = false;
               idisser_data delete = new idisser_data();
                iDissertation dd = (iDissertation)tree5.SelectedItem;
               delete.TreeViewItems4_delete(dd);
               this.tree6.ItemsSource = null;
               isselect = true;
               
               //this.tree6.Style
               
            
        }
        private void MenuItem_modify(object sender, RoutedEventArgs e)
        {
            cc = this.listView1.SelectedItem as title;
            newname = cc.title_name;
            Window1 w1 = new Window1();
            w1.getname(newname,1);
            w1.getnote += new Window1.myevent(m_window1_modify_notetitle);
            w1.Show();
                
        }
        private void m_window1_modify_notetitle(object sender, Window1.textEventArgs e)   //修改笔记名入口
        {
            newname = e.textbox;
            cc = (title)listView1.SelectedItem;
            cc.title_name = newname;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            count = listView1.Items.Count;
            label4.Content = count.ToString()+"条";
            
        }
        private void MenuItem_delete(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem == null)
                return;
            else
            {   
                PropertyNodeItem de=((PropertyNodeItem)tree2.Items[0]).Children[last - 2];
                count--;
                cc = (title)listView1.SelectedItem;
                PropertyNodeItem cs = new PropertyNodeItem()
                {
                    DisplayName = cc.title_name,
                    Name = cc.title_name,
                    Icon = "",
                    parent =de,
                    node_lev=Nodeetype.note
                };
                isdrag = true;
                itemlist.Remove(cc);
                label4.Content = count.ToString() + "条";
                de.Children.Add(cs);
            }
        }  //删除个人主题
        private void MenuItem_deletePS(object sender, RoutedEventArgs e) //删除附件
        {
            sidefiles selectone = listView2.SelectedItem as sidefiles;
            ss.Remove(selectone);
            if (this.listView2.Items.Count == 0)
            {
                this.windowsFormsHost1.Margin = new Thickness(0, 0, 0, 0);
            }
        }
        private void MenuItem_modifyPSname(object sender, RoutedEventArgs e) //附件重命名
        {
            MessageBox.Show("MenuItem_modifyPSname(object sender, RoutedEventArgs e");
            
            Window6 w6 = new Window6();
            w6.Show();
            w6.getdata += new Window6.myevent(MenuItem_modifyPSname_event);


        }
        private void MenuItem_modifyPSname_event(object sender, Window5.textEventArgs e)
        {
            sidefiles selectone = listView2.SelectedItem as sidefiles;
            string filename = System.IO.Path.GetFileNameWithoutExtension(selectone.path);
            string Extension = System.IO.Path.GetExtension(selectone.path);
            FileInfo fi = new FileInfo(selectone.path);
            int c = selectone.path.LastIndexOf(filename);
            string path = selectone.path.Substring(0, c);
            fi.MoveTo(path+e.textbox+Extension);
            selectone.file_name = selectone.file_name.Replace(filename,e.textbox);

        }
        private void MenuItem_Saveas(object sender, RoutedEventArgs e) //附件另存为
        {
            MessageBox.Show("MenuItem_Saveas(object sender, RoutedEventArgs e");
            sidefiles selectone = listView2.SelectedItem as sidefiles;
            string exfile = System.IO.Path.GetExtension(selectone.path);
            System.Windows.Forms.SaveFileDialog sf = new System.Windows.Forms.SaveFileDialog();
            sf.Title = "附件另存为";
            sf.Filter = exfile + "文件|" + exfile + "|所有文件(*.*)|*.*";
            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.Copy(selectone.path,sf.FileName);
            }
            
        }
        private void MenuItem_openPS(object sender, RoutedEventArgs e) //打开附件
        {
            MessageBox.Show("MenuItem_openPS(object sender, RoutedEventArgs e)");
            string path = ((sidefiles)this.listView2.SelectedItem).path;
            System.Diagnostics.Process.Start(path); //打开此文件。
        }
        private void note_add_Click(object sender, RoutedEventArgs e)   //增加新笔记触发的事件
        {
            int id = listView1.Items.Count;
            title newtile = new title()
            {
                ID = id.ToString(),
                title_name = "未命名",
                date = "刚刚",
                context = "",
                image = "",

            };
            itemlist.Insert(0, newtile);
            count++;
            label4.Content = count.ToString() + "条";
        }

        private void MenuItem_tag(object sender, RoutedEventArgs e) //增加标签
        {
            //Form1 f1 = new Form1();
            //f1.getbq+=new Form1.myevent(add_tag);
          //  f1.ShowDialog();
            Window_Tag w_tag = new Window_Tag();
            w_tag.getbq += new Window_Tag.myevent(add_tag);
            w_tag.Show();
        }
        private void add_tag(object sender,Window_Tag.ListviewText e)
        {
            
           // _listviewItems = e.m_listview;
           foreach(Tag_data ta in e.m_listview)
           {
             _listviewItems.Add(ta);
           }
            this.wrapPanel3.Visibility = Visibility.Visible;
           // this.listViewTag.ItemsSource = _listviewItems;
            this.windowsFormsHost1.Margin = new Thickness(0, 28 * n(), 0, 0);
            

        }    //增加标签
        public static ImageSource Converttoimag(Icon icon)   //将图标文件转换为imageSource
        {
            //Arguments checking
            if (icon == null)
                throw new ArgumentNullException("icon", "The icon can not be null.");

            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
        public string file_length(long len)
        {
            long i =len / 1024;
            int c =(int) i / 1024;
            if(i>0&&c==0)
            {
               return i.ToString()+"K";
            }
            if(c>0)
            { 
                float c1=(float)i;
                float cc=c1/1024;
                return cc.ToString("0.00")+"M";

            }
            else
                return len.ToString()+"B";
        }     //计算文件的长度，将文件的Byte字节转换
        private int n()
        {
            if (wrapPanel1.Visibility == Visibility.Visible && wrapPanel3.Visibility == Visibility.Visible)
                return 2;
            else
                return 1;
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)   //添加附件
        {

            System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
            op.InitialDirectory = @"c:\";
            op.RestoreDirectory = true;
            op.Filter = "所有文件(*.*)|*.*";
            //op.ShowDialog();
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               // wrapPanel1.Visibility = Visibility.Visible;
                filename = op.SafeFileName;
                FileInfo fi = new FileInfo(op.FileName);
                System.Drawing.Icon cc = System.Drawing.Icon.ExtractAssociatedIcon(op.FileName);
                file_size =file_length(fi.Length);
              
                ImageSource sd = Converttoimag(cc);
                //image4.Source = sd;
               //string ccc=sd.;
                sidefiles sos = new sidefiles() { 
                 path=op.FileName,
                 file_name=filename+"("+file_size+")",
                 image=sd,
                // tool_name=filename
                };
                //this.webBrowser1.Margin = new Thickness(0, 26, 0, 0);
                //int c = n;
               // d/ouble top = Convert.ToDouble(28*c);
                this.wrapPanel1.Visibility = Visibility.Visible;
                this.windowsFormsHost1.Margin=new Thickness(0, 28*n(), 0, 0);
                //this.wrapPanel3.Visibility = Visibility.Visible;
                ss.Add(sos);
            }
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //MenuItem cc = this.menu2.Items[0] as MenuItem;
            var item = sender as MenuItem;
            Window4 w4 = new Window4();
            w4.gettype(item.Name);
            w4.createidss += new Window4.myevent(createidsst);
            w4.Show();
        }
        private void createidsst(object sender, Window1.textEventArgs e1, Window4.idssSelect e2)
        {
            
            //this.tree5.Items.Add();
             iDissertation newite = new iDissertation() { 
             Name=e1.textbox,
             icon=e2.newtitle.Pic,
             nodetype=e2.type,
             href=item_Directory+"\\"+e1.textbox 
            };
             idisser_data.idisser.TreeViewItems4_add(newite);
        }  //增加idis项

        private void tree6_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //MessageBox.Show("sadasd");
            //ThreadPool.QueueUserWorkItem(status=>savexml(tree6_sel));
            if (issave == true)
            {
                if (tree6_sel.type != outlinetype.empty && tree6_sel.Name1 != null)
                {
                    savecontext = invoker.InvokeScript("getContent").ToString();
                    savexml(tree6_sel, savecontext);

                }
            }
                try
                {
                    tree6_sel = this.tree6.SelectedItem as outline;
                    textBox2.Text = tree6_sel.Name1;
                    if (tree6_sel.type != outlinetype.common)
                    {
                        this.tree6.ContextMenu = c5;
                        switch (tree6_sel.type)
                        { 
                            case outlinetype.Section1:
                                (c5.Items[0] as MenuItem).IsEnabled = false;
                                (c5.Items[1] as MenuItem).IsEnabled = true;
                                break;
                            case outlinetype.Section2:
                                (c5.Items[0] as MenuItem).IsEnabled = false;
                                (c5.Items[1] as MenuItem).IsEnabled = false;
                                break;
                            default:
                                (c5.Items[0] as MenuItem).IsEnabled = true;
                                (c5.Items[1] as MenuItem).IsEnabled = true;
                                break;
                                
                        }
                        
                    }
                    else
                        this.tree6.ContextMenu = null;
                    // iDissertation ccc = tree5.SelectedItem as iDissertation;
                    if (tree6_sel.Name1.Contains("封面") == true || tree6_sel.nodename == "Statement")
                    {

                        //string filepath = item_Directory;
                        // cd = "../../" + filepath + "/article_xml/";
                        //string cccc = ccc.href;
                        string cd = idd_href + "/" + tree6_sel.href;
                        this.webBrowser1.Navigate(@cd);
                        web_show = true;
                    }
                    else
                    {
                        if (web_show)
                        {
                            this.webBrowser1.Navigate("file:///F:/ueditor1_3_6-src_tofuchangli/ueditor1_3_6-src/index.html");
                            web_show = false;
                        }
                        if (tree6_sel.type != outlinetype.common)
                        {
                            article dd = new article(tree6_sel.secid, "Papersection/" + tree6_sel.nodename);

                            if (invoker.WaitWebPageLoad() == true)
                            {
                                invoker.InvokeScript("setContent", dd.getcontext().Replace("&amp;", "&"));
                            }
                        }
                        else
                        {
                            article dd = new article(tree6_sel.nodename);

                            if (invoker.WaitWebPageLoad() == true)
                            {
                                invoker.InvokeScript("setContent", dd.getcontext_comm().Replace("&amp;", "&"));
                            }
                        }
                    }
                }
                catch
                {
                    if (invoker.WaitWebPageLoad() == true)
                    {
                        invoker.InvokeScript("setContent", "");
                    }
                }
            issave = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {

        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            outline sel = tree6.SelectedItem as outline;
            if (sel.nodename != "Cover" || sel.nodename != "Statement")
            {
                string html = invoker.InvokeScript("getContent").ToString();
                if (sel.type == outlinetype.common)
                {
                    Savexml cxml = new Savexml(sel.nodename, html, sel.secid);
                    cxml.savexml(true);
                }
                else
                {
                    Savexml sxml = new Savexml("Papersection/" + sel.nodename, html, sel.secid);
                    sxml.savexml(true);
                }
            }
        }

        private void tree5_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           
            if (isselect)
            {
                tree5_sel = (iDissertation)tree5.SelectedItem;
                if (tree5_sel.nodetype != iDissType.nonode)
                {
                    issave = false;
                    web_show = true;
                    listView1.Visibility = Visibility.Hidden;
                    this.tree6.Visibility = Visibility.Visible;
                    idd_href = tree5_sel.href;
                    select_tree5 = new outline_Data(true);
                    //outline_Data.Instance = select_tree5;
                    this.tree6.ItemsSource = select_tree5.TreeViewItems1;
                }
                else
                {
                    listView1.Visibility = Visibility.Visible;
                    this.tree6.Visibility = Visibility.Hidden;
                }
            }
           
        }

        private void listView2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string path = ((sidefiles)this.listView2.SelectedItem).path;
            System.Diagnostics.Process.Start(path); //打开此文件。
        }
        private void MenuItem_Click_delete(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("dasdasd");
            isdelete = false;
            _listviewItems.Remove(_listView1Select);
            isdelete = true;

        }
        private void listViewTag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isdelete)
            {
                if (_listView1Select != null)
                    _listView1Select.isvisibli = Visibility.Collapsed;
                _listView1Select = this.listViewTag.SelectedItem as Tag_data;
                _listView1Select.isvisibli = Visibility.Visible;
            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Window_Tag w_tag = new Window_Tag();
            w_tag.getbq += new Window_Tag.myevent(add_tag);
            w_tag.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
           //openexcel op = new openexcel("F:\\3.Echarts\\3.Echarts\\data.xlsx");
           // System.Data.DataTable cc = op.getexcel();
        }       
    }
}
