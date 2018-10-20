using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    class LightGroupMethod
    {
        /// <summary>
        /// 把整个灯光平移至某个时间格
        /// </summary>
        /// <param name="lightGroup"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public static List<Light> SetStartTime(List<Light> lightGroup,int startTime)
        {
            List<Light> ll = LightBusiness.Copy(lightGroup);

            if (ll.Count == 0)
            {
                return ll;
            }
            ll = LightBusiness.Sort(ll);
            int addTime = startTime - ll[0].Time;
            for (int i = 0; i < ll.Count; i++)
            {
                ll[i].Time += addTime;
            }
            return ll;
        }
    }
}
