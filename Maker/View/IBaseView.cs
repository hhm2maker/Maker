using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View
{
    public interface IBaseView
    {  
        /// <summary>
       /// 设置数据给视图
       /// </summary>
       /// <param name="mActionBeanList"></param>
        void SetData(List<Light> mActionBeanList);
        /// <summary>
        /// 返回数据给主视图
        /// </summary>
        /// <returns></returns>
        List<Light> GetData();
    }
}
