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

namespace Maker.ViewModel
{
    public class FrameUserControlViewModel : ViewModelBase
    {
            /// <summary>
            /// 构造函数
            /// </summary>
            public FrameUserControlViewModel()
            {
                Welcome = new FrameUserControlModel() { NowTimePoint = 0 };
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
