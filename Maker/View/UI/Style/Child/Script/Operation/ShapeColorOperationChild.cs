using Maker.Business;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.LightScriptUserControl;
using MakerUI.Device;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Maker.View.UI.Style.Child.Operation
{
    public class ShapeColorOperationChild : OperationStyle
    {
        public override string Title { get; set; } = "ShapeColor";
        private ShapeColorOperationModel shapeColorOperationModel;

        private LaunchpadPro mLaunchpad = new LaunchpadPro();
        private List<TextBox> textBoxs = new List<TextBox>();
        public ShapeColorOperationChild(ShapeColorOperationModel shapeColorOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.shapeColorOperationModel = shapeColorOperationModel;

            ComboBox cb = GetComboBox(new List<string>() { "Square", "Vertical", "Horizontal" }, null);
            cb.IsEnabled = false;
            AddTitleAndControl("TypeColon", cb);

            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromRgb(43, 43, 43)));
            mLaunchpad.Size = 300;

            textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[0] + ""));
            textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[1] + ""));
            textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[2] + ""));
            textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[3] + ""));
            textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[4] + ""));

            if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.SQUARE)
            {
                cb.SelectedIndex = 0;
            }
            else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALVERTICAL)
            {
                cb.SelectedIndex = 1;

                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[5] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[6] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[7] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[8] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[9] + ""));
            }
            else if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALVERTICAL)
            {
                cb.SelectedIndex = 2;

                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[5] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[6] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[7] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[8] + ""));
                textBoxs.Add(GetTexeBox(shapeColorOperationModel.Colors[9] + ""));
            }
            GetTexeBlock(shapeColorOperationModel.TopString);

            List<FrameworkElement> frameworkElements = new List<FrameworkElement>();
            frameworkElements.Add(GetTexeBlock(shapeColorOperationModel.TopString));
            frameworkElements.AddRange(textBoxs.ToArray());
            frameworkElements.Add(GetTexeBlock(shapeColorOperationModel.BottomString));

            AddUIElement(GetDockPanel(mLaunchpad, GetVerticalStackPanel(frameworkElements))).
            AddUIElement(GetHorizontalStackPanel(new List<FrameworkElement>() { ViewBusiness.GetButton("Preview", Preview), ViewBusiness.GetButton("PasteValue", PasteRangeListContent) }));
            GetButton("Save", ToSave, out Button btn);
            AddUIElement(btn);

            CreateDialog();
        }

        private void ToSave(object sender, RoutedEventArgs e)
        {
            ToSave();
            suc.Test();
        }
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
                return StaticConstant.brushList[i - 1];
            }
        }

        public override bool ToSave()
        {
            if (!int.TryParse(textBoxs[0].Text, out int numberOne))
            {
                textBoxs[0].Select(0, textBoxs[0].Text.Length);
                return false;
            }
            if (!int.TryParse(textBoxs[1].Text, out int numberTwo))
            {
                textBoxs[1].Select(0, textBoxs[1].Text.Length);
                return false;
            }
            if (!int.TryParse(textBoxs[2].Text, out int numberThree))
            {
                textBoxs[2].Select(0, textBoxs[2].Text.Length);
                return false;
            }
            if (!int.TryParse(textBoxs[3].Text, out int numberFour))
            {
                textBoxs[3].Select(0, textBoxs[3].Text.Length);
                return false;
            }
            if (!int.TryParse(textBoxs[4].Text, out int numberFive))
            {
                textBoxs[4].Select(0, textBoxs[4].Text.Length);
                return false;
            }

            shapeColorOperationModel.Colors.Clear();
            shapeColorOperationModel.Colors.Add(numberOne);
            shapeColorOperationModel.Colors.Add(numberTwo);
            shapeColorOperationModel.Colors.Add(numberThree);
            shapeColorOperationModel.Colors.Add(numberFour);
            shapeColorOperationModel.Colors.Add(numberFive);

            if (shapeColorOperationModel.MyShapeType != ShapeColorOperationModel.ShapeType.SQUARE)
            {

                if (!int.TryParse(textBoxs[5].Text, out int numberSix))
                {
                    textBoxs[5].Select(0, textBoxs[5].Text.Length);
                    return false;
                }
                if (!int.TryParse(textBoxs[6].Text, out int numberSeven))
                {
                    textBoxs[6].Select(0, textBoxs[6].Text.Length);
                    return false;
                }
                if (!int.TryParse(textBoxs[7].Text, out int numberEight))
                {
                    textBoxs[7].Select(0, textBoxs[7].Text.Length);
                    return false;
                }
                if (!int.TryParse(textBoxs[8].Text, out int numberNine))
                {
                    textBoxs[8].Select(0, textBoxs[8].Text.Length);
                    return false;
                }
                if (!int.TryParse(textBoxs[9].Text, out int numberTen))
                {
                    textBoxs[9].Select(0, textBoxs[9].Text.Length);
                    return false;
                }

                shapeColorOperationModel.Colors.Add(numberSix);
                shapeColorOperationModel.Colors.Add(numberSeven);
                shapeColorOperationModel.Colors.Add(numberEight);
                shapeColorOperationModel.Colors.Add(numberNine);
                shapeColorOperationModel.Colors.Add(numberTen);
            }
            return true;
        }

        private void Preview(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(textBoxs[0].Text, out int numberOne))
            {
                textBoxs[0].Select(0, textBoxs[0].Text.Length);
                return;
            }
            if (!int.TryParse(textBoxs[1].Text, out int numberTwo))
            {
                textBoxs[1].Select(0, textBoxs[1].Text.Length);
                return;
            }
            if (!int.TryParse(textBoxs[2].Text, out int numberThree))
            {
                textBoxs[2].Select(0, textBoxs[2].Text.Length);
                return;
            }
            if (!int.TryParse(textBoxs[3].Text, out int numberFour))
            {
                textBoxs[3].Select(0, textBoxs[3].Text.Length);
                return;
            }
            if (!int.TryParse(textBoxs[4].Text, out int numberFive))
            {
                textBoxs[4].Select(0, textBoxs[4].Text.Length);
                return;
            }
            //方形
            if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.SQUARE)
            {
                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 44, 45, 54, 55 });
                lli.Add(new List<int>() { 33, 34, 35, 36, 43, 46, 53, 56, 63, 64, 65, 66 });
                lli.Add(new List<int>() { 22, 23, 24, 25, 26, 27, 32, 37, 42, 47, 52, 57, 62, 67, 72, 73, 74, 75, 76, 77 });
                lli.Add(new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 21, 28, 31, 38, 41, 48, 51, 58, 61, 68, 71, 78, 81, 82, 83, 84, 85, 86, 87, 88 });
                lli.Add(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 10, 19, 20, 29, 30, 39, 40, 49, 50, 59, 60, 69, 70, 79, 80, 89, 91, 92, 93, 94, 95, 96, 97, 98 });
                if (numberOne != 0)
                {
                    for (int i = 0; i < lli[0].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[0][i], NumToBrush(numberOne));
                    }
                }
                if (numberTwo != 0)
                {
                    for (int i = 0; i < lli[1].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[1][i], NumToBrush(numberTwo));
                    }
                }
                if (numberThree != 0)
                {
                    for (int i = 0; i < lli[2].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[2][i], NumToBrush(numberThree));
                    }
                }
                if (numberFour != 0)
                {
                    for (int i = 0; i < lli[3].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[3][i], NumToBrush(numberFour));
                    }
                }
                if (numberFive != 0)
                {
                    for (int i = 0; i < lli[4].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[4][i], NumToBrush(numberFive));
                    }
                }
            }
            //垂直径向
            if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALVERTICAL)
            {
                if (!int.TryParse(textBoxs[5].Text, out int numberSix))
                {
                    textBoxs[5].Select(0, textBoxs[5].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[6].Text, out int numberSeven))
                {
                    textBoxs[6].Select(0, textBoxs[6].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[7].Text, out int numberEight))
                {
                    textBoxs[7].Select(0, textBoxs[7].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[8].Text, out int numberNine))
                {
                    textBoxs[8].Select(0, textBoxs[8].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[9].Text, out int numberTen))
                {
                    textBoxs[9].Select(0, textBoxs[9].Text.Length);
                    return;
                }

                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 91, 92, 93, 94, 95, 96, 97, 98 });
                lli.Add(new List<int>() { 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 });
                lli.Add(new List<int>() { 70, 71, 72, 73, 74, 75, 76, 77, 78, 79 });
                lli.Add(new List<int>() { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 });
                lli.Add(new List<int>() { 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 });
                lli.Add(new List<int>() { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 });
                lli.Add(new List<int>() { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 });
                lli.Add(new List<int>() { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 });
                lli.Add(new List<int>() { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 });
                lli.Add(new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
                if (numberOne != 0)
                {
                    for (int i = 0; i < lli[0].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[0][i], NumToBrush(numberOne));
                    }
                }
                if (numberTwo != 0)
                {
                    for (int i = 0; i < lli[1].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[1][i], NumToBrush(numberTwo));
                    }
                }
                if (numberThree != 0)
                {
                    for (int i = 0; i < lli[2].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[2][i], NumToBrush(numberThree));
                    }
                }
                if (numberFour != 0)
                {
                    for (int i = 0; i < lli[3].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[3][i], NumToBrush(numberFour));
                    }
                }
                if (numberFive != 0)
                {
                    for (int i = 0; i < lli[4].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[4][i], NumToBrush(numberFive));
                    }
                }
                if (numberSix != 0)
                {
                    for (int i = 0; i < lli[5].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[5][i], NumToBrush(numberSix));
                    }
                }
                if (numberSeven != 0)
                {
                    for (int i = 0; i < lli[6].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[6][i], NumToBrush(numberSeven));
                    }
                }
                if (numberEight != 0)
                {
                    for (int i = 0; i < lli[7].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[7][i], NumToBrush(numberEight));
                    }
                }
                if (numberNine != 0)
                {
                    for (int i = 0; i < lli[8].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[8][i], NumToBrush(numberNine));
                    }
                }
                if (numberTen != 0)
                {
                    for (int i = 0; i < lli[9].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[9][i], NumToBrush(numberTen));
                    }
                }
            }
            //水平径向
            if (shapeColorOperationModel.MyShapeType == ShapeColorOperationModel.ShapeType.RADIALHORIZONTAL)
            {
                if (!int.TryParse(textBoxs[5].Text, out int numberSix))
                {
                    textBoxs[5].Select(0, textBoxs[5].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[6].Text, out int numberSeven))
                {
                    textBoxs[6].Select(0, textBoxs[6].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[7].Text, out int numberEight))
                {
                    textBoxs[7].Select(0, textBoxs[7].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[8].Text, out int numberNine))
                {
                    textBoxs[8].Select(0, textBoxs[8].Text.Length);
                    return;
                }
                if (!int.TryParse(textBoxs[9].Text, out int numberTen))
                {
                    textBoxs[9].Select(0, textBoxs[9].Text.Length);
                    return;
                }

                List<List<int>> lli = new List<List<int>>();
                lli.Add(new List<int>() { 10, 20, 30, 40, 50, 60, 70, 80 });
                lli.Add(new List<int>() { 1, 11, 21, 31, 41, 51, 61, 71, 81, 91 });
                lli.Add(new List<int>() { 2, 12, 22, 32, 42, 52, 62, 72, 82, 92 });
                lli.Add(new List<int>() { 3, 13, 23, 33, 43, 53, 63, 73, 83, 93 });
                lli.Add(new List<int>() { 4, 14, 24, 34, 44, 54, 64, 74, 84, 94 });
                lli.Add(new List<int>() { 5, 15, 25, 35, 45, 55, 65, 75, 85, 95 });
                lli.Add(new List<int>() { 6, 16, 26, 36, 46, 56, 66, 76, 86, 96 });
                lli.Add(new List<int>() { 7, 17, 27, 37, 47, 57, 67, 77, 87, 97 });
                lli.Add(new List<int>() { 8, 18, 28, 38, 48, 58, 68, 78, 88, 98 });
                lli.Add(new List<int>() { 19, 29, 39, 49, 59, 69, 79, 89 });

                if (numberOne != 0)
                {
                    for (int i = 0; i < lli[0].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[0][i], NumToBrush(numberOne));
                    }
                }
                if (numberTwo != 0)
                {
                    for (int i = 0; i < lli[1].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[1][i], NumToBrush(numberTwo));
                    }
                }
                if (numberThree != 0)
                {
                    for (int i = 0; i < lli[2].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[2][i], NumToBrush(numberThree));
                    }
                }
                if (numberFour != 0)
                {
                    for (int i = 0; i < lli[3].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[3][i], NumToBrush(numberFour));
                    }
                }
                if (numberFive != 0)
                {
                    for (int i = 0; i < lli[4].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[4][i], NumToBrush(numberFive));
                    }
                }
                if (numberSix != 0)
                {
                    for (int i = 0; i < lli[5].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[5][i], NumToBrush(numberSix));
                    }
                }
                if (numberSeven != 0)
                {
                    for (int i = 0; i < lli[6].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[6][i], NumToBrush(numberSeven));
                    }
                }
                if (numberEight != 0)
                {
                    for (int i = 0; i < lli[7].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[7][i], NumToBrush(numberEight));
                    }
                }
                if (numberNine != 0)
                {
                    for (int i = 0; i < lli[8].Count; i++)
                    {
                        mLaunchpad.SetButtonBackground(lli[8][i], NumToBrush(numberNine));
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
                    if (strs.Length > 0)
                    {
                        textBoxs[0].Text = strs[0];
                    }
                    if (strs.Length > 1)
                    {
                        textBoxs[1].Text = strs[1];
                    }
                    if (strs.Length > 2)
                    {
                        textBoxs[2].Text = strs[2];
                    }
                    if (strs.Length > 3)
                    {
                        textBoxs[3].Text = strs[3];
                    }
                    if (strs.Length > 4)
                    {
                        textBoxs[4].Text = strs[4];
                    }
                    if (strs.Length > 5)
                    {
                        textBoxs[5].Text = strs[5];
                    }
                    if (strs.Length > 6)
                    {
                        textBoxs[6].Text = strs[6];
                    }
                    if (strs.Length > 7)
                    {
                        textBoxs[7].Text = strs[7];
                    }
                    if (strs.Length > 8)
                    {
                        textBoxs[8].Text = strs[8];
                    }
                    if (strs.Length > 9)
                    {
                        textBoxs[8].Text = strs[9];
                    }
                }
            }
            catch
            {
            }
        }
    }
}
