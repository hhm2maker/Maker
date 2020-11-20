using Maker.Business;
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
using System.Windows.Shapes;

namespace Maker.View.UI.Git
{
    /// <summary>
    /// HintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GitWindow : Window
    {
        private NewMainWindow mw;
        public GitWindow(NewMainWindow mw, List<CommitModel> commitModels)
        {
            InitializeComponent();

            this.mw = mw;

            foreach (var item in commitModels)
            {
                ListBoxItem lbi = new ListBoxItem();
                Run run = new Run
                {
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0)),
                    Text = item.FileName
                };
                lbi.Content = run;
                lbFile.Items.Add(lbi);
            }

        }

        public class CommitModel
        {
            public enum Status
            {
                Add,
                Edit,
                Delete
            }

            public Status MyStatus {
                get;
                set;
            }

            public string FileName {
                get;
                set;
            }
        }
    }
}
