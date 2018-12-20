using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Model;
using Maker.View.LightUserControl;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight.Command;

namespace Maker.ViewModel
{
    public class FrameUserControlViewModel : ViewModelBase
    {
            /// <summary>
            /// 构造函数
            /// </summary>
            public FrameUserControlViewModel()
            {
                Welcome = new FrameUserControlModel() { };
            }


        private FrameUserControlModel welcome;
            /// <summary>
            /// 欢迎词属性
            /// </summary>
            public FrameUserControlModel Welcome
            {
                get { return welcome; }
                set { welcome = value; RaisePropertyChanged(() => Welcome); }
            }

        private RelayCommand<String> changeNowTimePointCmd;
          /// <summary>
        /// 执行提交命令的方法
        /// </summary>
        public RelayCommand<String> ChangeNowTimePointCmd
        {
            get
            {
                if (changeNowTimePointCmd == null) return new RelayCommand<String>((p) => ChangeNowTimePoint(p));
                return changeNowTimePointCmd;
            }
            set { changeNowTimePointCmd = value; }
        }

          /// <summary>
        /// 执行提交方法
        /// </summary>
        private void ChangeNowTimePoint(String str)
        {
            if (str.Equals("Left")) { 
            if (welcome.NowTimePoint <= 1) return;
            welcome.NowTimePoint--;
            }
            else if (str.Equals("Right"))
            {
                if (welcome.NowTimePoint > welcome.NowData.Count - 1) return;
                welcome.NowTimePoint++;
            }
            LoadFrame();
        }

        private void LoadFrame() {
            if (welcome.NowTimePoint == 0)
                return;

            welcome.CurrentFrame = welcome.LiTime[welcome.NowTimePoint - 1];

            List<Light> mLightList = new List<Light>();

            int[] x = welcome.NowData[welcome.LiTime[welcome.NowTimePoint - 1]];
            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i] == 0)
                {
                    continue;
                }
                mLightList.Add(new Light(0, 144, i + 28, x[i]));
            }
            Welcome.NowLightLight = mLightList;
        }
    }

public class NowTimePointRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int result))
            {
                value = result;
            }
            else
            {
                value = 0;
            }
            return new ValidationResult(true, null);
        }
    }


}
