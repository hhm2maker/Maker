using System;
using System.Windows.Controls;

namespace Maker.View.Style.Child
{
    public  class BaseChild:UserControl
    {
        public virtual string GetString(StyleWindow window,int position) {
            return "";
        }
        public virtual void SetString(String[] _content)
        {
           
        }
    }
}
