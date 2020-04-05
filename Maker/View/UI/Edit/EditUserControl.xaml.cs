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
using Maker.Business;
using Maker.Model;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Operation;

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

        public String FileName = "";
        public void IntoUserControl(String fileName)
        {
            FileName = fileName;
            mw.ShowFileName(FileName);

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

        public void IntoUserControl(String fileName,bool checkFileName)
        {
            FileName = fileName;
            mw.ShowFileName(FileName);

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
                if (baseUserControl.filePath.Equals(mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName) && checkFileName)
                    return;
                if (baseUserControl is ScriptUserControl)
                {
                    (baseUserControl as ScriptUserControl)._bIsEdit = false;
                }
            }
            baseUserControl.filePath = mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName;
            baseUserControl.LoadFile(fileName);
        }

        private List<Light> mLightList = new List<Light>();
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (gMain.Children.Count == 0 || (gMain.Children[0] as BaseUserControl).filePath.Equals(String.Empty))
            {
                if ((userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    return;
                }
                mLightList = (userControls[3] as BaseMakerLightUserControl).GetData();
            }
            else
            {
                if (userControls[userControls.IndexOf((BaseUserControl)gMain.Children[0])].IsMakerLightUserControl())
                {
                    BaseMakerLightUserControl baseMakerLightUserControl = gMain.Children[0] as BaseMakerLightUserControl;
                    mLightList = baseMakerLightUserControl.GetData();
                }
            }
            mLightList = Business.LightBusiness.Copy(mLightList);
            UserControl userControl = null;
            if (sender == iPlayer)
            {
                //DeviceModel deviceModel =  FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + playerDefault);
                //bToolChild.Width = deviceModel.DeviceSize;
                //bToolChild.Height = deviceModel.DeviceSize + 31;
                //bToolChild.Visibility = Visibility.Visible;
                //加入播放器页面
                userControl = new PlayerUserControl(mw, mLightList);
            }
            else if (sender == iPaved)
            {
                //加入平铺页面
                userControl = new ShowPavedUserControl(mw, mLightList);
            }
            else if (sender == iExport)
            {
                userControl = new ExportUserControl(mw, mLightList);
            }
            else if (sender == iPianoRoll)
            {
                userControl = new ShowPianoRollUserControl(mw, mLightList);
            }
            else if (sender == iData)
            {
                userControl = new DataGridUserControl(mw, mLightList);
            }
            else if (sender == iMy3D)
            {
                userControl = new My3DUserControl(mw, mLightList);
            }
            gTool.Children.Clear();
            gTool.Children.Add(userControl);
            gToolBackGround.Visibility = Visibility.Visible;
            DoubleAnimation daV = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            userControl.BeginAnimation(OpacityProperty, daV);
        }



        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2),
            };
            spBottomTool.BeginAnimation(Canvas.TopProperty, animation);
        }

        private void HideTool()
        {
            if (gTool.Children.Count > 0)
                return;
            DoubleAnimation animation = new DoubleAnimation
            {
                To = 40,
                Duration = TimeSpan.FromSeconds(0.2),
            };
            spBottomTool.BeginAnimation(Canvas.TopProperty, animation);
        }
        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            HideTool();
        }

        private void gToolBackGround_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RemoveTool();
        }

        /// <summary>
        /// 移除工具页面
        /// </summary>
        public void RemoveTool()
        {
            gTool.Children.RemoveAt(gTool.Children.Count - 1);
            gToolBackGround.Visibility = Visibility.Collapsed;
            HideTool();
        }
    }
}
