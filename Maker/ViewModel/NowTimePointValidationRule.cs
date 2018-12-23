﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maker.ViewModel
{
     public class NowTimePointValidationRule : ValidationRule
    {
        public ValidationParams Params
        {
            get; set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            //时间总数
            if (int.TryParse(value.ToString(), out int _value))
            {
                if (int.TryParse(Params.Data.ToString(), out int count)) {
                    if (_value > count)
                    {
                        return new ValidationResult(false, null);//验证失败
                    }
                    else if (_value < 0)
                    {
                        return new ValidationResult(false, null);//验证失败
                    }
                    else if (_value == 0 && count>0)
                    {
                        return new ValidationResult(false, null);//验证失败
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }else {
                    return new ValidationResult(false, null);//验证失败
                }
            }
            else {
                return new ValidationResult(false, null);//验证失败
            }
        }
    }

    public class ValidationParams : DependencyObject
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
           "Data", typeof(object), typeof(ValidationParams), new FrameworkPropertyMetadata(null));

        public object Data
        {
            get { return GetValue(DataProperty); }
            set
            {
                SetValue(DataProperty, value);
            }
        }
    }

}
