using Maker.Business;
using Maker.View.UI.Git;
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

namespace Maker.View.UI.Tool
{
    /// <summary>
    /// HintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GitUserControl : UserControl
    {
        private NewMainWindow mw;
        public GitUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            git = new CommandRunner("git", mw.LastProjectPath);
        }

        public void GetVersion()
        {
            ToResult("version");
        }

        public void Commit()
        {
            string result = GetResult("status");

            var arr = result.Split(Environment.NewLine.ToCharArray());

            List<GitWindow.CommitModel> commitModels = new List<GitWindow.CommitModel>();

            for (int i = 0; i < arr.Length; i++)
            {
                //暂存区有文件
                if (arr[i].Contains("Changes not staged for commit:"))
                {
                    List<string> noCommit = new List<string>();
                    bool isStart = false;
                    for (int j = i + 1; j < arr.Length; j++) {
                        if (arr[j].Trim().Equals(string.Empty)) {
                            if (isStart) {
                                i = j;
                                break;
                            }
                            isStart = true;
                            continue;
                        }
                        if (isStart) {
                            noCommit.Add(arr[j]);
                        }
                    }
                    foreach (var item in noCommit)
                    {
                        if (item.Contains("deleted:"))
                        {
                            string[] str = item.Split(new[] { "deleted:" }, StringSplitOptions.None);
                            commitModels.Add(new GitWindow.CommitModel()
                            {
                                FileName = str[1].Trim(),
                                MyStatus = GitWindow.CommitModel.Status.Delete
                            });
                        }
                        if (item.Contains("modified:"))
                        {
                            string[] str = item.Split(new[] { "modified:" }, StringSplitOptions.None);
                            commitModels.Add(new GitWindow.CommitModel()
                            {
                                FileName = str[1].Trim(),
                                MyStatus = GitWindow.CommitModel.Status.Edit
                            });
                        }
                    }
                }

                //暂存区有未添加(Add)的文件
                if (arr[i].Contains("Untracked files:"))
                {
                    List<string> noAdd = new List<string>();
                    bool isStart = false;
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (arr[j].Trim().Equals(string.Empty))
                        {
                            if (isStart)
                            {
                                i = j;
                                break;
                            }
                            isStart = true;
                            continue;
                        }
                        if (isStart)
                        {
                            noAdd.Add(arr[j]);
                        }
                    }

                    foreach (var item in noAdd)
                    {
                        commitModels.Add(new GitWindow.CommitModel()
                        {
                            FileName = item.Trim(),
                            MyStatus = GitWindow.CommitModel.Status.Add
                        });
                    }
                }
            }

            GitWindow gitWindow = new GitWindow(mw,commitModels);
            gitWindow.ShowDialog();
        }

        CommandRunner git = null;

        private string GetResult(string line)
        {
            return git.Run(line);
        }

        private void ToResult(string line)
        {
            var result = git.Run(line);
            tbResult.Text += result + Environment.NewLine;

            mw.TextBlock_MouseLeftButtonDown_2(mw.tbGit, null);
        }
    }
}
