using Maker.Business;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static Maker.Model.EnumCollection;

namespace Maker.View.Control
{
    public partial class MainWindow
    {
        public String lastProjectPath = String.Empty;
        public ProjectType projectType;
        public String tutorial;

        //列出项目根目录
        private void ListRoots()
        {
            if (!Directory.Exists(lastProjectPath))
            {
                return;
            }
            List<String> listStr = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(lastProjectPath);
            foreach (FileInfo driver in directory.GetFiles())
            {
                if (!driver.Name.EndsWith(".makerpj"))
                {
                    continue;
                }
                listStr.Add(driver.Name);

                bool contains = false;
                foreach (TreeViewItem item in tvProject.Items)
                {
                    if (driver.Name.Equals(item.Header))
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    tvProject.Items.Add(GetTreeViewItem(driver.Name,0));
                }
            }
            foreach (DirectoryInfo driver in directory.GetDirectories())
            {
                listStr.Add(driver.Name);

                bool contains = false;
                foreach (TreeViewItem item in tvProject.Items)
                {
                    StackPanel panel = (StackPanel)item.Header;
                    TextBlock text = (TextBlock)panel.Children[1];
                    if (driver.Name.Equals(text.Text))
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    tvProject.Items.Add(GetTreeViewItem(driver.Name, 1));
                }
            }
            for (int i = tvProject.Items.Count - 1; i >= 0; i--)
            {
                TreeViewItem item = (TreeViewItem)tvProject.Items[i];
                StackPanel panel = (StackPanel)item.Header;
                TextBlock text = (TextBlock)panel.Children[1];
                if (!listStr.Contains(text.Text))
                {
                    tvProject.Items.RemoveAt(i);
                }
            }
            for (int i = 0; i < tvProject.Items.Count; i++)
            {
                TreeViewItem node = (TreeViewItem)tvProject.Items[i];
                NodeUpdate(node);
            }
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }
        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
        private void Node_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem nodes = (TreeViewItem)sender;
            foreach (TreeViewItem node in nodes.Items) //更新所有子结点
            {
                NodeUpdate(node);
            }
            e.Handled = true;
        }

        //更新结点(列出当前目录下的子目录)
        private void NodeUpdate(TreeViewItem node)
        {
            try
            {
                //node.Items.Clear();
                //node.Header.ToString()
                StackPanel _panel = (StackPanel)node.Header;
                TextBlock _text = (TextBlock)_panel.Children[1];
                String path = _text.Text;

                var parent = node;
                while (parent is TreeViewItem)
                {
                    StackPanel panel = (StackPanel)parent.Header;
                    TextBlock text = (TextBlock)panel.Children[1];
                    if (parent == null) return;
                    if (parent.Parent is System.Windows.Controls.TreeView)
                    {
                        path = lastProjectPath + @"\" + path;
                        break;
                    }
                    else
                    {
                        path = text.Text + @"\" + path;
                        parent = (TreeViewItem)parent.Parent;
                    }
                }
                List<String> listStr = new List<string>();

                if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo f in files)
                    {
                        bool contains = false;
                        listStr.Add(f.Name);
                        foreach (TreeViewItem item in node.Items)
                        {
                            StackPanel panel = (StackPanel)item.Header;
                            TextBlock text = (TextBlock)panel.Children[1];
                            if (f.Name.Equals(text.Text))
                            {
                                contains = true;
                                break;
                            }
                        }
                        if (!contains)
                        {
                            node.Items.Add(GetTreeViewItem(f.Name, 2));
                        }

                    }
                    DirectoryInfo[] dirs = dir.GetDirectories();
                    foreach (DirectoryInfo d in dirs)
                    {
                        bool contains = false;
                        listStr.Add(d.Name);

                        foreach (TreeViewItem item in node.Items)
                        {
                            StackPanel panel = (StackPanel)item.Header;
                            TextBlock text = (TextBlock)panel.Children[1];
                            if (d.Name.Equals(text.Text))
                            {
                                contains = true;
                                break;
                            }
                        }
                        if (!contains)
                        {
                            node.Items.Add(GetTreeViewItem(d.Name,2));
                        }
                    }
                    for (int i = node.Items.Count - 1; i >= 0; i--)
                    {
                        TreeViewItem item = (TreeViewItem)node.Items[i];
                        StackPanel panel = (StackPanel)item.Header;
                        TextBlock text = (TextBlock)panel.Children[1];
                        if (!listStr.Contains(text.Text))
                        {
                            node.Items.RemoveAt(i);
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public String folderPath = String.Empty;
        public String lightScriptFilePath = String.Empty;
        public bool isSelectFile = false;
        public bool isSelect = false;
        public bool isSelectFileToFile = true;
        private void FolderNode_Selected(object sender, RoutedEventArgs e)
        {
            isSelect = true;
            TreeViewItem item = (TreeViewItem)sender;
            MenuItem menuitem = (MenuItem)lbProjectDocumentMenu.Items[0];
            Separator separator = (Separator)lbProjectDocumentMenu.Items[1];
            MenuItem menuitemImport = (MenuItem)lbProjectDocumentMenu.Items[5];
            MenuItem menuitemExport = (MenuItem)lbProjectDocumentMenu.Items[6];
            Separator separator2 = (Separator)lbProjectDocumentMenu.Items[7];
            if (isSelectFile)
            {
                isSelectFile = false;
                menuitem.Visibility = Visibility.Collapsed;
                separator.Visibility = Visibility.Collapsed;
                menuitemImport.Visibility = Visibility.Visible;
                menuitemExport.Visibility = Visibility.Visible;
                separator2.Visibility = Visibility.Visible;
                isSelectFileToFile = true;
            }
            else
            {
                isSelectFileToFile = false;
                menuitem.Visibility = Visibility.Visible;
                separator.Visibility = Visibility.Visible;
                menuitemImport.Visibility = Visibility.Collapsed;
                menuitemExport.Visibility = Visibility.Collapsed;
                separator2.Visibility = Visibility.Collapsed;
            }
            StackPanel _panel = (StackPanel)item.Header;
            TextBlock _text = (TextBlock)_panel.Children[1];
            String path = _text.Text;
            var parent = item;
            while (!(parent.Parent is System.Windows.Controls.TreeView))
            {
                parent = (TreeViewItem)parent.Parent;
                StackPanel panel = (StackPanel)parent.Header;
                TextBlock text = (TextBlock)panel.Children[1];
                if (parent == null) return;
                if (!(parent.Parent is System.Windows.Controls.TreeView))
                {
                    path = text.Text + @"\" + path;
                }
                else
                {
                    path = text.Text + @"\" + path;
                }
            }
            path = lastProjectPath + @"\" + path;
            folderPath = path;
        }
        private void Nodes_Selected(object sender, RoutedEventArgs e)
        {
            isSelect = true;
            isSelectFile = true;
            TreeViewItem item = (TreeViewItem)sender;
            StackPanel _panel = (StackPanel)item.Header;
            TextBlock _text = (TextBlock)_panel.Children[1];

            String path = _text.Text;
            //node.Header.ToString()
            var parent = item;
            while (!(parent.Parent is System.Windows.Controls.TreeView))
            {
                parent = (TreeViewItem)parent.Parent;
                StackPanel panel = (StackPanel)parent.Header;
                TextBlock text = (TextBlock)panel.Children[1];
                if (parent == null) return;
                if (!(parent.Parent is System.Windows.Controls.TreeView))
                {
                    path = text.Text + @"\" + path;
                }
                else
                {
                    path = text.Text + @"\" + path;
                }
            }
            path = lastProjectPath + @"\" + path;

            if (mode == MainWindowMode.Input) {
                iuc.RefreshData(true);
            }

            lightScriptFilePath = path;
            if (_text.Text.ToString().EndsWith(".lightScript"))
            {
                ProjectDocument_SelectionChanged_LightScript();
            }
            else if (_text.Text.ToString().EndsWith(".light"))
            {
                ProjectDocument_SelectionChanged_Light();
            }
            else if (_text.Text.ToString().EndsWith(".xml"))
            {
                ProjectDocument_SelectionChanged_Page();
            }
            else if (_text.Text.ToString().EndsWith(".makerpj"))
            {
                ProjectDocument_SelectionChanged_Makerpj();
            }
            else if (_text.Text.EndsWith(".png"))
            {

            }
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            String fileName = String.Empty;
            if (folderPath.EndsWith("LightScript"))
            {
                for (int i = 1; i < 1000000; i++)
                {
                    if (!System.IO.File.Exists(folderPath + @"\" + i + ".lightScript"))
                    {
                        fileName = folderPath + @"\" + i + ".lightScript";
                        break;
                    }
                }
            }
            else if (folderPath.EndsWith("Light"))
            {
                for (int i = 1; i < 1000000; i++)
                {
                    if (!System.IO.File.Exists(folderPath + @"\" + i + ".light"))
                    {
                        fileName = folderPath + @"\" + i + ".light";
                        break;
                    }
                }
            }
            else if (folderPath.EndsWith("Page"))
            {
                for (int i = 1; i < 1000000; i++)
                {
                    if (!System.IO.File.Exists(folderPath + @"\" + i + ".xml"))
                    {
                        fileName = folderPath + @"\" + i + ".xml";
                        break;
                    }
                }
            }
            // 判断文件是否存在，不存在则创建
            if (!fileName.Equals(String.Empty))
            {
                if (folderPath.EndsWith("Page"))
                {
                    //获取对象
                    XDocument xDoc = new XDocument();
                    // 添加根节点
                    XElement xRoot = new XElement("Page");
                    // 添加节点使用Add
                    xDoc.Add(xRoot);
                    for (int i = 0; i < 96; i++)
                    {
                        // 创建一个按钮加到root中
                        XElement xButton = new XElement("Buttons");
                        xRoot.Add(xButton);
                    }
                    // 保存该文档  
                    xDoc.Save(fileName);
                }
                else
                {
                    File.Create(fileName).Close();
                }
                TreeViewItem nodes = GetTreeViewItem(Path.GetFileName(fileName), 2);
                nodes.Selected += Nodes_Selected;
                TreeViewItem item = (TreeViewItem)tvProject.SelectedItem;
                item.Items.Add(nodes);
                nodes.IsSelected = true;
            }
        }
        
        LightScriptBusiness business = new LightScriptBusiness();
        FileBusiness fileBusiness = new FileBusiness();
        public List<Light> AllFileToLightList(String filePath) {
            List<Light> mLightList = new List<Light>();
            if (filePath.EndsWith(".lightScript"))
            {
                mLightList = business.ScriptToLightGroup(business.GetCompleteLightScript(lastProjectPath + @"\LightScript\" + filePath), "Main");
            }
            else if (filePath.EndsWith(".light"))
            {
                mLightList = fileBusiness.ReadLightFile(lastProjectPath + @"\Light\" + filePath);
            }
            else if (filePath.EndsWith(".mid"))
            {
                mLightList = fileBusiness.ReadMidiFile(lastProjectPath + @"\Midi\" + filePath);
            }
            mLightList = LightBusiness.Sort(mLightList);
            return mLightList;
        }
        private BitmapImage imgSourceSetting = new BitmapImage(new Uri("pack://application:,,,../Image/file_setting.png", UriKind.RelativeOrAbsolute));
        private BitmapImage imgSourceDirectory = new BitmapImage(new Uri("pack://application:,,,../Image/directory.png", UriKind.RelativeOrAbsolute));
        private BitmapImage imgSourceFile = new BitmapImage(new Uri("pack://application:,,,../Image/file.png", UriKind.RelativeOrAbsolute));

        /// <summary>
        /// 获得TreeViewItem
        /// </summary>
        /// <param name="text"></param>
        /// <param name="Type">类型 0 - 设置，1 - 文件夹，2 - 文件</param>
        /// <returns></returns>
        private TreeViewItem GetTreeViewItem(String text,int type)
        {
            StackPanel panel = new StackPanel();
            panel.Width = tvProject.ActualWidth - 50;
            panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
            Image image = new Image();
            image.Width = 20;
            image.Height = 20;
            if (type == 0)
            {
                image.Source = imgSourceSetting;
            }
            else if (type == 1)
            {
                image.Source = imgSourceDirectory;
            }
            else if (type == 2)
            {
                image.Source = imgSourceFile;
            }

            TextBlock block = new TextBlock();
            block.Margin = new Thickness(5, 0, 0, 0);
            if (type != 1) {
                block.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            }
            block.Text = text;
            panel.Children.Add(image);
            panel.Children.Add(block);

            TreeViewItem node = new TreeViewItem();
            node.Header = panel;
            if (type == 1)
            {
                node.Selected += FolderNode_Selected;
                node.Expanded += Node_Expanded;
            }
            else {
                node.Selected += Nodes_Selected;
            }

            return node;
        }
    }
}
