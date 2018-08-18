using Maker.View.Online.Model;
using Sharer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View.Online.Find
{
    /// <summary>
    /// ProjectMainUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectMainUserControl : UserControl
    {
        FindUserControl fuc;
        public ProjectMainUserControl(FindUserControl fuc)
        {
            InitializeComponent();
            this.fuc = fuc;
        }

        internal void SetData(ProjectInfo project)
        {
            m_tbProjectId.Text = project.ProjectId.ToString();
            m_tbProjectName.Text = project.ProjectName;
            m_tbUploader.Text = project.ProjectUploader;
            m_tbProjectRemarks.Text = project.ProjectRemarks;
            m_tbUploadTime.Text = project.UploadTime.ToString();
        }
        private void m_btnBack_Click(object sender, RoutedEventArgs e)
        {
            fuc.selTrans();
        }
        private void m_btnDownload_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Library\" + m_tbProjectName.Text + ".lightScript")) {
                System.Windows.Forms.MessageBoxButtons mssBoxBt = System.Windows.Forms.MessageBoxButtons.OKCancel;
                System.Windows.Forms.MessageBoxIcon mssIcon = System.Windows.Forms.MessageBoxIcon.Warning;
                System.Windows.Forms.MessageBoxDefaultButton mssDefbt = System.Windows.Forms.MessageBoxDefaultButton.Button1;
                System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("该目录下已有同名文件，是否覆盖？", "提示", mssBoxBt, mssIcon, mssDefbt);
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                else {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Library\" + m_tbProjectName.Text + ".lightScript");    
                }
            }
            DownloadFile("http://www.launchpadlight.com/File/LightScript/" + m_tbProjectName.Text + ".lightScript", AppDomain.CurrentDomain.BaseDirectory+@"\Library\"+m_tbProjectName.Text+".lightScript");
            System.Windows.Forms.MessageBox.Show("下载成功");
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="URL">下载文件地址</param>
        /// <param name="Filename">下载后的存放地址</param>
        public void DownloadFile(string URL, string filename)
        {
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);

                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}