using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.UI.MyFile
{
    class BaseFileManager
    {
        private NewMainWindow mw;
        public BaseFileManager(NewMainWindow mw)
        {
            this.mw = mw;
        }

        public void InitFile(String fileName) {
            mw.editUserControl.IntoUserControl(fileName);
        }

        public enum FileType
        {
            Light = 0,
            LightScript = 1,
            LimitlessLamp = 2,
            Play = 3
        }

        public List<String> GetFile(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Light:
                    return FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "Light", new List<string>() { ".light", ".mid" });
                case FileType.LightScript:
                    return FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "LightScript", new List<string>() { ".lightScript" });
                case FileType.LimitlessLamp:
                    return FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "LimitlessLamp", new List<string>() { ".limitlessLamp" });
                case FileType.Play:
                    return FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "Play", new List<string>() { ".lightPage" });
                default:
                    return null;
            }
        }

        public bool CheckFile(String oldFileName, String fileName,out BaseUserControl baseUserControl) {
            baseUserControl = GetNeedControlBaseUserControl(fileName);
            if (baseUserControl == null)
            {
                return false;
            }

            String _filePath = baseUserControl.GetFileDirectory();

            _filePath = _filePath + fileName;
            if (File.Exists(_filePath))
            {
                new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CopyFile(String oldFileName, String fileName) {
            if (CheckFile(oldFileName, fileName, out BaseUserControl baseUserControl))
            {
                File.Copy(mw.LastProjectPath + baseUserControl._fileType + @"\" + oldFileName
               , mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName);
                return true;
            }
            else {
                return false;
            }
          
        }

        public bool RenameFile(String oldFileName, String fileName)
        {
            if (CheckFile(oldFileName, fileName, out BaseUserControl baseUserControl))
            {
                File.Move(mw.LastProjectPath + baseUserControl._fileType + @"\" + oldFileName
               , mw.LastProjectPath + baseUserControl._fileType + @"\" + fileName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteFile(String fileName) {
            BaseUserControl baseUserControl = GetNeedControlBaseUserControl(fileName);
            if (baseUserControl == null)
                return false;
            baseUserControl.filePath = fileName;
            baseUserControl.DeleteFile(null,null);

            if (baseUserControl == mw.editUserControl.userControls[3])
                baseUserControl.HideControl();

            mw.tbFileName.Text = String.Empty;
            return true;
        }

        public bool GotoFile(String fileName) {
            BaseUserControl baseUserControl = GetNeedControlBaseUserControl(fileName);
            String _filePath = baseUserControl.GetFileDirectory() + fileName;

            ProcessStartInfo psi;
            psi = new ProcessStartInfo("Explorer.exe")
            {

                Arguments = "/e,/select," + _filePath
            };
            Process.Start(psi);
            return true;
        }

        /// <summary>
        /// 根据传入的文件名返回对应操作类
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private BaseUserControl GetNeedControlBaseUserControl(String fileName) {

            if (fileName.EndsWith(".light") || fileName.EndsWith(".mid"))
            {
                return mw.editUserControl.userControls[0];
            }
            else if (fileName.EndsWith(".lightScript"))
            {
                return mw.editUserControl.userControls[3];
            }
            else if (fileName.EndsWith(".limitlessLamp"))
            {
                return mw.editUserControl.userControls[9];
            }
            else if (fileName.EndsWith(".lightPage"))
            {
                return mw.editUserControl.userControls[8];
            }
            return null;
        }

    }
}
