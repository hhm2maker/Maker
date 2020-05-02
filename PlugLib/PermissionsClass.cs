using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugLib
{
    public static class PermissionsClass
    {
        public enum Permissions
        {
            None = 0,
            InputAndOutput = 1,
        }

        public static string PermissionsExplain(Permissions permissions) {
            if (permissions == Permissions.None)
            {
                return "无任何权限";
            }
            else if (permissions == Permissions.InputAndOutput)
            {
                return "输入输出权限";
            }
            else {
                return "获取说明失败";
            }
        }
    }
}
