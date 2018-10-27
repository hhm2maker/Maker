using Maker.View.Control;
using Maker.View.Style.Child;
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

namespace Maker.View.Style
{
    /// <summary>
    /// StyleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StyleWindow : Window
    {
        public StyleWindow(Window mw)
        {
            InitializeComponent();
            Owner = mw;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public String _Content {
            get;
            set;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0;i<lbCatalog.Items.Count;i++) {
                CheckBox cb = (CheckBox)lbCatalog.Items[i];
                if (cb.IsChecked == true) {
                    BaseChild child = (BaseChild)svMain.Children[i];
                    String result = child.GetString(this, i);
                    if (result == null)
                    {
                        return;
                    }
                    else {
                        builder.Append(result);
                    }
                }
            }
            _Content = builder.ToString();

            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LocationChanged += new EventHandler(RuntimePopup_LocationChanged);
        }
        void RuntimePopup_LocationChanged(object sender, EventArgs e)
        {
            var mi = typeof(System.Windows.Controls.Primitives.Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mi.Invoke(popup, null);//控制popup随window移动而移动
        }

        private int lastSelection = 0;

        private void lbCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCatalog.SelectedIndex == -1)
                return;
            UserControl sp;
            if (lastSelection != -1) {
                sp = (UserControl)svMain.Children[lastSelection];
                sp.Visibility = Visibility.Collapsed;
            }
            lastSelection = lbCatalog.SelectedIndex;
            sp = (UserControl)svMain.Children[lastSelection];
            sp.Visibility = Visibility.Visible;
        }

        public void SetData(string content)
        {
            if (content.Equals(String.Empty))
            {
                AddContent("Color");
                AddContent("Shape");
                AddContent("Time");
                AddContent("ColorOverlay");
                AddContent("SportOverlay");
                AddContent("Other");
            }
            else
            {
                String[] contents = content.Split(';');
                foreach (String str in contents)
                {
                    if (str.Equals(String.Empty))
                        continue;
                    String[] strs = str.Split('=');
                    String type = strs[0];
                    String[] _contents = strs[1].Split(',');

                    AddContent(type);
                    CheckBox box = (CheckBox)lbCatalog.Items[lbCatalog.Items.Count - 1];
                    box.IsChecked = true;

                    BaseChild child = (BaseChild)svMain.Children[svMain.Children.Count - 1];
                    child.SetString(_contents);
                }
            }
            foreach (object o in svMain.Children) {
                UserControl u = (UserControl)o;
                u.Visibility = Visibility.Collapsed;
            }
            lastSelection = 0;
            lbCatalog.SelectedIndex = 0;
            UserControl sp = (UserControl)svMain.Children[0];
            sp.Visibility = Visibility.Visible;
        }

        private void AddContent(string type)
        {
            CheckBox box = new CheckBox();
            box.FontSize = 16;
            box.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            box.SetValue(CheckBox.StyleProperty, Application.Current.Resources["CheckBoxStyle1"]);
            box.Width = 200;
            if (type.Equals("Color")) {
                box.SetResourceReference(CheckBox.ContentProperty, "Color");
                svMain.Children.Add(new ColorChild());
            }
            else if (type.Equals("Shape"))
            {
                box.SetResourceReference(CheckBox.ContentProperty, "Shape");
                svMain.Children.Add(new ShapeChild());
            }
            else if (type.Equals("Time"))
            {
                box.SetResourceReference(CheckBox.ContentProperty, "Time");
                svMain.Children.Add(new TimeChild());
            }
            else if (type.Equals("ColorOverlay"))
            {
                box.SetResourceReference(CheckBox.ContentProperty, "ColorSuperposition");
                svMain.Children.Add(new ColorOverlayChild());
            }
            else if (type.Equals("SportOverlay"))
            {
                box.SetResourceReference(CheckBox.ContentProperty, "AccelerationOrDeceleration");
                svMain.Children.Add(new SportOverlayChild());
            }
            else if (type.Equals("Other"))
            {
                box.SetResourceReference(CheckBox.ContentProperty, "Other");
                svMain.Children.Add(new OtherChild());
            }
            box.Click += Box_Click;
            lbCatalog.Items.Add(box);
        }

        private void Box_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            lbCatalog.SelectedItem = sender;
        }

        private void btnNewFx_Click(object sender, RoutedEventArgs e)
        {
            if (popup.IsOpen == true) {
                popup.IsOpen = false;
            }
            else{
                //刷新数据
                //颜色、时间、其他不可以重复
                //形状、颜色叠加、运动叠加可以重复
                bool bColor = true;
                bool bTime = true;
                bool bOther = true;
                foreach (Object obj in lbCatalog.Items) {
                    CheckBox box = (CheckBox)obj;
                    if (box.Content.Equals("颜色")|| box.Content.Equals("Color")) {
                        bColor = false;
                    }
                    if (box.Content.Equals("时间") || box.Content.Equals("Time"))
                    {
                        bTime = false;
                    }
                    if (box.Content.Equals("其他") || box.Content.Equals("Other"))
                    {
                        bOther = false;
                    }
                }
                lbFx.Items.Clear();
                if (bColor == true) {
                    ListBoxItem item = new ListBoxItem();
                    item.SetResourceReference(ListBoxItem.ContentProperty, "Color");
                    lbFx.Items.Add(item);
                }
                ListBoxItem item2 = new ListBoxItem();
                item2.SetResourceReference(ListBoxItem.ContentProperty, "Shape");
                lbFx.Items.Add(item2);
                if (bTime == true) {
                    ListBoxItem item3 = new ListBoxItem();
                    item3.SetResourceReference(ListBoxItem.ContentProperty, "Time");
                    lbFx.Items.Add(item3);
                }
                ListBoxItem item4 = new ListBoxItem();
                item4.SetResourceReference(ListBoxItem.ContentProperty, "ColorSuperposition");
                lbFx.Items.Add(item4);
                ListBoxItem item5 = new ListBoxItem();
                item5.SetResourceReference(ListBoxItem.ContentProperty, "AccelerationOrDeceleration");
                lbFx.Items.Add(item5);
                if (bOther == true) {
                    ListBoxItem item6 = new ListBoxItem();
                    item6.SetResourceReference(ListBoxItem.ContentProperty, "Other");
                    lbFx.Items.Add(item6);
                }
                popup.IsOpen = true;
            }
         
        }

        private void lbFx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbFx.SelectedIndex == -1) {
                return;
            }
            ListBoxItem item = (ListBoxItem)lbFx.SelectedItem;
            if (item.Content.Equals("颜色")|| item.Content.Equals("Color")) {
                AddContent("Color");
            }
            if (item.Content.Equals("形状") || item.Content.Equals("Shape"))
            {
                AddContent("Shape");
            }
            if (item.Content.Equals("时间") || item.Content.Equals("Time"))
            {
                AddContent("Time");
            }
            if (item.Content.Equals("颜色叠加") || item.Content.Equals("Color Overlap"))
            {
                AddContent("ColorOverlay");
            }
            if (item.Content.Equals("运动叠加") || item.Content.Equals("Accelerate/Decelerate"))
            {
                AddContent("SportOverlay");
            }
            if (item.Content.Equals("其他") || item.Content.Equals("Other"))
            {
                AddContent("Other");
            }
            lbCatalog.SelectedIndex = lbCatalog.Items.Count - 1;
            popup.IsOpen = false;
        }

        private void BtnRemoveFx_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lbCatalog.SelectedIndex == -1)
                return;
            int position = lbCatalog.SelectedIndex;
            lbCatalog.Items.RemoveAt(position);
            svMain.Children.RemoveAt(position);
            lastSelection = -1;
        }

        private void ImgUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lbCatalog.SelectedIndex <= 0) {
                return;
            }
            else
            {
                int i = lbCatalog.SelectedIndex;

                List<CheckBox> checkboxs = new List<CheckBox>();
                foreach (Object o in lbCatalog.Items) {
                    checkboxs.Add((CheckBox)o);
                }

                lbCatalog.Items.Clear();

                CheckBox box = checkboxs[i-1];
                checkboxs[i-1]  = checkboxs[i];
                checkboxs[i] = box;

                foreach (CheckBox c in checkboxs) {
                    lbCatalog.Items.Add(c);
                }

                List<BaseChild> basechilds = new List<BaseChild>();
                foreach (Object o in svMain.Children)
                {
                    basechilds.Add((BaseChild)o);
                }

                svMain.Children.Clear();

                BaseChild child = basechilds[i - 1];
                basechilds[i - 1] = basechilds[i];
                basechilds[i] = child;

                foreach (BaseChild b in basechilds)
                {
                    svMain.Children.Add(b);
                }
                lbCatalog.SelectedIndex = i - 1;
            }
        }
        private void ImgDown_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lbCatalog.SelectedIndex == -1 || lbCatalog.SelectedIndex == lbCatalog.Items.Count-1)
            {
                return;
            }
            else
            {
                int i = lbCatalog.SelectedIndex;

                List<CheckBox> checkboxs = new List<CheckBox>();
                foreach (Object o in lbCatalog.Items)
                {
                    checkboxs.Add((CheckBox)o);
                }

                lbCatalog.Items.Clear();

                CheckBox box = checkboxs[i + 1];
                checkboxs[i + 1] = checkboxs[i];
                checkboxs[i] = box;

                foreach (CheckBox c in checkboxs)
                {
                    lbCatalog.Items.Add(c);
                }

                List<BaseChild> basechilds = new List<BaseChild>();
                foreach (Object o in svMain.Children)
                {
                    basechilds.Add((BaseChild)o);
                }

                svMain.Children.Clear();

                BaseChild child = basechilds[i + 1];
                basechilds[i + 1] = basechilds[i];
                basechilds[i] = child;

                foreach (BaseChild b in basechilds)
                {
                    svMain.Children.Add(b);
                }
                lbCatalog.SelectedIndex = i + 1;
            }
        }
    }
}
