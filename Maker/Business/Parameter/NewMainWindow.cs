using Maker.Model;
using Maker.View;
using System;
using System.Collections.Generic;
using static Maker.Model.EnumCollection;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.LightUserControl;
using Maker.View.PageWindow;
using Maker.View.Play;
using Maker.View.Tool;
using Maker.Business.Model.Config;
using System.IO;
using Maker.View.UI.Play;

namespace Maker
{
    public partial class NewMainWindow 
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        public String strMyLanguage = String.Empty;
        /// <summary>
        /// 默认播放器
        /// </summary>
        public String playerDefault;
        /// <summary>
        /// 播放器类型
        /// </summary>
        public PlayerType playerType;
        /// <summary>
        /// 设备列表
        /// </summary>
        public Dictionary<String, PlayerUserControl> playerDictionary = new Dictionary<String, PlayerUserControl>();
        /// <summary>
        /// 最后一个项目路径
        /// </summary>
        public String LastProjectPath {
            get {
                if (projectConfigModel.Path.Equals(String.Empty)
                              || !Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Project\" + projectConfigModel.Path))
                {
                    return AppDomain.CurrentDomain.BaseDirectory + @"\Project\KeyBoard\";
                }
                else
                {
                    return AppDomain.CurrentDomain.BaseDirectory + @"Project\" + projectConfigModel.Path + @"\";
                }
            }
        }
        /// <summary>
        /// 是否是第一次
        /// </summary>
        //public bool isFirst = true;
        /// <summary>
        /// 提示字典
        /// </summary>
        public Dictionary<int, HintModel> hintModelDictionary = new Dictionary<int, HintModel>();

        //public String strNowVersion;

        public IsFirstConfigModel isFirstConfigModel = new IsFirstConfigModel();
        public LanguageConfigModel languageConfigModel = new LanguageConfigModel();
        public BasicConfigModel basicConfigModel = new BasicConfigModel();
        public TestConfigModel testConfigModel = new TestConfigModel();
        public PavedConfigModel pavedConfigModel = new PavedConfigModel();
        public ProjectConfigModel projectConfigModel = new ProjectConfigModel();
        public HideConfigModel hideConfigModel = new HideConfigModel();
        public VersionConfigModel versionConfigModel = new VersionConfigModel();
        public HelpConfigModel helpConfigModel = new HelpConfigModel();
        public HintWindowConfigModel hintWindowConfigModel = new HintWindowConfigModel();
    }
}
