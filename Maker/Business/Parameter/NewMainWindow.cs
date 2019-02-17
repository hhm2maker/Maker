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
        public String lastProjectPath;
        /// <summary>
        /// 是否是第一次
        /// </summary>
        public bool isFirst = true;
        /// <summary>
        /// 平铺列数
        /// </summary>
        public int pavedColumns = 0;
        /// <summary>
        /// 平铺最大个数
        /// </summary>
        public int pavedMax = 0;
        /// <summary>
        /// 提示字典
        /// </summary>
        public Dictionary<int, HintModel> hintModelDictionary = new Dictionary<int, HintModel>();
        /// <summary>
        /// 范围列表显示数字
        /// </summary>
        public bool isRangeListNumber = false;
        /// <summary>
        /// 当前版本
        /// </summary>
        public String strNowVersion;
        /// <summary>
        /// 透明度
        /// </summary>
        public String strStyleOpacity = String.Empty;
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
        public View.UI.PlayUserControl playuc;
        //Idea
        public IdeaUserControl iuc;
        //LimitlessLamp
        public LimitlessLampUserControl lluc;
        //PlayerManagement
        public PlayerManagementUserControl pmuc;

        private List<BaseUserControl> userControls = new List<BaseUserControl>();
    }
}
