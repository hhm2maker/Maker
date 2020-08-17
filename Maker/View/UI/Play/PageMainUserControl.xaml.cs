using Maker.Business;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Dialog;
using Maker.View.Style;
using Maker.View.UI.Style;
using Operation.Model;
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
        private PlayStyleWindow psw;
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

            psw = new PlayStyleWindow(this);
            spRight.Children.Add(psw);
        }

        bool isFirst = true;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Height = mw.gMost.ActualHeight;
            if (isFirst)
            {
                InitLaunchpad();
                isFirst = false;
            }
        }

        private void InitLaunchpad()
        {
            mLaunchpad.Size = 600;
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromRgb(46, 48, 51)));
            mLaunchpad.SetButtonClickEvent(Button_MouseLeftButtonDown);
        }

        public List<List<PageButtonModel>> _pageModes = new List<List<PageButtonModel>>();

        public void LoadFileData(string filePath)
        {
            //for (int i = 0; i < 96; i++)
            //{
            //    mLaunchpad.SetButtonBackground(i, new SolidColorBrush(Color.FromArgb(255, 73, 191, 231)));
            //}
            _pageModes.Clear();
            tbPosition.Text = "-1";
            view.Count = 0;

            ReadPageFile(filePath, out _pageModes);
        }

        protected override void LoadFileContent()
        {
            _pageModes.Clear();
            view.Count = 0;

            ReadPageFile(filePath, out _pageModes);

            InitPosition(11);
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
                    {
                        foreach (var xEdit in xDown.Elements())
                        {
                            model._down.OperationModels.Add(XNameToModel(xEdit));
                        }
                    }
                    XElement xLoop = _element.Element("Loop");
                    {
                        foreach (var xEdit in xLoop.Elements())
                        {
                            model._loop.OperationModels.Add(XNameToModel(xEdit));
                        }
                    }

                    XElement xUp = _element.Element("Up");
                    {
                        foreach (var xEdit in xUp.Elements())
                        {
                            model._up.OperationModels.Add(XNameToModel(xEdit));
                        }
                    }
                    _mButtons.Add(model);
                }
                pageModes.Add(_mButtons);
            }
        }

        private BaseOperationModel XNameToModel(XElement xEdit)
        {
            BaseOperationModel baseOperationModel = null;
            if (xEdit.Name.ToString().Equals("LightFile"))
            {
                baseOperationModel = new LightFilePlayModel();
            }
            else if (xEdit.Name.ToString().Equals("GotoPage"))
            {
                baseOperationModel = new GotoPagePlayModel();
            }
            else if (xEdit.Name.ToString().Equals("AudioFile"))
            {
                baseOperationModel = new AudioFilePlayModel();
            }
            baseOperationModel.SetXElement(xEdit);
            return baseOperationModel;
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            position = mLaunchpad.GetNumber((Shape)sender);
            InitPosition(position);
        }

        private void InitPosition(int position)
        {
            tbPosition.Text = (position).ToString();
            int count = _pageModes[position].Count;
            view.Count = count;
            RefreshContent();
        }

        private void SavePage(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SavePage(filePath, _pageModes);
        }

        public void SavePage(String filePath, List<List<PageButtonModel>> _pageModes) {
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
                    {
                        foreach (var mItem in _pageModes[i][j]._down.OperationModels)
                        {
                            xDown.Add(mItem.GetXElement());
                        }
                    }
                    xButton.Add(xDown);
                    //Loop
                    XElement xLoop = new XElement("Loop");
                    {
                        foreach (var mItem in _pageModes[i][j]._loop.OperationModels)
                        {
                            xLoop.Add(mItem.GetXElement());
                        }
                    }
                    xButton.Add(xLoop);
                    //Up
                    XElement xUp = new XElement("Up");
                    {

                        foreach (var mItem in _pageModes[i][j]._up.OperationModels)
                        {
                            xUp.Add(mItem.GetXElement());
                        }
                    }
                    xButton.Add(xUp);

                    xButtons.Add(xButton);
                }
                xRoot.Add(xButtons);
            }
            // 保存该文档  
            xDoc.Save(filePath);
        }

        private void AddCount(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AddCount();
        }

        private void AddCount()
        {
            if (tbPosition.Text.Equals("-1"))
                return;
            int position = int.Parse(tbPosition.Text);
            _pageModes[position].Add(new PageButtonModel());
            view.Count = view.Count + 1;
            RefreshContent();
        }

        private void RemoveCount(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbPosition.Text.Equals("-1") || tbCount.Text.Equals("0"))
                return;

            int position = int.Parse(tbPosition.Text);
            _pageModes[position].RemoveAt(view.Count - 1);
            view.Count = view.Count - 1;
            RefreshContent();
        }

        int position;

        public void RefreshContent()
        {
            if (view.Count == 0)
            {
                psw.SetData(new List<BaseOperationModel>());
            }
            else
            {
                position = int.Parse(tbPosition.Text);
                if (nowSelectType == PageUCSelectType.Down)
                {
                    psw.SetData(_pageModes[position][int.Parse(tbCount.Text) - 1]._down.OperationModels);
                }
                else if (nowSelectType == PageUCSelectType.Loop)
                {
                    psw.SetData(_pageModes[position][int.Parse(tbCount.Text) - 1]._loop.OperationModels);
                }
                else if (nowSelectType == PageUCSelectType.Up)
                {
                    psw.SetData(_pageModes[position][int.Parse(tbCount.Text) - 1]._up.OperationModels);
                }
            }

            for (int i = 0; i < _pageModes.Count; i++)
            {

                if (_pageModes[i].Count == 0)
                {
                    mLaunchpad.SetButtonBackground(i, 0);
                }
                else
                {
                    mLaunchpad.SetButtonBackground(i, 5);
                }
            }
        }

        private void LastCount(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbPosition.Text.Equals("-1") || tbCount.Text.Equals("0"))
                return;
            if (int.Parse(tbCount.Text) == 1)
                return;
            int position = int.Parse(tbPosition.Text);
            view.Count = view.Count - 1;
            RefreshContent();
        }

        private void NextCount(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbPosition.Text.Equals("-1") || tbCount.Text.Equals("0"))
                return;
            int position = int.Parse(tbPosition.Text);
            if (int.Parse(tbCount.Text) == _pageModes[position].Count)
                return;
            view.Count = view.Count + 1;
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
            for (int i = 0; i < 100; i++)
            {
                // 创建一个按钮加到root中
                XElement xButton = new XElement("Buttons");
                xRoot.Add(xButton);
            }
            // 保存该文档  
            xDoc.Save(filePath);
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (_pageModes[position].Count == 0)
            {
                AddCount();
                //return;
            }
            BaseButtonModel bbm;
            switch (nowSelectType)
            {
                case PageUCSelectType.Down:
                    bbm = _pageModes[position][int.Parse(tbCount.Text) - 1]._down;
                    break;
                case PageUCSelectType.Loop:
                    bbm = _pageModes[position][int.Parse(tbCount.Text) - 1]._loop;
                    break;
                case PageUCSelectType.Up:
                    bbm = _pageModes[position][int.Parse(tbCount.Text) - 1]._up;
                    break;
                default:
                    bbm = _pageModes[position][int.Parse(tbCount.Text) - 1]._down;
                    break;
            }

            if (sender == btnAddLight)
            {
                bbm.OperationModels.Add(new LightFilePlayModel("", mw.NowProjectModel.Bpm));
            }
            else if (sender == btnAddAudio)
            {
                bbm.OperationModels.Add(new AudioFilePlayModel());
            }
            else if (sender == btnAddGoto)
            {
                bbm.OperationModels.Add(new GotoPagePlayModel());
            }
            RefreshContent();
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
