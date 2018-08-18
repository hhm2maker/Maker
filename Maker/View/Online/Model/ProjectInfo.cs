using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.Online.Model
{
    /// <summary>
    /// ProjectInfo 的摘要说明
    /// </summary>
    public class ProjectInfo
    {
        public int ProjectId
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public string ProjectUploader
        {
            get;
            set;
        }
        public string ProjectRemarks
        {
            get;
            set;
        }
        public DateTime UploadTime
        {
            get;
            set;
        }
        /// <summary>
        /// 无参构造函数  
        /// </summary>
        public ProjectInfo() { }

        public ProjectInfo(int ProjectId, string ProjectName, string ProjectUploader, string ProjectRemarks, DateTime UploadTime)
        {
            // TODO: 在此处添加构造函数逻辑
            this.ProjectId = ProjectId;
            this.ProjectName = ProjectName;
            this.ProjectUploader = ProjectUploader;
            this.ProjectRemarks = ProjectRemarks;
            this.UploadTime = UploadTime;
        }
    }
}
