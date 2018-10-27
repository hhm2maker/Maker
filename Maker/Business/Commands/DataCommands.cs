using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maker.Commands
{
    public class DataCommands
    {
        private static RoutedUICommand editcommand;

        static DataCommands() {
            InputGestureCollection inputs = new InputGestureCollection();
            //注册鼠标左键点击触发命令
            inputs.Add(new MouseGesture(MouseAction.LeftClick));
            //inputs.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            editcommand = new RoutedUICommand("Edit","Edit",typeof(DataCommands),inputs);
        }

        public static RoutedUICommand Editcommand {
            get { return editcommand; }
        }
    }
}
