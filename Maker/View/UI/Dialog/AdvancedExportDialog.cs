using Maker.View.Control;
using Maker.ViewBusiness;
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

namespace Maker.View.Dialog
{
    /// <summary>
    /// AdvancedExportDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AdvancedExportDialog : BaseDialog
    {
        private String fileName;
        public TextBox tbFileName;
        public CheckBox cbWriteToFile, cbRemoveNotLaunchpadNumbers, cbCloseColorTo64;
        public ComboBox cbExportType, cbDisassemblyOrSplicingColon;
        public AdvancedExportDialog(NewMainWindow mw, String fileName)
        {
            Owner = mw;
            this.fileName = fileName;
            //构建对话框
            AddTopHintTextBlock("FileNameColon");
            AddTextBox();
            AddCheckBox("FileNameWriteToFile", false);
            AddTopHintTextBlock("ExportTypeColon");
            AddComboBox(new List<string>() { "MidiFile", "LightFile" }, cbExportType_SelectionChanged);
            AddTopHintTextBlock("DisassemblyOrSplicingColon");
            AddComboBox(new List<string>() { "None", "Disassembly", "Splicing" }, null);
            StackPanel spRemove = new StackPanel();
            spRemove.Orientation = Orientation.Horizontal;
            cbRemoveNotLaunchpadNumbers = UIViewBusiness.GetCheckBox("RemoveNotLaunchpadNumbers", true);
            cbRemoveNotLaunchpadNumbers.Width = 280;
            spRemove.Children.Add(cbRemoveNotLaunchpadNumbers);
            Image imgRemove = new Image
            {
                Width = 15
            };
            RenderOptions.SetBitmapScalingMode(imgRemove, BitmapScalingMode.Fant);
            imgRemove.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/ask.png", UriKind.RelativeOrAbsolute));
            imgRemove.Margin = new Thickness(5, 20, 0, 0);
            ToolTip ttImgRemove = new ToolTip();
            StackPanel spTtImgRemove = new StackPanel();
            spTtImgRemove.Background = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
            spTtImgRemove.Margin = new Thickness(-7, -5, -7, -5);
            spTtImgRemove.Orientation = Orientation.Vertical;
            TextBlock tbSpTtImgRemovePosition = new TextBlock();
            tbSpTtImgRemovePosition.FontSize = 14;
            tbSpTtImgRemovePosition.Margin = new Thickness(10, 10, 10, 5);
            tbSpTtImgRemovePosition.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            tbSpTtImgRemovePosition.SetResourceReference(TextBlock.TextProperty, "LaunchpadNumbersPosition");
            spTtImgRemove.Children.Add(tbSpTtImgRemovePosition);
            TextBlock tbSpTtImgRemoveColor = new TextBlock();
            tbSpTtImgRemoveColor.FontSize = 14;
            tbSpTtImgRemoveColor.Margin = new Thickness(10, 0, 10, 10);
            tbSpTtImgRemoveColor.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            tbSpTtImgRemoveColor.SetResourceReference(TextBlock.TextProperty, "LaunchpadNumbersColor");
            spTtImgRemove.Children.Add(tbSpTtImgRemoveColor);
            ttImgRemove.Content = spTtImgRemove;
            imgRemove.ToolTip = ttImgRemove;
            spRemove.Children.Add(imgRemove);
            AddUIElement(spRemove);
            cbCloseColorTo64 = UIViewBusiness.GetLongCheckBox("CloseColorTo64", true);
            AddUIElement(cbCloseColorTo64);
            CreateDialog(300, 350, null);
            tbFileName = Get(1) as TextBox;
            cbWriteToFile = Get(2) as CheckBox;
            cbExportType = Get(4) as ComboBox;
            cbDisassemblyOrSplicingColon = Get(6) as ComboBox;
            //个性化设置
            Window_Loaded();
            SetResourceReference(TitleProperty, "AdvancedExport");
        }

        private void Window_Loaded()
        {
            tbFileName.Text = fileName;
        }


        private void cbExportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbExportType == null)
                return;
            if (cbExportType.SelectedIndex == 0)
            {
                cbWriteToFile.Visibility = Visibility.Visible;
            }
            else
            {
                cbWriteToFile.Visibility = Visibility.Collapsed;
            }
        }
    }
}
