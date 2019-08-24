using Maker.View.UI.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
using System.Windows.Controls;
using System.IO;

namespace Maker.View.UI.Edit
{
    /// <summary>
    /// EditUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class EditUserControl : BaseChildUserControl
    {
        public NewMainWindow mw;
        public EditUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
            Title = "Edit";
            IsShowWindowTitle = false;
           
            InitUserControl();
        }
        public List<BaseUserControl> userControls = new List<BaseUserControl>();
        //Light
        public FrameUserControl fuc;
        public TextBoxUserControl tbuc;
        public PianoRollUserControl pruc;
        //LightScript
        public ScriptUserControl suc;
        public CodeUserControl cuc;
        //Page
        public PageMainUserControl puc;
        //Play
        public PlayExportUserControl peuc;
        //Play
        public PlayUserControl playuc;
        //Idea
        public IdeaUserControl iuc;
        //LimitlessLamp
        public LimitlessLampUserControl lluc;
        //PlayerManagement
        public PlayerManagementUserControl pmuc;
        /// <summary>
        /// 初始化用户控件
        /// </summary>
        private void InitUserControl()
        {
            //FrameUserControl
            fuc = new FrameUserControl(mw);
            userControls.Add(fuc);
            //TextBoxUserControl
            tbuc = new TextBoxUserControl(mw);
            userControls.Add(tbuc);
            //PianoRollUserControl
            pruc = new PianoRollUserControl(mw);
            userControls.Add(pruc);
            //ScriptUserControl
            suc = new ScriptUserControl(mw);
            userControls.Add(suc);
            //CodeUserControl
            cuc = new CodeUserControl(mw);
            userControls.Add(cuc);
            //PageMainUserControl 
            puc = new PageMainUserControl(mw);
            userControls.Add(puc);
            //PlayExportUserControl
            peuc = new PlayExportUserControl(mw);
            userControls.Add(peuc);
            //PlayUserControl
            playuc = new PlayUserControl(mw);
            userControls.Add(playuc);
            //PlayerUserControl
            pmuc = new PlayerManagementUserControl(mw);
            userControls.Add(pmuc);
            //LimitlessLampUserControl
            lluc = new LimitlessLampUserControl(mw);
            userControls.Add(lluc);
            //IdeaUserControl
            iuc = new IdeaUserControl(mw);
            userControls.Add(iuc);
          
        }

        public void IntoUserControl(String fileName)
        {
            if (fileName.EndsWith(".mid"))
            {
                gMain.Children.Clear();
                gMain.Children.Add(userControls[0]);
            }
            else
            {
                for (int i = 0; i < userControls.Count; i++)
                {
                    if (fileName.EndsWith(userControls[i]._fileExtension))
                    {
                        gMain.Children.Clear();
                        gMain.Children.Add(userControls[i]);
                        break;
                    }
                }
            }
            BaseUserControl baseUserControl = gMain.Children[0] as BaseUserControl;

            if (!fileName.EndsWith(".lightScript"))
            {

            }
            else
            {
                //关闭文件选择器
                //CloseFileControl();
                //baseUserControl = mw.gCenter.Children[0] as BaseUserControl;
                if (baseUserControl.filePath.Equals(mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName))
                    return;
                if (baseUserControl is ScriptUserControl)
                {
                    (baseUserControl as ScriptUserControl)._bIsEdit = false;
                }
            }
            baseUserControl.filePath = mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName;
            baseUserControl.LoadFile(fileName);

        }

    }
}
