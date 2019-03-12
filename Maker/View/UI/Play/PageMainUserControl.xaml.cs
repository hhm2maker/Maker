using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using static Maker.Model.EnumCollection;

namespace Maker.View.PageWindow
{
    /// <summary>
    /// PageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PageMainUserControl : BaseUserControl
    {
        public View view; 
        public PageMainUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            _fileExtension = ".lightPage";
            _fileType = "Play";
            mainView = gMain;
            HideControl();

            nowSelectType = PageUCSelectType.Down;
            UpdateButtonColor();
            view = DataContext as View;
        }

        bool isFirst = true;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Width = mw.ActualWidth * 0.9;
            Height = mw.gMost.ActualHeight;
            if (isFirst)
            {
                InitLaunchpad();
                isFirst = false;
            }
        }

        private void InitLaunchpad()
        {
            mLaunchpad.SetSize(600);
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromRgb(46, 48, 51)));
            mLaunchpad.SetButtonClickEvent(Button_MouseLeftButtonDown);
        }

    

        private List<List<PageButtonModel>> _pageModes = new List<List<PageButtonModel>>();


        public void LoadFileData(string filePath)
        {
            //for (int i = 0; i < 96; i++)
            //{
            //    mLaunchpad.SetButtonBackground(i, new SolidColorBrush(Color.FromArgb(255, 73, 191, 231)));
            //}
            _pageModes.Clear();
            tbPosition.Text = "-1";
            view.Count = 0;
            tbLightName.Text = "";
            tbGoto.Text = "";
            tbBpm.Text = "";

            ReadPageFile(filePath, out _pageModes);
            //for (int i = 0; i < mLaunchpad.Count; i++)
            //{
            //    if (!_lightNames[i].Equals(String.Empty))
            //    {
            //        mLaunchpad.SetButtonBackground(i, new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)));
            //    }
            //    if (!_gotos[i].Equals(String.Empty))
            //    {
            //        mLaunchpad.SetButtonBackground(i, new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)));
            //    }
            //    if (!_lightNames[i].Equals(String.Empty) && !_gotos[i].Equals(String.Empty))
            //    {
            //        mLaunchpad.SetButtonBackground(i, new SolidColorBrush(Color.FromArgb(255, 255, 0, 255)));
            //    }
            //}
        }
        protected override void LoadFileContent() {
            _pageModes.Clear();
            tbPosition.Text = "-1";
            view.Count = 0;
            tbLightName.Text = "";
            tbGoto.Text = "";
            tbBpm.Text = "";

            ReadPageFile(filePath, out _pageModes);
        }

        public void ReadPageFile(String filePath, out List<List<PageButtonModel>> pageModes)
        {
            pageModes = new List<List<PageButtonModel>>();
            foreach (XElement element in XDocument.Load(filePath).Element("Page").Elements("Buttons"))
            {
                List<PageButtonModel> _mButtons = new List<PageButtonModel>();
                foreach (XElement _element in element.Elements("Button"))
                {
                    PageButtonModel model = new PageButtonModel();
                    XElement xDown = _element.Element("Down");
                    model._down._lightName = xDown.Attribute("lightname").Value;
                    model._down._goto = xDown.Attribute("goto").Value;
                    model._down._bpm = xDown.Attribute("bpm").Value;
                    XElement xLoop = _element.Element("Loop");
                    model._loop._lightName = xLoop.Attribute("lightname").Value;
                    model._loop._goto = xLoop.Attribute("goto").Value;
                    model._loop._bpm = xLoop.Attribute("bpm").Value;
                    XElement xUp = _element.Element("Up");
                    model._up._lightName = xUp.Attribute("lightname").Value;
                    model._up._goto = xUp.Attribute("goto").Value;
                    model._up._bpm = xUp.Attribute("bpm").Value;
                    _mButtons.Add(model);
                }
                pageModes.Add(_mButtons);
            }
        }
        private bool noSaveBpm = false;
        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            noSaveBpm = true;
            int position = mLaunchpad.GetNumber((Shape)sender);
            tbPosition.Text = (position + 28).ToString();
            int count = _pageModes[position].Count;
            view.Count = count;
            RefreshContent();
        }
   

        private void ReplaceLight(object sender, RoutedEventArgs e)
        {
            if (tbCount.Text.Equals("0"))
                return;
            List<String> fileNames = new List<string>();
            FileBusiness business = new FileBusiness();
            fileNames.AddRange(business.GetFilesName(mw.LastProjectPath + @"\Light", new List<string>() { ".light" }));
            fileNames.AddRange(business.GetFilesName(mw.LastProjectPath + @"\LightScript", new List<string>() { ".lightScript" }));
            fileNames.AddRange(business.GetFilesName(mw.LastProjectPath + @"\Midi", new List<string>() { ".mid" }));
            ShowLightListDialog dialog = new ShowLightListDialog(mw, tbLightName.Text, fileNames);
            if (dialog.ShowDialog() == true)
            {
                tbLightName.Text = dialog.selectItem;
                int position = int.Parse(tbPosition.Text) - 28;
                if (nowSelectType == PageUCSelectType.Down)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._down._lightName = dialog.selectItem;
                }
                else if (nowSelectType == PageUCSelectType.Loop)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._lightName = dialog.selectItem;
                }
                else if (nowSelectType == PageUCSelectType.Up)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._up._lightName = dialog.selectItem;
                }
                //if (!_gotos[position].Equals(""))
                //{
                //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 255, 0, 255)));
                //}
                //else {
                //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)));
                //}
            }
        }
        private void ReplacePage(object sender, RoutedEventArgs e)
        {
            if (tbCount.Text.Equals("0"))
                return;
            List<String> fileNames = new List<string>();
            FileBusiness business = new FileBusiness();
            fileNames.AddRange(business.GetFilesName(mw.LastProjectPath + @"\Page", new List<string>() { ".xml" }));
            fileNames.Remove(System.IO.Path.GetFileName(filePath));
            ShowLightListDialog dialog = new ShowLightListDialog(mw, tbLightName.Text, fileNames);
            if (dialog.ShowDialog() == true)
            {
                tbGoto.Text = dialog.selectItem;
                int position = int.Parse(tbPosition.Text) - 28;
                if (nowSelectType == PageUCSelectType.Down)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._down._goto = dialog.selectItem;
                }
                else if (nowSelectType == PageUCSelectType.Loop)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._goto = dialog.selectItem;
                }
                else if (nowSelectType == PageUCSelectType.Up)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._up._goto = dialog.selectItem;
                }
                //if (!_lightNames[position].Equals(""))
                //{
                //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 255, 0, 255)));
                //}
                //else
                //{
                //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)));
                //}
            }
        }
        private void EditLight(object sender, RoutedEventArgs e)
        {
            if (tbLightName.Text.Equals(String.Empty))
                return;
            String[] str = tbLightName.Text.Split('.');
            String name = str[0];
            String extension = str[1];
            if (extension.EndsWith("light"))
            {
                SelectTreeViewItem(GetTreeViewRootItem("Light"), name, "light");
            }
            else if (extension.EndsWith("lightScript"))
            {
                SelectTreeViewItem(GetTreeViewRootItem("LightScript"), name, "lightScript");
            }
            else if (extension.EndsWith("mid"))
            {
                SelectTreeViewItem(GetTreeViewRootItem("Midi"), name, "mid");
            }
        }
        private void GotoPage(object sender, RoutedEventArgs e)
        {
            if (tbGoto.Text.Equals(String.Empty))
                return;
            String[] str = tbGoto.Text.Split('.');
            String name = str[0];
            String extension = str[1];
            if (extension.EndsWith("xml"))
            {
                SelectTreeViewItem(GetTreeViewRootItem("Page"), name, "xml");
            }
        }
        private TreeViewItem GetTreeViewRootItem(String rootName)
        {
            //TreeViewItem item = null;
            //StackPanel _panel = null;
            //TextBlock _text = null;
            //for (int i = 0; i < mw.tvProject.Items.Count; i++)
            //{
            //    item = (TreeViewItem)mw.tvProject.Items[i];
            //    _panel = (StackPanel)item.Header;
            //    _text = (TextBlock)_panel.Children[1];
            //    if (_text.Text.Equals(rootName))
            //    {
            //        return item;
            //    }
            //    //找不到该文件夹
            //    if (i == mw.tvProject.Items.Count - 1)
            //        return null;
            //}
            return null;
        }
        private void SelectTreeViewItem(TreeViewItem root, String selectName, String extension)
        {
            TreeViewItem item = null;
            StackPanel _panel = null;
            TextBlock _text = null;
            for (int i = 0; i < root.Items.Count; i++)
            {
                item = (TreeViewItem)root.Items[i];
                _panel = (StackPanel)item.Header;
                _text = (TextBlock)_panel.Children[1];
                if (_text.Text.Equals(selectName + "." + extension))
                {
                    item.IsSelected = true;
                    return;
                }
            }
        }

        private void SavePage(object sender, RoutedEventArgs e)
        {
            File.Delete(filePath);
            XDocument xDoc = new XDocument();
            // 添加根节点
            XElement xRoot = new XElement("Page");
            // 添加节点使用Add
            xDoc.Add(xRoot);
            for (int i = 0; i < _pageModes.Count; i++)
            {
                // 创建一个按钮加到root中
                XElement xButtons = new XElement("Buttons");
                for (int j = 0; j < _pageModes[i].Count; j++)
                {
                    XElement xButton = new XElement("Button");
                    //Down
                    XElement xDown = new XElement("Down");
                    XAttribute xDownLightName = new XAttribute("lightname", _pageModes[i][j]._down._lightName);
                    XAttribute xDownGoto = new XAttribute("goto", _pageModes[i][j]._down._goto);
                    XAttribute xDownBpm = new XAttribute("bpm", _pageModes[i][j]._down._bpm);
                    xDown.Add(xDownLightName);
                    xDown.Add(xDownGoto);
                    xDown.Add(xDownBpm);
                    xButton.Add(xDown);
                    //Loop
                    XElement xLoop = new XElement("Loop");
                    XAttribute xLoopLightName = new XAttribute("lightname", _pageModes[i][j]._loop._lightName);
                    XAttribute xLoopGoto = new XAttribute("goto", _pageModes[i][j]._loop._goto);
                    XAttribute xLoopBpm = new XAttribute("bpm", _pageModes[i][j]._loop._bpm);
                    xLoop.Add(xLoopLightName);
                    xLoop.Add(xLoopGoto);
                    xLoop.Add(xLoopBpm);
                    xButton.Add(xLoop);
                    //Up
                    XElement xUp = new XElement("Up");
                    XAttribute xUpLightName = new XAttribute("lightname", _pageModes[i][j]._up._lightName);
                    XAttribute xUpGoto = new XAttribute("goto", _pageModes[i][j]._up._goto);
                    XAttribute xUpBpm = new XAttribute("bpm", _pageModes[i][j]._up._bpm);
                    xUp.Add(xUpLightName);
                    xUp.Add(xUpGoto);
                    xUp.Add(xUpBpm);
                    xButton.Add(xUp);

                    xButtons.Add(xButton);
                }
                xRoot.Add(xButtons);
            }
            // 保存该文档  
            xDoc.Save(filePath);
        }

        private void MoveLight(object sender, RoutedEventArgs e)
        {
            if (tbLightName.Text.Equals(String.Empty))
                return;
            tbLightName.Text = "";
            int position = int.Parse(tbPosition.Text) - 28;
            if (nowSelectType == PageUCSelectType.Down)
            {
                _pageModes[position][int.Parse(tbCount.Text) - 1]._down._lightName = "";
            }
            else if (nowSelectType == PageUCSelectType.Loop)
            {
                _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._lightName = "";
            }
            else if (nowSelectType == PageUCSelectType.Up)
            {
                _pageModes[position][int.Parse(tbCount.Text) - 1]._up._lightName = "";
            }
            //if (!_gotos[position].Equals(""))
            //{
            //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)));
            //}
            //else
            //{
            //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));
            //}
        }
        private void MovePage(object sender, RoutedEventArgs e)
        {
            if (tbGoto.Text.Equals(String.Empty))
                return;
            tbGoto.Text = "";
            int position = int.Parse(tbPosition.Text) - 28;
            if (nowSelectType == PageUCSelectType.Down)
            {
                _pageModes[position][int.Parse(tbCount.Text) - 1]._down._goto = "";
            }
            else if (nowSelectType == PageUCSelectType.Loop)
            {
                _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._goto = "";
            }
            else if (nowSelectType == PageUCSelectType.Up)
            {
                _pageModes[position][int.Parse(tbCount.Text) - 1]._up._goto = "";
            }
            //if (!_lightNames[position].Equals(""))
            //{
            //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)));
            //}
            //else
            //{
            //    mLaunchpad.SetButtonBackground(position, new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));
            //}
        }

        private void tbBpm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (noSaveBpm)
            {
                noSaveBpm = false;
                return;
            }
            try
            {
                int position = int.Parse(tbPosition.Text) - 28;
                if (nowSelectType == PageUCSelectType.Down)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._down._bpm = tbBpm.Text;
                }
                else if (nowSelectType == PageUCSelectType.Loop)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._bpm = tbBpm.Text;
                }
                else if (nowSelectType == PageUCSelectType.Up)
                {
                    _pageModes[position][int.Parse(tbCount.Text) - 1]._up._bpm = tbBpm.Text;
                }
            }
            catch { }
        }

        private void AddCount(object sender, RoutedEventArgs e)
        {
            if (tbPosition.Text.Equals("-1"))
                return;
            int position = int.Parse(tbPosition.Text) - 28;
            _pageModes[position].Add(new PageButtonModel());
            view.Count = view.Count + 1;

            noSaveBpm = true;
            tbLightName.Text = "";
            tbGoto.Text = "";
            tbBpm.Text = "";
        }

        private void RemoveCount(object sender, RoutedEventArgs e)
        {
            if (tbPosition.Text.Equals("-1") || tbCount.Text.Equals("0"))
                return;

            int position = int.Parse(tbPosition.Text) - 28;
            _pageModes[position].RemoveAt(view.Count - 1);
            view.Count = view.Count - 1;
            noSaveBpm = true;
            RefreshContent();
        }

        private void RefreshContent()
        {
            if (view.Count == 0)
            {
                tbLightName.Text = "";
                tbGoto.Text = "";
                tbBpm.Text = "";
            }
            else
            {
                int position = int.Parse(tbPosition.Text) - 28;
                if (nowSelectType == PageUCSelectType.Down)
                {
                    tbLightName.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._down._lightName;
                    tbGoto.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._down._goto;
                    tbBpm.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._down._bpm;
                }
                else if (nowSelectType == PageUCSelectType.Loop)
                {
                    tbLightName.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._lightName;
                    tbGoto.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._goto;
                    tbBpm.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._bpm;
                }
                else if (nowSelectType == PageUCSelectType.Up)
                {
                    tbLightName.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._up._lightName;
                    tbGoto.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._up._goto;
                    tbBpm.Text = _pageModes[position][int.Parse(tbCount.Text) - 1]._up._bpm;
                }
            }
        }

        private void LastCount(object sender, RoutedEventArgs e)
        {
            if (tbPosition.Text.Equals("-1") || tbCount.Text.Equals("0"))
                return;
            if (int.Parse(tbCount.Text) == 1)
                return;
            int position = int.Parse(tbPosition.Text) - 28;
            view.Count = view.Count - 1;
            noSaveBpm = true;
            RefreshContent();
        }

        private void NextCount(object sender, RoutedEventArgs e)
        {
            if (tbPosition.Text.Equals("-1") || tbCount.Text.Equals("0"))
                return;
            int position = int.Parse(tbPosition.Text) - 28;
            if (int.Parse(tbCount.Text) == _pageModes[position].Count)
                return;
            view.Count = view.Count + 1;
            noSaveBpm = true;
            RefreshContent();
        }

        private PageUCSelectType nowSelectType;
        private void UpdateButtonColor()
        {
            if (nowSelectType == PageUCSelectType.Down)
            {
                ChangeButtonColor(btnDownButton, true);
                ChangeButtonColor(btnLoopButton, false);
                ChangeButtonColor(btnUpButton, false);
            }
            else if (nowSelectType == PageUCSelectType.Loop)
            {
                ChangeButtonColor(btnDownButton, false);
                ChangeButtonColor(btnLoopButton, true);
                ChangeButtonColor(btnUpButton, false);
            }
            else if (nowSelectType == PageUCSelectType.Up)
            {
                ChangeButtonColor(btnDownButton, false);
                ChangeButtonColor(btnLoopButton, false);
                ChangeButtonColor(btnUpButton, true);
            }
        }

        private void ChangeButtonColor(Button button, bool isSelect)
        {
            if (isSelect)
            {
                button.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
                button.Foreground = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
            }
            else
            {
                button.Background = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
                button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                button.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnDownButton)
            {
                nowSelectType = PageUCSelectType.Down;
            }
            else if (sender == btnLoopButton)
            {
                nowSelectType = PageUCSelectType.Loop;
            }
            else if (sender == btnUpButton)
            {
                nowSelectType = PageUCSelectType.Up;
            }
            UpdateButtonColor();
            RefreshContent();
        }

        protected override void CreateFile(String filePath)
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
            xDoc.Save(filePath);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            mw.RemoveChildren();
        }
    }
    public class View : INotifyPropertyChanged
    {
        public View() { }

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
