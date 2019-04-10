using Maker.Business.Model.OperationModel;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Maker.View.UI.Style.Child
{
    public partial class ThirdPartyOperationChild : OperationStyle
    {
        private ThirdPartyOperationModel thirdPartyOperationModel;
        public ThirdPartyOperationChild(ThirdPartyOperationModel thirdPartyOperationModel)
        {
            this.thirdPartyOperationModel = thirdPartyOperationModel;
            //构建对话框
            //AddTopHintTextBlock("StartColon");
            //AddTextBox();
            //AddTopHintTextBlock("EndColon");
            //AddTextBox();
            //CreateDialog(200, 200);
            //tbStart = Get(1) as TextBox;
            //tbEnd = Get(3) as TextBox;

            ThirdPartyModel thirdPartyModel = new ThirdPartyModel();
            for (int i = 0; i < StaticConstant.mw.suc.thirdPartys.Count; i++) {
                if (StaticConstant.mw.suc.thirdPartys[i].name.Equals(thirdPartyOperationModel.ThirdPartyName)) {
                    thirdPartyModel = StaticConstant.mw.suc.thirdPartys[i];
                }
            }
            
            String _viewFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Operation\View\" + thirdPartyModel.view + ".xml";
            XDocument doc = XDocument.Load(_viewFilePath);
            foreach (XElement element in doc.Element("Views").Elements())
            {
                if (element.Attribute("type").Value.Equals("textblock"))
                {
                    if (StaticConstant.mw.strMyLanguage.Equals("en-US"))
                    {
                        AddTopHintTextBlockForThirdPartyModel(element.Attribute("entext").Value);
                    }
                    else if (StaticConstant.mw.strMyLanguage.Equals("zh-CN"))
                    {
                        AddTopHintTextBlockForThirdPartyModel(element.Attribute("zhtext").Value);
                    }
                }
                if (element.Attribute("type").Value.Equals("textbox"))
                {
                    AddTextBox();
                }
            }
            CreateDialog(200, 50 * UICount);

            //String viewString = window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].view;
            //if (viewString.Equals(String.Empty))
            //{
            //    //不需要View
            //    for (int k = 0; k < window.iuc.lbStep.SelectedItems.Count; k++)
            //    {
            //        StackPanel sp = (StackPanel)window.iuc.lbStep.SelectedItems[k];
            //        //没有可操作的灯光组
            //        if (!window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)].Contains(window.iuc.GetStepName(sp) + "LightGroup"))
            //        {
            //            continue;
            //        }
            //        String command = String.Empty;
            //        command = Environment.NewLine + "\t" + window.iuc.GetStepName(sp) + "LightGroup = Edit." + window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].name + "(" + window.iuc.GetStepName(sp) + "LightGroup);";
            //        window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)] += command;
            //    }
            //    window.iuc.RefreshData();
            //}
            //else
            //{
            //    ThirdPartyDialog dialog = new ThirdPartyDialog(window, viewString);
            //    if (window.strMyLanguage.Equals("en-US"))
            //    {
            //        dialog.Title = window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].entext;
            //    }
            //    else if (window.strMyLanguage.Equals("zh-CN"))
            //    {
            //        dialog.Title = window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].zhtext;
            //    }
            //    if (dialog.ShowDialog() == true)
            //    {
            //        for (int k = 0; k < window.iuc.lbStep.SelectedItems.Count; k++)
            //        {
            //            StackPanel sp = (StackPanel)window.iuc.lbStep.SelectedItems[k];
            //            //没有可操作的灯光组
            //            if (!window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)].Contains(window.iuc.GetStepName(sp) + "LightGroup"))
            //            {
            //                continue;
            //            }
            //            String command = Environment.NewLine + "\t" + window.iuc.GetStepName(sp) + "LightGroup = Edit." + window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].name + "(" + window.iuc.GetStepName(sp) + "LightGroup" + dialog.result + ");";
            //            window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)] += command;
            //        }
            //        window.iuc.RefreshData();
            //    }
            //}

            //tbStart.Text = interceptTimeOperationModel.Start.ToString();
            //tbEnd.Text = interceptTimeOperationModel.End.ToString();
        }


        public override bool ToSave() {
            List<String> parameters = new List<string>();
            for (int i = 0; i < _UI.Count; i++) {
                if (_UI[i] is TextBox) {
                    if ((_UI[i] as TextBox).Text.Equals(String.Empty)) {
                        (_UI[i] as TextBox).Focus();
                        return false;
                    }
                    parameters.Add((_UI[i] as TextBox).Text);
                }
            }
            thirdPartyOperationModel.Parameters = parameters;
            return true;
        }

       
    }
}
