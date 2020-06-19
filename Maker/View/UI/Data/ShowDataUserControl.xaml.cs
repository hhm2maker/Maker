using Maker.View.LightScriptUserControl;
using Maker.View.Tool;
using Maker.View.UI.Utils;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.UI.Data
{
    /// <summary>
    /// ShowDataUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ShowDataUserControl : UserControl
    {
        NewMainWindow mw;
        public ShowDataUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
        }

        private int showModel = -1;
        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = ((sender as TextBlock).Parent) as StackPanel;
            int position = sp.Children.IndexOf((sender as TextBlock));
            if (position == showModel)
            {
                return;
            }

            if (showModel != -1)
            {
                (sp.Children[showModel] as TextBlock).Background = new SolidColorBrush(Colors.Transparent);
                (sp.Children[showModel] as TextBlock).Foreground = ResourcesUtils.Resources2Brush(this, "TitleFontColor");
            }

            showModel = position;
            (sp.Children[showModel] as TextBlock).Background = ResourcesUtils.Resources2Brush(this, "MainBottomSelect");
            (sp.Children[showModel] as TextBlock).Foreground = new SolidColorBrush(Colors.White);

            ShowData();
        }

        private void ShowData()
        {
            if (showModel == -1)
            {
                return;
            }
            List<Light> mLightList = mw.GetData();

            UserControl userControl = null;
            if (showModel == 0)
            {
                //DeviceModel deviceModel =  FileBusiness.CreateInstance().LoadDeviceModel(AppDomain.CurrentDomain.BaseDirectory + @"Device\" + playerDefault);
                //bToolChild.Width = deviceModel.DeviceSize;
                //bToolChild.Height = deviceModel.DeviceSize + 31;
                //bToolChild.Visibility = Visibility.Visible;
                //加入播放器页面
                if (!(mw.editUserControl.userControls[3] as BaseUserControl).filePath.Equals(String.Empty))
                {
                    String strAudioResources = (mw.editUserControl.userControls[3] as ScriptUserControl).AudioResources;
                    if (!strAudioResources.Contains(@"\"))
                    {
                        //说明是不完整路径
                        strAudioResources = mw.LastProjectPath + @"Audio\" + strAudioResources;
                    }
                    if (File.Exists(strAudioResources))
                    {
                        userControl = new PlayerUserControl(mw, mLightList, strAudioResources, (mw.editUserControl.userControls[3] as ScriptUserControl).nowTimeP, (mw.editUserControl.userControls[3] as ScriptUserControl).nowTimeI);
                    }
                    else
                    {
                        userControl = new PlayerUserControl(mw, mLightList);
                    }
                }
                else
                {
                    userControl = new PlayerUserControl(mw, mLightList);
                }
            }
            else if (showModel == 1)
            {
                //加入平铺页面
                userControl = new ShowPavedUserControl(mw, mLightList);
            }
            else if (showModel == 2)
            {
                userControl = new ExportUserControl(mw, mLightList);
            }
            else if (showModel == 3)
            {
                userControl = new ShowPianoRollUserControl(mw, mLightList);
            }
            else if (showModel == 4)
            {
                userControl = new DataGridUserControl(mw, mLightList);
            }
            else if (showModel == 5)
            {
                userControl = new My3DUserControl(mw, mLightList);
            }

            spBottomTool.Children.Clear();
            spBottomTool.Children.Add(userControl);
            //spPlay.Children.Clear();
            //Point point = iPlay.TranslatePoint(new Point(0, 0), this);
            //spPlay.Margin = new Thickness(0, point.Y + SystemParameters.CaptionHeight, 0, 0);
            //spPlay.Children.Add(userControl);
            //gToolBackGround.Visibility = Visibility.Visible;
        }
    }
}
