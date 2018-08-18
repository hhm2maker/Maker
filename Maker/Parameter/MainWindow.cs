using Maker.Model;
using Maker.View.Online.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Maker.Model.EnumCollection;

namespace Maker.View.Control
{
    public partial class MainWindow 
    {
        //配置文件内容
        //风格
        public String strStyleOpacity = String.Empty;
        //字体和颜色
        public String[] strsInputForecolor = new String[3];
        public String strInputFontName = String.Empty;
        public String strInputFontStyle = String.Empty;
        public String strInputFontSize = String.Empty;
        public String strInputFontStrikeout = String.Empty;
        public String strInputFontUnderline = String.Empty;
        public String strInputFormatDelimiter = String.Empty;
        public String strInputFormatRange = String.Empty;
        /// <summary>
        /// 其他软件打开
        /// </summary>
        public String strToolOtherDrawingSoftwarePath = String.Empty;
        /// <summary>
        /// 是否在工作状态
        /// </summary>
        public bool bIsWork = false;
        /// <summary>
        /// 当前版本号
        /// </summary>
        public String strNowVersion = String.Empty;
        /// <summary>
        /// 是否自动更新
        /// </summary>
        public bool bIsAutoUpdate = false;
        //是否显示提示
        public bool bIsShowTip = false;
        //范围列表显示数字
        public bool bIsRangeListNumber = false;
        //颜色表路径
        public String strColortabPath = String.Empty;
        //是否显示文件管理器
        public bool bViewFileManager = true;
        //当前语言
        public String strMyLanguage = String.Empty;
        //用户名
        public String strUserName = String.Empty;
        //用户密码
        public String strUserPassword = String.Empty;
        //是否登录
        public bool isLogin = false;
        //用户对象
        public UserInfo mUser = null;
        //上传数量
        public int iUploadCount = 0;
        //画板颜色
        public Color colorInkCanvas;
        //画板图片大小
        public int iInkCanvasSize = 0;
        //播放器类型
        public PlayerType pleyerType = PlayerType.ParagraphLightList;
        //平铺列数
        public int pavedColumns = 0;
        //平铺最大个数
        public int pavedMax = 0;
    }
}
