using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;
using System.Xml.Linq;
using Operation;

namespace Maker.View
{
    public class BaseMakerLightUserControl : BaseUserControl, ISimpleMakerLight
    {
        public BaseMakerLightUserControl()
        {
            bMakerLightUserControl = true;
        }

        public virtual void SetData(List<Light> lightList) { }

        public virtual List<Light> GetData() { return null; }

    }
}
