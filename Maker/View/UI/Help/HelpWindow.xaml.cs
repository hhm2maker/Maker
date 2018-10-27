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

namespace Maker.View.Help
{
    /// <summary>
    /// HelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvMain.SelectedItem == tviConstitute)
            {
                SetContext("0");
            }
            if (tvMain.SelectedItem == tviFile)
            {
                SetContext("1");
            }
            if (tvMain.SelectedItem == tviOperation)
            {
                SetContext("2");
            }
            if (tvMain.SelectedItem == tviOperationMain)
            {
                SetContext("2-0");
            }
            if (tvMain.SelectedItem == tviOperationInput)
            {
                SetContext("2-1");
            }
            if (tvMain.SelectedItem == tviOperationFrame)
            {
                SetContext("2-2");
            }
            if (tvMain.SelectedItem == tviTool)
            {
                SetContext("3");
            }
            if (tvMain.SelectedItem == tviSettingAndHelp)
            {
                SetContext("4");
            }
        }

        public void SetContext(String position)
        {
            if (position.Equals("0"))
            {
                string myText = "本软件由四个区块构成，分别是：文件区、操作区、工具区、帮助和设置区。" + Environment.NewLine
                    + "文件区：对.midi文件和.light文件进行管理。" + Environment.NewLine
                     + "操作区：对文件进行以动作为单位的编辑。" + Environment.NewLine
                        + "工具区：包含常用工具，如：播放器、编辑器等。" + Environment.NewLine
                         + "帮助和设置区：对软件的使用提供帮助和设置。" + Environment.NewLine;
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline
                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
            else if (position.Equals("1"))
            {
                string myText = "文件区分为两块，一块为项目文件(.light文件)，另一块为工程文件(.midi文件)。" + Environment.NewLine
                    + "项目文件为灯光文件，由数个动作构成，能通过【操作区】或【工具区-编辑】对其进行改动。可通过【文件】-【项目文件】-【导出】转变为.midi文件。" + Environment.NewLine
                     + "工程文件为二进制文件,可被live和max直接读取，也可被本软件解析为灯光文件";
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "(本软件把midi文件作为灯光读取，如读取音乐midi可能会发生不可预知的错误)。";
                r = new Run(myText);
                r.Foreground = new SolidColorBrush(Color.FromRgb(255, 128, 114));
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "可通过双击【文件】-【工程文件】内的.midi文件转变为.light文件。" + Environment.NewLine;
                r = new Run(myText);
                //r.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "本软件非自动更新，推荐将项目文件放置在其他文件内，以免删除软件文件夹时将项目文件也同时删除。(你可以在【设置】-【文件】内修改文件的存放路径)";
                r = new Run(myText);
                r.Foreground = new SolidColorBrush(Color.FromRgb(255, 128, 114));
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
            else if (position.Equals("2"))
            {
                string myText = "操作区由三部分组成，分别是：主页、输入和逐帧。" + Environment.NewLine
                    + "主页：对动作进行标准清晰的查看。" + Environment.NewLine
                     + "输入：对动作转化为数字(可编辑状态)。" + Environment.NewLine
                        + "逐帧：对动作转化为图像(可视化状态)。" + Environment.NewLine + Environment.NewLine
                           + "动作是由四个数字构成的：" + Environment.NewLine
                + "时间：表示该动作从第几格开始。你可以以BPM作为分格依据。例如：歌曲的BPM为96，你就可以以每秒有96格,那么如果你需要在一秒钟内做两个动作那么输入区就会是这样的:0,x,x,x; 45,x,x,x; (该例只涉及到时间，并没有关闭动作)。" + Environment.NewLine
                  + "行为：选填以下两种： o / O：打开(open)或c / C:关闭(close)" + Environment.NewLine
                   + "位置：Launchpad格子所对应的位置。如：36是左下角的方块按钮。具体数值请参照【工具】-【附件】里的位置表。" + Environment.NewLine
                    + "颜色：Launchpad所对应的颜色。如：5是红色。具体数值请参照【工具】-【附件】里的颜色表。" + Environment.NewLine;
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline
                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
            else if (position.Equals("2-0"))
            {
                string myText = "主页：对动作进行标准清晰的查看。" + Environment.NewLine;
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "主页仅有查看功能，任何操作不会影响动作以及动作的排列方式。" + Environment.NewLine;
                r = new Run(myText);
                r.Foreground = new SolidColorBrush(Color.FromRgb(255, 128, 114));
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "能够获取灯光文件的基础属性：包含多少种颜色、动作，是否包含顶部圆钮灯光，是否包含其他圆钮灯光。" + Environment.NewLine
                     + "能够对动作进行排序。" + Environment.NewLine;
                r = new Run(myText);
                //r.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
            else if (position.Equals("2-1"))
            {
                string myText = "输入：对动作转化为数字(可编辑状态)。" + Environment.NewLine
                    + "选中编辑：选中编辑区可更改输入区选中的动作。框内可填写三种形式的文字: +数字，-数字，数字。+数字，代表选中动作的对应属性加上该数字。如：时间 + 10 代表选中动作推迟10格时间。-数字，代表选中动作的对应属性减去该数字。如：时间 - 10 代表选中动作提前10格时间。数字，代表选中动作的对应属性改变为该数字。如：时间10 代表选中动作的时间变为10。替换和添加：替换为在当前选中的动作中更改。添加为在当前选中的动作不变，添加为新的动作并将新的动作选中。" + Environment.NewLine
                     + "快速生成区由五个内容组成：时间：从第几格开始。范围：选取某一段的范围，中间以逗号或者空格分隔(可在【设置】-【输入】-【分隔符】处修改)。如：36,37,38。如连续段可用数字1 - 数字2表示。如：36 - 38。以上两种符号可同时起作用。如要表示36,37,38,40,41，42。可缩写成：36 - 38,40 - 42。间隔：范围选中范围内，前一个位置和后一个位置相差时间。可填0，代表同时亮。如想要36 - 40同时亮,范围填：36 - 40、间隔填0。持续：每个位置亮的时间，单位：格时间。颜色：位置亮的时间，同样可以用分隔符和范围符。" + Environment.NewLine
                      + "条件判断区：当满足如果属性的情况下，替换成那么里的属性。" + Environment.NewLine;
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline
                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
            else if (position.Equals("2-2"))
            {
                string myText = "逐帧：对动作转化为图像(可视化状态)。" + Environment.NewLine;
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "进入逐帧为了正常显示会进行强制【拆分】(你可以通过【帮助】-【工具】-【拆分与拼合】了解更多)" + Environment.NewLine
                    + "你需要创建节点才能开始绘制" + Environment.NewLine
                     + "你需要在结束绘制后在末尾创建一个不含任何颜色的时间节点作为结束节点，该行为作用：告诉软件最后一个时间点的灯光何时结束。如无此操作将会导致导出的midi文件缺失关闭行为，Live(max)解析播放该灯光则会让灯光在+1格时间消失，本软件解析播放灯光则会停留在最后一格无消失。" + Environment.NewLine;
                r = new Run(myText);
                r.Foreground = new SolidColorBrush(Color.FromRgb(255, 128, 114));
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "上一节点：跳转到上一个节点" + Environment.NewLine
                    + "下一节点：跳转到下一个节点" + Environment.NewLine
                      + "添加节点：弹出对话框引导添加节点(重量级添加)" + Environment.NewLine
                      + "删除节点：删除该节点" + Environment.NewLine
                        + "开始节点：添加一个第0帧的节点" + Environment.NewLine
                         + "添加节点：根据左侧输入框内容添加节点(轻量级添加),";
                r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "有可能会引起【该帧已有时间节点】和【输入格式不正确】异常。";
                r = new Run(myText);
                r.Foreground = new SolidColorBrush(Color.FromRgb(255, 128, 114));
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                myText = "输入框内可填写+数字：表示新节点所在帧为当前时间节点所在帧+数字;-数字：表示新节点所在帧为当前时间节点所在帧-数字;数字：表示新节点所在帧为该数字。" + Environment.NewLine
                    + "位置（四区）内的操作是针对当前页单个区块共16个Launchpad格子进行的操作，不包含四外圈圆钮" + Environment.NewLine
                        + "位置（整页）内的操作是针对当前页整个Launchpad进行的操作，含包含四外圈圆钮" + Environment.NewLine;

                r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline

                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
            else if (position.Equals("3"))
            {
                string myText = "工具区：包含常用工具，如：播放器、编辑器等。" + Environment.NewLine
                    + "编辑器：编辑器所做的操作是针对所有的动作的。颜色编辑分为两种，单独修改某个颜色可选用【更改】,修改多个颜色可选用【格式化颜色】-【自定义】。" + Environment.NewLine
                + "播放器：模拟Launchpad播放灯光(可设置BPM)。" + Environment.NewLine
                + "附件：查看颜色表和位置表，你可以通过【设置】-【工具】-【其他画图软件路径】里设置用其他软件打开位置表。" + Environment.NewLine
                + "附件：查看颜色表和位置表，你可以通过【设置】-【工具】-【其他画图软件路径】里设置用其他软件打开位置表。" + Environment.NewLine
                 + "系统工具：打开Windows系统工具，如：计算器。" + Environment.NewLine
                   + "拆分与拼合：对动作根据时间节点进行拆分和拼合。如：有三个节点0、8、16，有一个动作为0开16关，则会被拆分成0开8关8开16关。你也可以手动将其拼合。注意：如果在逐帧的状态下选择【拼合】则会在刷新数据时被重新拆分。" + Environment.NewLine
                ;

                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline
                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
            else if (position.Equals("4"))
            {
                string myText = "设置和帮助因其较为明了故不再赘述。" + Environment.NewLine;
                FlowDocument doc = new FlowDocument();
                Paragraph p = new Paragraph();
                Run r = new Run(myText);
                r.FontSize = 22;
                p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline
                doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
                rtbMain.Document = doc;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string myText = "欢迎来到帮助文档，你可以从左边选择你想要了解的内容!";
            FlowDocument doc = new FlowDocument();
            Paragraph p = new Paragraph();
            Run r = new Run(myText);
            r.FontSize = 22;
            p.Inlines.Add(r);//Run级元素添加到Paragraph元素的Inline
            doc.Blocks.Add(p);//Paragraph级元素添加到流文档的块级元素
            rtbMain.Document = doc;
        }
    }
}
