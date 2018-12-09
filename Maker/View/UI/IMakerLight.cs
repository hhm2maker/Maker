using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Maker.View
{
    public interface IMakerLight
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        List<Light>  GetData();
    }
}
