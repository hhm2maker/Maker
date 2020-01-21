using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Maker.View.LightUserControl;
using Operation;

namespace Maker.View.Tool
{
    /// <summary>
    /// PavedLaunchpadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExportUserControl : UserControl
    {
        private NewMainWindow mw;
        private List<Light> mLightList;
        public ExportUserControl(NewMainWindow mw,List<Light> mLightList)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
        }
        

        private void btnPaved_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnPaved.IsEnabled = false;
            DoubleAnimation daHeight = new DoubleAnimation();
                daHeight.From = 1;
                daHeight.To = 0;
                daHeight.Duration = TimeSpan.FromSeconds(0.2);

            daHeight.Completed += Board_Completed;
            wMain.BeginAnimation(OpacityProperty, daHeight);
        }

        private void Board_Completed(object sender, EventArgs e)
        {
            mw.editUserControl.RemoveTool();
        }

        private void wMain_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void ExportMidi(String fileName, bool isWriteToFile)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //设置文件类型
            if (mw.strMyLanguage.Equals("en-US"))
            {
                saveFileDialog.Filter = @"MIDI File|*.mid";
            }
            else if (mw.strMyLanguage.Equals("zh-CN"))
            {
                saveFileDialog.Filter = @"MIDI 序列|*.mid";
            }
            //设置默认文件类型显示顺序
            saveFileDialog.FilterIndex = 2;
            //默认保存名
            saveFileDialog.FileName = fileName;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //点了保存按钮进入
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Business.FileBusiness.CreateInstance().WriteMidiFile(saveFileDialog.FileName.ToString(), fileName, mLightList, isWriteToFile);
            }
        }

        private void ExportLight(String fileName)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //设置文件类型
            if (mw.strMyLanguage.Equals("en-US"))
            {
                saveFileDialog.Filter = @"Light File|*.light";
            }
            else if (mw.strMyLanguage.Equals("zh-CN"))
            {
                saveFileDialog.Filter = @"Light 文件|*.light";
            }
            //设置默认文件类型显示顺序
            saveFileDialog.FilterIndex = 2;
            //默认保存名
            saveFileDialog.FileName = fileName;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //点了保存按钮进入
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Business.FileBusiness.CreateInstance().WriteLightFile(saveFileDialog.FileName.ToString(),  mLightList);
                //bridge.ExportLight(saveFileDialog.FileName.ToString(), mActionBeanList);
            }
        }

        private void ExportUnipadLight(String fileName)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //设置文件类型
            //if (mw.strMyLanguage.Equals("en-US"))
            //{
            //    saveFileDialog.Filter = @"Unipad Light File";
            //}
            //else if (mw.strMyLanguage.Equals("zh-CN"))
            //{
            //    saveFileDialog.Filter = @"Unipad Light 文件";
            //}
            //设置默认文件类型显示顺序
            saveFileDialog.FilterIndex = 2;
            //默认保存名
            saveFileDialog.FileName = fileName;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //点了保存按钮进入
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Business.FileBusiness.CreateInstance().WriteUnipadLightFile(saveFileDialog.FileName.ToString(),int.Parse(tbBpm.Text), mLightList);
            }
        }

        private void ExportFile(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl;
            if (mw.cMost.Children.Count != 0) {
                 baseUserControl = mw.cMost.Children[0] as BaseUserControl;
            }
            else {
                baseUserControl = mw.projectUserControl.suc as BaseUserControl;
            }
            //(lbMain.SelectedItem as TreeViewItem).Header.ToString()
            //没有AB集合不能保存
            if (mLightList.Count == 0)
            {
                new MessageDialog(mw, "CanNotExportEmptyFiles").ShowDialog();
                return;
            }
            if (sender == miExportMidi)
            {
                ExportMidi(System.IO.Path.GetFileNameWithoutExtension(baseUserControl.filePath), false);
            }
            if (sender == miExportLight)
            {
                ExportLight(System.IO.Path.GetFileNameWithoutExtension(baseUserControl.filePath));
            }
            if (sender == miExportUnipadLight)
            {
                ExportUnipadLight(System.IO.Path.GetFileNameWithoutExtension(baseUserControl.filePath));
            }
            if (sender == miExportAdvanced)
            {
                AdvancedExportDialog dialog = new AdvancedExportDialog(mw, "");
                if (dialog.ShowDialog() == true)
                {
                    if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 1)
                    {
                        mLightList = LightBusiness.Split(mLightList);
                    }
                    else if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 2)
                    {
                        mLightList = LightBusiness.Splice(mLightList);
                    }
                    if (dialog.cbRemoveNotLaunchpadNumbers.IsChecked == true)
                    {
                        mLightList = LightBusiness.RemoveNotLaunchpadNumbers(mLightList);
                    }
                    if (dialog.cbCloseColorTo64.IsChecked == true)
                    {
                        mLightList = LightBusiness.CloseColorTo64(mLightList);
                    }
                    if (dialog.cbExportType.SelectedIndex == 0)
                    {
                        ExportMidi(dialog.tbFileName.Text, (bool)dialog.cbWriteToFile.IsChecked);
                    }
                    else if (dialog.cbExportType.SelectedIndex == 1)
                    {
                        ExportLight(dialog.tbFileName.Text);
                    }
                }
            }
        }

        private void ImportFile(object sender, RoutedEventArgs e)
        {
            if (sender == miMidiFile)
            {
                ImportFile(0, 0);
            }
            else
            {
                ImportFile(0, 1);
            }
            mw.editUserControl.gToolBackGround.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="inputType">输入类型0-导入，1双击Midi列表</param>
        /// <param name="type">文件类型0 - midi，1 - Light</param>
        private void ImportFile(int inputType, int type)
        {
            BaseUserControl baseUserControl = mw.cMost.Children[0] as BaseUserControl;
            
            String fileName = String.Empty;
            //文件 - 导入
            if (inputType == 0)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                if (type == 0)
                {
                    openFileDialog1.Filter = "Midi文件(*.mid)|*.mid|Midi文件(*.midi)|*.midi|All files(*.*)|*.*";
                }
                else
                {
                    openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";
                }

                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }
            //if (!fileName.Equals(String.Empty))
            //{
            //    ImportOrGetDialog dialog = null;
            //    if (type == 0)
            //    {
            //        dialog = new ImportOrGetDialog(this, fileName, 0);
            //    }
            //    else
            //    {
            //        dialog = new ImportOrGetDialog(this, fileName, 1);
            //    }
            //    if (dialog.ShowDialog() == true)
            //    {
            //        String usableStepName = dialog.UsableStepName;
            //        if (dialog.rbImport.IsChecked == true)
            //        {
            //            iuc.lightScriptDictionary.Add(usableStepName, dialog.tbImport.Text);
            //            iuc.containDictionary.Add(usableStepName, dialog.importList);
            //            //如果选择导入，则或将复制文件到灯光语句同文件夹下
            //            //判断同文件下是否有该文件
            //            if (!File.Exists(lastProjectPath + @"\Resource\" + Path.GetFileName(fileName)))
            //            {
            //                //如果不存在，则复制
            //                File.Copy(fileName, lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
            //            }
            //            else
            //            {
            //                //如果存在
            //                //先判断是否是同路径
            //                if (!Path.GetDirectoryName(fileName).Equals(lastProjectPath + @"\Resource"))
            //                {
            //                    //不是同路径
            //                    //询问是否替换
            //                    if (System.Windows.Forms.MessageBox.Show("该文件夹下已有同名文件，是否覆盖", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //                    {
            //                        //删除
            //                        File.Delete(lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
            //                        //复制
            //                        File.Copy(fileName, lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
            //                    }
            //                    else
            //                    {
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //        if (dialog.rbGet.IsChecked == true)
            //        {
            //            iuc.lightScriptDictionary.Add(usableStepName, dialog.tbGet.Text);
            //            iuc.containDictionary.Add(usableStepName, dialog.getList);
            //        }
            //        iuc.visibleDictionary.Add(usableStepName, true);
            //        iuc.AddStep(usableStepName, "");
            //        iuc.lbStep.SelectedIndex = iuc.lbStep.Items.Count - 1;
            //        iuc.RefreshData();
            //    }
            // }
        }
    }
}
