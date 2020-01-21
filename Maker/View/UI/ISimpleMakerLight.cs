using Maker.Model;
using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Maker.View
{
    public interface ISimpleMakerLight:IMakerLight
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        void SetData(List<Light> lightList);
    }
}
