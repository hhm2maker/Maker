using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.Online.Model
{
    /// <summary>
    /// UserInfo 的摘要说明
    /// </summary>
    public class UserInfo
    {
        public int UserId
        {
            get;
            set;
        }
        public string UserOccupation
        {
            get;
            set;
        }
        public int UserGrade
        {
            get;
            set;
        }
        /// <summary>
        /// 无参构造函数  
        /// </summary>
        public UserInfo() { }

        public UserInfo(int UserId, string UserOccupation, int UserGrade)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.UserId = UserId;
            this.UserOccupation = UserOccupation;
            this.UserGrade = UserGrade;
        }
    }
}
