using Maker.View.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Maker.View.Introduction
{
    public class BaseIntroductionPage : UserControl
    {
        //protected CatalogUserControl cuc;
        protected int[] iPosition;
        public BaseIntroductionPage()
        {
        }
        public BaseIntroductionPage(NewMainWindow cuc, int[] iPosition)
        {
            //this.cuc = cuc;
            this.iPosition = iPosition;
            //Loaded += BaseIntroductionPage_Loaded;
        }

        protected List<Button> btnList;
        protected void SetButtonList(List<Button> btnList) {
            this.btnList = btnList;
        }

        protected void SetButtonEvent() {
            for (int i = 0; i < btnList.Count; i++) {
                btnList[i].Click += Button_Event;
            }
        }

        private void Button_Event(object sender, System.Windows.RoutedEventArgs e)
        {
            //cuc.IntoUserControl(iPosition[btnList.IndexOf(sender as Button)]); 
        }

        //private void BaseIntroductionPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    cuc.AddIntroducePage(ActualHeight);
        //}

    }
}
