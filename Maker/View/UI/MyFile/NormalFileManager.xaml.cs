using Maker.Business;
using Maker.View.UI.Edit;
using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using Maker.View.Dialog;

namespace Maker.View.UI.MyFile
{
    /// <summary>
    /// FileManager.xaml 的交互逻辑
    /// </summary>
    public partial class NormalFileManager : UserControl
    {
        private BaseFileManager baseFileManager;
        private NewMainWindow mw;
        public NormalFileManager(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            baseFileManager = new BaseFileManager(mw);
            InitContextMenu();
            InitFile();
        }

        public void InitFile()
        {
            tvLight.Items.Clear();
            tvLightScript.Items.Clear();
            tvLimitlessLamp.Items.Clear();
            tvPage.Items.Clear();

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "Light", new List<string>() { ".light", ".mid" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                item.HorizontalContentAlignment = HorizontalAlignment.Left;
                item.VerticalContentAlignment = VerticalAlignment.Center;
                tvLight.Items.Add(item);
            }

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "LightScript", new List<string>() { ".lightScript" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                item.HorizontalContentAlignment = HorizontalAlignment.Left;
                item.VerticalContentAlignment = VerticalAlignment.Center;
                tvLightScript.Items.Add(item);
            }

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "LimitlessLamp", new List<string>() { ".limitlessLamp" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                item.HorizontalContentAlignment = HorizontalAlignment.Left;
                item.VerticalContentAlignment = VerticalAlignment.Center;
                tvLimitlessLamp.Items.Add(item);
            }

            foreach (String str in FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "Play", new List<string>() { ".lightPage" }))
            {
                TreeViewItem item = new TreeViewItem
                {
                    Header = str,
                };
                item.FontSize = 16;
                item.ContextMenu = contextMenu;
                item.HorizontalContentAlignment = HorizontalAlignment.Left;
                item.VerticalContentAlignment = VerticalAlignment.Center;
                tvPage.Items.Add(item);
            }

            if (!File.Exists(mw.LastProjectPath + @"Play\" + mw.projectConfigModel.Path + ".playExport"))
            {
                mw.editUserControl.peuc.NewFileResult2(mw.projectConfigModel.Path + ".playExport");
            }

            if(tviPlayExport == null)
            {
                tviPlayExport = new TreeViewItem
                {
                    Header = mw.projectConfigModel.Path + ".playExport",
                };
                tviPlayExport.FontSize = 16;
                tviPlayExport.ContextMenu = contextMenu;
                tviPlayExport.HorizontalContentAlignment = HorizontalAlignment.Left;
                tviPlayExport.VerticalContentAlignment = VerticalAlignment.Center;
                tvMain.Items.Add(tviPlayExport);
            }
        }

        TreeViewItem tviPlayExport;

        public ContextMenu contextMenu;
        private void InitContextMenu()
        {
            contextMenu = new ContextMenu();

            MenuItem copyMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Copy"]
            };
            copyMenuItem.Click += CopyFileName;
            contextMenu.Items.Add(copyMenuItem);

            MenuItem renameMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Rename"]
            };
            renameMenuItem.Click += RenameFileName;
            contextMenu.Items.Add(renameMenuItem);

            MenuItem deleteMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Delete"]
            };
            deleteMenuItem.Click += btnDelete_Click;
            contextMenu.Items.Add(deleteMenuItem);

            contextMenu.Items.Add(new Separator());

            MenuItem editMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["Edit"]
            };
            editMenuItem.Click += btnEdit_Click;
            contextMenu.Items.Add(editMenuItem);

            contextMenu.Items.Add(new Separator());

            MenuItem goToFileMenuItem = new MenuItem
            {
                Header = Application.Current.Resources["OpenFoldersInTheFileResourceManager"]
            };
            goToFileMenuItem.Click += GoToFile;
            contextMenu.Items.Add(goToFileMenuItem);
        }

        public void btnNew_Click(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl;
            if (sender == null)
            {
                sender = miNewLightScript;
            }

            if (sender == miNewLight)
            {
                baseUserControl = mw.editUserControl.userControls[0];
            }
            else if (sender == miNewLightScript)
            {
                baseUserControl = mw.editUserControl.userControls[3];
            }
            else if (sender == miNewLimitlessLamp)
            {
                baseUserControl = mw.editUserControl.userControls[9];
            }
            else if (sender == miNewPage)
            {
                baseUserControl = mw.editUserControl.userControls[5];
            }
            //else if (sender == miPage)
            //{
            //    baseUserControl = editUserControl.userControls[5];
            //}
            else
            {
                return;
            }
            baseUserControl.NewFile(sender, e);
        }

        private void CopyFileName(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < mw.editUserControl.userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(mw.editUserControl.userControls[i]._fileExtension))
                    {
                        baseUserControl = mw.editUserControl.userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = mw.editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            needControlBaseUserControl = baseUserControl;

            baseUserControl.filePath = needControlFileName;

            //String _filePath = baseUserControl.GetFileDirectory();
            UserControlDialog.NewFileDialog newFileDialog = new UserControlDialog.NewFileDialog(mw, true, baseUserControl._fileExtension, FileBusiness.CreateInstance().GetFilesName(baseUserControl.filePath, new List<string>() { baseUserControl._fileExtension }), baseUserControl._fileExtension, "", NewFileResult2);
            mw.ShowMakerDialog(newFileDialog);
        }

        public TreeViewItem needControlListBoxItem;
        public String needControlFileName;
        public BaseUserControl needControlBaseUserControl;
        private void GetNeedControl(object sender)
        {
            needControlListBoxItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TreeViewItem;
            needControlFileName = needControlListBoxItem.Header.ToString();
        }

        private void GoToFile(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                if (needControlFileName.EndsWith(".mid"))
                {
                    baseUserControl = mw.editUserControl.userControls[0];
                }
                else
                {
                    for (int i = 0; i < mw.editUserControl.userControls.Count; i++)
                    {
                        if (needControlFileName.EndsWith(mw.editUserControl.userControls[i]._fileExtension))
                        {
                            baseUserControl = mw.editUserControl.userControls[i];
                            break;
                        }
                    }
                }
            }
            else
            {
                baseUserControl = mw.editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            needControlBaseUserControl = baseUserControl;

            baseUserControl.filePath = needControlFileName;

            String _filePath = baseUserControl.GetFileDirectory() + baseUserControl.filePath;

            ProcessStartInfo psi;
            psi = new ProcessStartInfo("Explorer.exe")
            {

                Arguments = "/e,/select," + _filePath
            };
            Process.Start(psi);
        }

        private void RenameFileName(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < mw.editUserControl.userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(mw.editUserControl.userControls[i]._fileExtension))
                    {
                        baseUserControl = mw.editUserControl.userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = mw.editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            needControlBaseUserControl = baseUserControl;

            baseUserControl.filePath = needControlFileName;

            String _filePath = baseUserControl.GetFileDirectory();
            UserControlDialog.NewFileDialog newFileDialog = new UserControlDialog.NewFileDialog(mw, true, baseUserControl._fileExtension, FileBusiness.CreateInstance().GetFilesName(baseUserControl.filePath, new List<string>() { baseUserControl._fileExtension }), baseUserControl._fileExtension, "", NewFileResult);
            mw.ShowMakerDialog(newFileDialog);
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            EditWindow editWindow = new EditWindow(mw, needControlFileName);
            editWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            GetNeedControl(sender);
            if (mw.hintModelDictionary.ContainsKey(2))
            {
                if (mw.hintModelDictionary[2].IsHint == false)
                {
                    DeleteFile(sender, e);
                    return;
                }
            }
            HintDialog hintDialog = new HintDialog("删除文件", "您确定要删除文件？",
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    DeleteFile(_o, _e);
                    mw.RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    mw.RemoveDialog();
                },
                delegate (System.Object _o, RoutedEventArgs _e)
                {
                    mw.NotHint(2);
                });
            mw.ShowMakerDialog(hintDialog);
        }

        public void DeleteFile(object sender, RoutedEventArgs e)
        {
            BaseUserControl baseUserControl = null;
            if (!needControlFileName.EndsWith(".lightScript"))
            {
                for (int i = 0; i < mw.editUserControl.userControls.Count; i++)
                {
                    if (needControlFileName.EndsWith(mw.editUserControl.userControls[i]._fileExtension))
                    {
                        baseUserControl = mw.editUserControl.userControls[i];
                        break;
                    }
                }
            }
            else
            {
                baseUserControl = mw.editUserControl.userControls[3] as BaseUserControl;
            }

            if (baseUserControl == null)
                return;
            baseUserControl.filePath = needControlFileName;
            baseUserControl.DeleteFile(sender, e);

            if (baseUserControl == mw.editUserControl.userControls[3])
                baseUserControl.HideControl();

            mw.tbFileName.Text = String.Empty;
            ((needControlListBoxItem as TreeViewItem).Parent as ItemsControl).Items.Remove(needControlListBoxItem as TreeViewItem);
            //tvMain.Items.Remove(tvMain.SelectedItem);

            //for (int i = 0; i < lbFile.Items.Count; i++)
            //{
            //    if ((lbFile.Items[i] as ListBoxItem).Items.Contains(needControlListBoxItem))
            //    {
            //        (lbFile.Items[i] as ListBoxItem).Items.Remove(needControlListBoxItem);
            //    }
            //}
        }

        public void NewFileResult(String filePath)
        {
            mw.RemoveDialog();
            String _filePath = needControlBaseUserControl.GetFileDirectory();

            _filePath = _filePath + filePath;
            if (File.Exists(_filePath))
            {
                new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                return;
            }
            else
            {
                File.Move(mw.LastProjectPath + needControlBaseUserControl._fileType + @"\" + needControlBaseUserControl.filePath
                    , mw.LastProjectPath + needControlBaseUserControl._fileType + @"\" + filePath);
                needControlListBoxItem.Header = filePath;
                needControlBaseUserControl.filePath = filePath;
            }
        }

        public void NewFileResult2(String filePath)
        {
            mw.RemoveDialog();
            String _filePath = needControlBaseUserControl.GetFileDirectory();

            _filePath = _filePath + filePath;
            if (File.Exists(_filePath))
            {
                new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                return;
            }
            else
            {
                File.Copy(mw.LastProjectPath + needControlBaseUserControl._fileType + @"\" + needControlBaseUserControl.filePath
                   , mw.LastProjectPath + needControlBaseUserControl._fileType + @"\" + filePath);
                //needControlListBoxItem.Header = filePath;
                //needControlBaseUserControl.filePath = filePath;

                InitFile();
            }
        }


        private void Image_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            NewScript();
        }

        public void NewScript()
        {
            btnNew_Click(null, null);
        }


        private void Image_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            InitFile();
        }


        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvMain.SelectedItem == null)
            {
                return;
            }
            if ((((sender as TreeView).SelectedItem) as TreeViewItem).Parent is TreeView)
            {
                if ((sender as TreeView).SelectedItem != tviPlayExport) {
                    return;
                }
            }
            baseFileManager.InitFile((((sender as TreeView).SelectedItem) as TreeViewItem).Header.ToString());
        }

        public void NoSelected()
        {
            if (tvMain.SelectedItem == null)
            {
                return;
            }
            (tvMain.SelectedItem as TreeViewItem).IsSelected = false;
        }
    }
}
