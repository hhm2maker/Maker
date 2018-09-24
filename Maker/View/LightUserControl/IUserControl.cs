using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Maker.View.LightWindow
{
    public interface IUserControl
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        void SetData(List<Light> lightList);
        /// <summary>
        /// 获取数据
        /// </summary>
        List<Light>  GetData();

    }
}
