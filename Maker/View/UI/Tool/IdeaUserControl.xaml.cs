using Maker.Business;
using Maker.Model;
using Maker.Utils;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Maker.ViewBusiness;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace Maker.View
{
    /// <summary>
    /// PlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IdeaUserControl : BaseUserControl
    {
        public IdeaUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            _fileExtension = ".idea";

            mainView = gMain;
            HideControl();
        }

        public override String GetFileDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"Idea\";
        }

        protected override void CreateFile(String filePath)
        {
            //获取对象
            XDocument xDoc = new XDocument();
            // 添加根节点
            XElement xRoot = new XElement("Root");
            // 添加节点使用Add
            xDoc.Add(xRoot);
            // 创建一个按钮加到root中
            XElement xIdeas = new XElement("Ideas");
            xRoot.Add(xIdeas);
            // 保存该文档  
            xDoc.Save(filePath);
        }
        private List<IdeaModel> ideaModels = new List<IdeaModel>();
        protected override void LoadFileContent()
        {
            foreach (XElement element in XDocument.Load(filePath).Element("Root").Element("Ideas").Elements("Idea"))
            {
                IdeaModel ideaModel = new IdeaModel();
                ideaModel.Title = element.Attribute("title").Value;
                ideaModel.Url = element.Attribute("url").Value;
                ideaModel.Content = element.Attribute("content").Value;

                ideaModels.Add(ideaModel);
            }
        }
        private new void SaveFile(object sender, RoutedEventArgs e)
        {
            XDocument xDoc = new XDocument();
            // 添加根节点
            XElement xRoot = new XElement("Root");
            // 添加节点使用Add
            xDoc.Add(xRoot);
          
            XElement xIdeas = new XElement("Ideas");
            xRoot.Add(xIdeas);

            foreach (IdeaModel ideaModel in ideaModels) {
                XElement xIdeaModel = new XElement("Idea");
                XAttribute xTitle = new XAttribute("title", ideaModel.Title);
                XAttribute xUrl = new XAttribute("url", ideaModel.Url);
                XAttribute xContent = new XAttribute("content", ideaModel.Content);
                xIdeaModel.Add(xTitle);
                xIdeaModel.Add(xUrl);
                xIdeaModel.Add(xContent);
                xIdeas.Add(xIdeaModel);
            }
            // 保存该文档  
            xDoc.Save(filePath);
        }
    }
}
