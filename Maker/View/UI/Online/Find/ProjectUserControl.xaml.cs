using Maker.View.Online.Model;
using Newtonsoft.Json;
using Sharer.Utils;
using System;
using System.Collections.Generic;
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
    /// ProjectUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectUserControl : UserControl
    {
        public ProjectUserControl()
        {
            InitializeComponent();
        }
        private List<ProjectInfo> listProjectInfo = new List<ProjectInfo>();
        private bool IsFirst = true;
        private FindUserControl fuc;
        public ProjectUserControl(FindUserControl fuc)
        {
            InitializeComponent();
            this.fuc = fuc;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsFirst)
            {
                LoadToList(true, 0);
                IsFirst = !IsFirst;
                m_dgProjectMain.ItemsSource = listProjectInfo;

            }
        }
        private void LoadToList(bool IsChangePage, int Page)
        {
            listProjectInfo.Clear();

            string paraUrlCoded = System.Web.HttpUtility.UrlEncode("Page");
            paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(Page.ToString());
            string result = NoFileRequestUtils.NoFilePostRequest("http://www.launchpadlight.com/sharer/SelectProjectAll", paraUrlCoded);

            if (result.StartsWith("success:"))
            {
                listProjectInfo = JsonConvert.DeserializeObject<List<ProjectInfo>>(result.Substring(14));
            
                //foreach(ProjectInfo p in listProjectInfo) {
                //    if (p.ProjectRemarks.Length > 30)
                //    {
                //        p.ProjectRemarks = p.ProjectRemarks.Substring(0, 30);
                //    }
                //    else {
                //        p.ProjectRemarks = p.ProjectRemarks.PadRight(' ');
                //    }
                //}

                int count = int.Parse(result.Substring(8, 6));
                //如果需要改变底栏
                if (IsChangePage)
                {
                    if (count <= 20)
                    {
                        tbPageNow.Text = 1.ToString();
                        tbPageCount.Text = 1.ToString();
                    }
                    else
                    {
                        tbPageNow.Text = 1.ToString();
                        if (count % 20 == 0)
                        {
                            tbPageCount.Text = (count / 20).ToString();
                        }
                        else
                        {
                            tbPageCount.Text = (count / 20 + 1).ToString();
                        }
                    }
                }

            }
            m_dgProjectMain.ItemsSource = listProjectInfo;
        }



        private void tbLastPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(tbPageNow.Text) <= 1)
            {
                return;
            }
            LoadToList(true, int.Parse(tbPageNow.Text) - 1 - 1);
            tbPageNow.Text = (int.Parse(tbPageNow.Text)).ToString();
        }

        private void tbNextPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(tbPageNow.Text) >= int.Parse(tbPageCount.Text))
            {
                return;
            }
            LoadToList(true, int.Parse(tbPageNow.Text));
            tbPageNow.Text = (int.Parse(tbPageNow.Text) + 1).ToString();
        }

        private void tbRefresh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoadToList(true, 0);
        }



        private void m_dgProjectMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (m_dgProjectMain.SelectedIndex == -1)
            {
                return;
            }
            fuc.selTrans();
            fuc.pmuc.SetData(listProjectInfo[m_dgProjectMain.SelectedIndex]);




        }
    }
}
