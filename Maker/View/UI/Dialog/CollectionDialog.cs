using Maker.View.Control;
using Maker.View.LightScriptUserControl;
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
using static Maker.Model.EnumCollection;

namespace Maker.View.Dialog
{
    /// <summary>
    /// CollectionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionDialog : BaseDialog
    {
        private ScriptUserControl iuc;
        public ComboBox cbLightName, cbType;
        public CollectionDialog(ScriptUserControl iuc)
        {
            Owner = iuc.mw;
            this.iuc = iuc;
            ToCreateDialog();
        }

        private void ToCreateDialog()
        {
            //构建对话框
            AddTopHintTextBlock("LightNameColon");
            AddComboBox(new List<string>() { }, null);
            AddTopHintTextBlock("TypeColon");
            AddComboBox(new List<string>() { "None", "ShadeIntersection", "ShadeComplement" }, null);
            CreateDialog(200, 180, null);
            cbLightName = Get(1) as ComboBox;
            cbType = Get(3) as ComboBox;
            //个性化设置
            Window_Loaded();
            SetResourceReference(TitleProperty, "Collection");
        }

        private String stepName = String.Empty;
        private CollectionType type = CollectionType.Intersection;
        public CollectionDialog(UserControl iuc, String stepName, CollectionType type)
        {
            //Owner = iuc.mw_;
            //this.iuc = iuc;
            this.stepName = stepName;
            this.type = type;
            ToCreateDialog();
        }
        private void Window_Loaded()
        {
            List<String> ls = iuc.GetStepNameCollection();
            ls.Remove(iuc.GetStepName());
            int position = -1;
            if (!stepName.Equals(String.Empty))
            {
                position = ls.IndexOf(stepName);
            }
            for (int i = 0; i < ls.Count; i++)
            {
                cbLightName.Items.Add(ls[i]);
            }
            if (ls.Count > 0)
            {
                if (position == -1)
                {
                    cbLightName.SelectedIndex = 0;
                }
                else
                {
                    cbLightName.SelectedIndex = position;
                    if (type == CollectionType.Intersection)
                    {
                        cbType.SelectedIndex = 1;
                    }
                    else if (type == CollectionType.Complement)
                    {
                        cbType.SelectedIndex = 2;
                    }
                }
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
