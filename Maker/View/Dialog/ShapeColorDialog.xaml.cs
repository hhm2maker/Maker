using Maker.Business;
using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ShapeColorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeColorDialog : Window
    {
        private int type;
        private MainWindow mw;
        public ShapeColorDialog(MainWindow mw,int type)
        {
            InitializeComponent();
            Owner = mw;
            this.type = type;
            this.mw = mw;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255,83,83,83)));
            mLaunchpad.SetSize(350);
            if (type == 0) {
                Title = "方形";
                tbHelpOne.Text = "中间";
                tbHelpTwo.Text = "外面";
                tbNumberSix.Visibility = Visibility.Collapsed;
                tbNumberSeven.Visibility = Visibility.Collapsed;
                tbNumberEight.Visibility = Visibility.Collapsed;
                tbNumberNine.Visibility = Visibility.Collapsed;
                tbNumberTen.Visibility = Visibility.Collapsed;
            }
            else if (type == 1)
            {
                Title = "垂直径向";
                tbHelpOne.Text = "上";
                tbHelpTwo.Text = "下";
            }
            else if (type == 2)
            {
                Title = "水平径向";
                tbHelpOne.Text = "左";
                tbHelpTwo.Text = "右";
            }
            FileBusiness file = new FileBusiness();
            ColorList = file.ReadColorFile(mw.strColortabPath);
        }
        private List<String> ColorList = new List<string>();
   
        /// <summary>
        /// 数字转笔刷
        /// </summary>
        /// <param name="i">颜色数值</param>
        /// <returns>SolidColorBrush笔刷</returns>
        private SolidColorBrush NumToBrush(int i)
        {
            if (i == 0)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F5"));
            }
            else
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorList[i - 1]));
            }
        }
        public String content {
            get;
            set;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbNumberOne.Text, out int numberOne))
            {
                tbNumberOne.Select(0, tbNumberOne.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberTwo.Text, out int numberTwo))
            {
                tbNumberTwo.Select(0, tbNumberTwo.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberThree.Text, out int numberThree))
            {
                tbNumberThree.Select(0, tbNumberThree.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberFour.Text, out int numberFour))
            {
                tbNumberFour.Select(0, tbNumberFour.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberFive.Text, out int numberFive))
            {
                tbNumberFour.Select(0, tbNumberFour.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberSix.Text, out int numberSix))
            {
                tbNumberSix.Select(0, tbNumberSix.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberSeven.Text, out int numberSeven))
            {
                tbNumberSeven.Select(0, tbNumberSeven.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberEight.Text, out int numberEight))
            {
                tbNumberEight.Select(0, tbNumberEight.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberNine.Text, out int numberNine))
            {
                tbNumberNine.Select(0, tbNumberNine.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberTen.Text, out int numberTen))
            {
                tbNumberTen.Select(0, tbNumberTen.Text.Length);
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(numberOne+" ");
            sb.Append(numberTwo + " ");
            sb.Append(numberThree + " ");
            sb.Append(numberFour + " ");
            sb.Append(numberFive + " ");
            if (type != 0) {
                sb.Append(numberSix + " ");
                sb.Append(numberSeven + " ");
                sb.Append(numberEight + " ");
                sb.Append(numberNine + " ");
                sb.Append(numberTen + " ");
            }
            content = sb.ToString().Trim();
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void Preview(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbNumberOne.Text, out int numberOne))
            {
                tbNumberOne.Select(0, tbNumberOne.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberTwo.Text, out int numberTwo))
            {
                tbNumberTwo.Select(0, tbNumberTwo.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberThree.Text, out int numberThree))
            {
                tbNumberThree.Select(0, tbNumberThree.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberFour.Text, out int numberFour))
            {
                tbNumberFour.Select(0, tbNumberFour.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberFive.Text, out int numberFive))
            {
                tbNumberFour.Select(0, tbNumberFour.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberSix.Text, out int numberSix))
            {
                tbNumberSix.Select(0, tbNumberSix.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberSeven.Text, out int numberSeven))
            {
                tbNumberSeven.Select(0, tbNumberSeven.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberEight.Text, out int numberEight))
            {
                tbNumberEight.Select(0, tbNumberEight.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberNine.Text, out int numberNine))
            {
                tbNumberNine.Select(0, tbNumberNine.Text.Length);
                return;
            }
            if (!int.TryParse(tbNumberTen.Text, out int numberTen))
            {
                tbNumberTen.Select(0, tbNumberTen.Text.Length);
                return;
            }

            //方形
            if (type == 0) {
                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 51, 55, 80, 84 });
                lli.Add(new List<int>() { 46, 47, 50, 54, 58, 59, 76, 77, 81, 85, 88, 89 });
                lli.Add(new List<int>() { 41, 42, 43, 45, 49, 53, 57, 61, 62, 63, 72, 73, 74, 78, 82, 86, 90, 92, 93, 94 });
                lli.Add(new List<int>() { 36, 37, 38, 39, 40, 44, 48, 52, 56, 60, 64, 65,66,67,68,69,70,71,75,79,83,87,91,95,96,97,98,99 });
                List<int> _list = new List<int>();
                for (int i = 28;i<=35;i++) {
                    _list.Add(i);
                }
                for (int i = 100; i <= 123; i++)
                {
                    _list.Add(i);
                }
                lli.Add(_list);
                if (numberOne != 0) {
                   for(int i = 0;i < lli[0].Count; i++) {
                        mLaunchpad.SetButtonBackground(lli[0][i] - 28,NumToBrush(numberOne));
                    }
                }
                if (numberTwo != 0)
                {
                    for (int i = 0; i < lli[1].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[1][i] - 28, NumToBrush(numberTwo));
                    }
                }
                if (numberThree != 0)
                {
                    for (int i = 0; i < lli[2].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[2][i] - 28, NumToBrush(numberThree));
                    }
                }
                if (numberFour != 0)
                {
                    for (int i = 0; i < lli[3].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[3][i] - 28, NumToBrush(numberFour));
                    }
                }
                if (numberFive != 0)
                {
                    for (int i = 0; i < lli[4].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[4][i] - 28, NumToBrush(numberFive));
                    }
                }
            }
            //垂直径向
            if (type == 1)
            {
                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 28, 29, 30, 31, 32, 33, 34, 35 });
                lli.Add(new List<int>() { 108, 64, 65, 66, 67, 96, 97, 98, 99, 100 });
                lli.Add(new List<int>() { 109, 60, 61, 62, 63, 92, 93, 94, 95, 101 });
                lli.Add(new List<int>() { 110, 56, 57, 58, 59, 88, 89, 90, 91, 102 });
                lli.Add(new List<int>() { 111, 52, 53, 54, 55, 84, 85, 86, 87, 103 });
                lli.Add(new List<int>() { 112, 48, 49, 50, 51, 80, 81, 82, 83, 104 });
                lli.Add(new List<int>() { 113, 44, 45, 46, 47, 76, 77, 78, 79, 105 });
                lli.Add(new List<int>() { 114, 40, 41, 42, 43, 72, 73, 74, 75, 106 });
                lli.Add(new List<int>() { 115, 36, 37, 38, 39, 68, 69, 70, 71, 107 });
                lli.Add(new List<int>() { 116, 117, 118, 119, 120, 121, 122, 123 });
                if (numberOne != 0)
                {
                    for (int i = 0; i < lli[0].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[0][i] - 28, NumToBrush(numberOne));
                    }
                }
                if (numberTwo != 0)
                {
                    for (int i = 0; i < lli[1].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[1][i] - 28, NumToBrush(numberTwo));
                    }
                }
                if (numberThree != 0)
                {
                    for (int i = 0; i < lli[2].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[2][i] - 28, NumToBrush(numberThree));
                    }
                }
                if (numberFour != 0)
                {
                    for (int i = 0; i < lli[3].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[3][i] - 28, NumToBrush(numberFour));
                    }
                }
                if (numberFive != 0)
                {
                    for (int i = 0; i < lli[4].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[4][i] - 28, NumToBrush(numberFive));
                    }
                }
                if (numberSix != 0)
                {
                    for (int i = 0; i < lli[5].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[5][i] - 28, NumToBrush(numberSix));
                    }
                }
                if (numberSeven != 0)
                {
                    for (int i = 0; i < lli[6].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[6][i] - 28, NumToBrush(numberSeven));
                    }
                }
                if (numberEight != 0)
                {
                    for (int i = 0; i < lli[7].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[7][i] - 28, NumToBrush(numberEight));
                    }
                }
                if (numberNine != 0)
                {
                    for (int i = 0; i < lli[8].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[8][i] - 28, NumToBrush(numberNine));
                    }
                }
                if (numberTen != 0)
                {
                    for (int i = 0; i < lli[9].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[9][i] - 28, NumToBrush(numberTen));
                    }
                }
            }
            //水平径向
            if (type == 2)
            {
                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 108, 109, 110, 111, 112, 113, 114, 115 });
                lli.Add(new List<int>() { 28, 64, 60, 56, 52, 48, 44, 40, 36, 116 });
                lli.Add(new List<int>() { 29, 65, 61, 57, 53, 49, 45, 41, 37, 117 });
                lli.Add(new List<int>() { 30, 66, 62, 58, 54, 50, 46, 42, 38, 118 });
                lli.Add(new List<int>() { 31, 67, 63, 59, 55, 51, 47, 43, 39, 119 });
                lli.Add(new List<int>() { 32, 96, 92, 88, 84, 80, 76, 72, 68, 120 });
                lli.Add(new List<int>() { 33, 97, 93, 89, 85, 81, 77, 73, 69, 121 });
                lli.Add(new List<int>() { 34, 98, 94, 90, 86, 82, 78, 74, 70, 122 });
                lli.Add(new List<int>() { 35, 99, 95, 91, 87, 83, 79, 75, 71, 123 });
                lli.Add(new List<int>() { 100, 101, 102, 103, 104, 105, 106, 107 });
                if (numberOne != 0)
                {
                    for (int i = 0; i < lli[0].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[0][i] - 28, NumToBrush(numberOne));
                    }
                }
                if (numberTwo != 0)
                {
                    for (int i = 0; i < lli[1].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[1][i] - 28, NumToBrush(numberTwo));
                    }
                }
                if (numberThree != 0)
                {
                    for (int i = 0; i < lli[2].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[2][i] - 28, NumToBrush(numberThree));
                    }
                }
                if (numberFour != 0)
                {
                    for (int i = 0; i < lli[3].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[3][i] - 28, NumToBrush(numberFour));
                    }
                }
                if (numberFive != 0)
                {
                    for (int i = 0; i < lli[4].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[4][i] - 28, NumToBrush(numberFive));
                    }
                }
                if (numberSix != 0)
                {
                    for (int i = 0; i < lli[5].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[5][i] - 28, NumToBrush(numberSix));
                    }
                }
                if (numberSeven != 0)
                {
                    for (int i = 0; i < lli[6].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[6][i] - 28, NumToBrush(numberSeven));
                    }
                }
                if (numberEight != 0)
                {
                    for (int i = 0; i < lli[7].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[7][i] - 28, NumToBrush(numberEight));
                    }
                }
                if (numberNine != 0)
                {
                    for (int i = 0; i < lli[8].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[8][i] - 28, NumToBrush(numberNine));
                    }
                }
                if (numberTen != 0)
                {
                    for (int i = 0; i < lli[9].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[9][i] - 28, NumToBrush(numberTen));
                    }
                }
            }
        }

        private void PasteRangeListContent(object sender, RoutedEventArgs e)
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();
                // Determines whether the data is in a format you can use.
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    // Yes it is, so display it in a text box.
                    String str = (String)iData.GetData(DataFormats.Text);
                    String[] strs = str.Split(' ');
                    if (strs.Length > 0) {
                        tbNumberOne.Text = strs[0];
                    }
                    if (strs.Length > 1)
                    {
                        tbNumberTwo.Text = strs[1];
                    }
                    if (strs.Length > 2)
                    {
                        tbNumberThree.Text = strs[2];
                    }
                    if (strs.Length > 3)
                    {
                        tbNumberFour.Text = strs[3];
                    }
                    if (strs.Length > 4)
                    {
                        tbNumberFive.Text = strs[4];
                    }
                    if (strs.Length > 5)
                    {
                        tbNumberSix.Text = strs[5];
                    }
                    if (strs.Length > 6)
                    {
                        tbNumberSeven.Text = strs[6];
                    }
                    if (strs.Length > 7)
                    {
                        tbNumberEight.Text = strs[7];
                    }
                    if (strs.Length > 8)
                    {
                        tbNumberNine.Text = strs[8];
                    }
                    if (strs.Length > 9)
                    {
                        tbNumberTen.Text = strs[9];
                    }
                }
            }
            catch {
            }
        }
    }
}
