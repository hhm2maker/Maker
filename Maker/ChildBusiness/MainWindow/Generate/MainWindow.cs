using Maker.Business;
using Maker.Model;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static Maker.Model.EnumCollection;

namespace Maker.View.Control
{
    public partial class MainWindow
    {
        private void GenerateExe(object sender, RoutedEventArgs e)
        {
            if (projectType == ProjectType.launchpadlightproject)
            {
                GenerateLaunchpadLightProject();
            }
            else if (projectType == ProjectType.launchpadlightlibrary) {
                GenerateLaunchpadLightLibrary();
            }
        }
        private void GenerateLaunchpadLightLibrary()
        {
            List<String> fileNames = new List<string>();
            FileBusiness business = new FileBusiness();
            fileNames.AddRange(business.GetFilesName(lastProjectPath + @"\Light", new List<string>() { ".light" }));
            fileNames.AddRange(business.GetFilesName(lastProjectPath + @"\LightScript", new List<string>() { ".lightScript" }));
            fileNames.AddRange(business.GetFilesName(lastProjectPath + @"\Midi", new List<string>() { ".mid" }));
            ShowLightListDialog dialog = new ShowLightListDialog(this, "", fileNames);
            if (dialog.ShowDialog() == true)
            {
                //tbLightName.Text = dialog.selectItem;
                //int position = int.Parse(tbPosition.Text) - 28;
                //if (nowSelectType == PageUCSelectType.Down)
                //{
                //    _pageModes[position][int.Parse(tbCount.Text) - 1]._down._lightName = dialog.selectItem;
                //}
                //else if (nowSelectType == PageUCSelectType.Loop)
                //{
                //    _pageModes[position][int.Parse(tbCount.Text) - 1]._loop._lightName = dialog.selectItem;
                //}
                //else if (nowSelectType == PageUCSelectType.Up)
                //{
                //    _pageModes[position][int.Parse(tbCount.Text) - 1]._up._lightName = dialog.selectItem;
                //}
             
            }
        }
        private void GenerateLaunchpadLightProject()
        {
            TreeViewItem _item = null;
            StackPanel _panel = null;
            TextBlock _text = null;
            for (int i = 0; i < tvProject.Items.Count; i++)
            {
                _item = (TreeViewItem)tvProject.Items[i];
                _panel = (StackPanel)_item.Header;
                _text = (TextBlock)_panel.Children[1];
                if (_text.Text.Equals("Page"))
                    break;
                if (i == tvProject.Items.Count - 1)
                    return;
            }
            Dictionary<String, String> mDictionary = new Dictionary<string, string>();
            Dictionary<String, List<Light>> mLightDictionary = new Dictionary<string, List<Light>>();
            //获取对象
            XDocument xDoc = new XDocument();
            XElement xRoot = new XElement("Root");
            xDoc.Add(xRoot);
          
            //Tutorial
            XElement xTutorial = new XElement("Tutorial");
            XAttribute xContent;
            if (tutorial.Equals(String.Empty)) {
                xContent = new XAttribute("content", "");
            }
            else
            {
                xContent = new XAttribute("content", fileBusiness.String2Base(fileBusiness.WriteMidiContent(AllFileToLightList(tutorial))));
            }
            xTutorial.Add(xContent);
            xRoot.Add(xTutorial);

            //Pages
            XElement xPages = new XElement("Pages");
            XAttribute xFirst = new XAttribute("first", firstPageName);
            xPages.Add(xFirst);
            xRoot.Add(xPages);

            TreeViewItem item = null;

            int nowPosition = 0;
            for (int i = 0; i < _item.Items.Count; i++)
            {
                item = (TreeViewItem)_item.Items[i];
                _panel = (StackPanel)item.Header;
                _text = (TextBlock)_panel.Children[1];

                XElement xPage = new XElement("Page");
                XAttribute xPageName = new XAttribute("name", _text.Text);
                xPage.Add(xPageName);

                puc.ReadPageFile(lastProjectPath + @"\Page\" + _text.Text, out List<List<PageButtonModel>> pageModes);
                for (int x = 0; x < pageModes.Count; x++)
                {
                    if (pageModes[x].Count == 0)
                        continue;

                    XElement xButtons = new XElement("Buttons");
                    XAttribute xPosition = new XAttribute("position", x + 28);
                    xButtons.Add(xPosition);
                    for (int y = 0; y < pageModes[x].Count; y++)
                    {
                        XElement xButton = new XElement("Button");
                        //Down
                        XElement xDown = new XElement("Down");
                        XAttribute xDownLightName = new XAttribute("lightname", "");
                        if (!pageModes[x][y]._down._lightName.Equals(String.Empty))
                        {
                            if (mDictionary.ContainsKey(pageModes[x][y]._down._lightName))
                            {
                                xDownLightName = new XAttribute("lightname", mDictionary[pageModes[x][y]._down._lightName]);
                            }
                            else
                            {
                                mDictionary.Add(pageModes[x][y]._down._lightName, nowPosition.ToString());
                                mLightDictionary.Add(nowPosition.ToString(), AllFileToLightList(pageModes[x][y]._down._lightName));
                                xDownLightName = new XAttribute("lightname", nowPosition.ToString());
                                nowPosition++;
                            }
                        }
                        XAttribute xDownGoto = new XAttribute("goto", pageModes[x][y]._down._goto);
                        XAttribute xDownBpm = new XAttribute("bpm", pageModes[x][y]._down._bpm);
                        xDown.Add(xDownLightName);
                        xDown.Add(xDownGoto);
                        xDown.Add(xDownBpm);
                        xButton.Add(xDown);
                        //Loop
                        XElement xLoop = new XElement("Loop");
                        XAttribute xLoopLightName = new XAttribute("lightname", "");
                        if (!pageModes[x][y]._loop._lightName.Equals(String.Empty))
                        {
                            if (mDictionary.ContainsKey(pageModes[x][y]._loop._lightName))
                            {
                                xLoopLightName = new XAttribute("lightname", mDictionary[pageModes[x][y]._loop._lightName]);
                            }
                            else
                            {
                                mDictionary.Add(pageModes[x][y]._loop._lightName, nowPosition.ToString());
                                mLightDictionary.Add(nowPosition.ToString(), AllFileToLightList(pageModes[x][y]._loop._lightName));
                                xLoopLightName = new XAttribute("lightname", nowPosition.ToString());
                                nowPosition++;
                            }
                        }
                        XAttribute xLoopGoto = new XAttribute("goto", pageModes[x][y]._loop._goto);
                        XAttribute xLoopBpm = new XAttribute("bpm", pageModes[x][y]._loop._bpm);
                        xLoop.Add(xLoopLightName);
                        xLoop.Add(xLoopGoto);
                        xLoop.Add(xLoopBpm);
                        xButton.Add(xLoop);
                        //Up
                        XElement xUp = new XElement("Up");
                        XAttribute xUpLightName = new XAttribute("lightname", "");
                        if (!pageModes[x][y]._up._lightName.Equals(String.Empty))
                        {
                            if (mDictionary.ContainsKey(pageModes[x][y]._up._lightName))
                            {
                                xUpLightName = new XAttribute("lightname", mDictionary[pageModes[x][y]._up._lightName]);
                            }
                            else
                            {
                                mDictionary.Add(pageModes[x][y]._up._lightName, nowPosition.ToString());
                                mLightDictionary.Add(nowPosition.ToString(), AllFileToLightList(pageModes[x][y]._up._lightName));
                                xUpLightName = new XAttribute("lightname", nowPosition.ToString());
                                nowPosition++;
                            }
                        }
                        XAttribute xUpGoto = new XAttribute("goto", pageModes[x][y]._up._goto);
                        XAttribute xUpBpm = new XAttribute("bpm", pageModes[x][y]._up._bpm);
                        xUp.Add(xUpLightName);
                        xUp.Add(xUpGoto);
                        xUp.Add(xUpBpm);
                        xButton.Add(xUp);

                        xButtons.Add(xButton);
                    }
                    xPage.Add(xButtons);
                }
                xPages.Add(xPage);
            }
            //Lights
            XElement xLights = new XElement("Lights");
            xRoot.Add(xLights);
            foreach (var mItem in mLightDictionary)
            {
                XElement xLight = new XElement("Light");
                XAttribute xLightName = new XAttribute("name", mItem.Key);
                xLight.Add(xLightName);
                //StringBuilder builder = new StringBuilder();
                //for (int y = 0; y < mItem.Value.Count; y++)
                //{
                //    Light l = mItem.Value[y];
                //    builder.Append(l.Time + "," + l.Action + "," + l.Position + "," + l.Color + ";");
                //}
                //XAttribute xValue = new XAttribute("value", builder.ToString());
                //String str = fileBusiness.String2Base(fileBusiness.WriteMidiContent(mItem.Value));
                //Console.WriteLine(str);
                //Console.WriteLine("-----------");

                //String str = fileBusiness.Base2String(fileBusiness.String2Base(fileBusiness.WriteMidiContent(mItem.Value)));
                //List<int> mList = new List<int>();
                //for (int i = 0; i < str.Length; i++)
                //{
                //    mList.Add(str[i]);
                //}
                //LightBusiness.Print(fileBusiness.ReadMidiContent(mList));

                XAttribute xValue = new XAttribute("value", fileBusiness.String2Base(fileBusiness.WriteMidiContent(mItem.Value)));
                xLight.Add(xValue);
                xLights.Add(xLight);
            }
            DirectoryInfo d = new DirectoryInfo(lastProjectPath);
            xDoc.Save(lastProjectPath + @"\" + d.Name + ".makerpl");
        }
    }
}
