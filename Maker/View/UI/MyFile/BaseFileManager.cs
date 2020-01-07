using Maker.Business;
using System;
using System.Collections.Generic;
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
    }
}
