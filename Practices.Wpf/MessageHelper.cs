using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Practices.Wpf
{
    public class MessageHelper
    {
        private MessageHelper() { }

        public static void Show(string msg, string title = "提示")
        {
            ShowImplWithUserDefinedWindow(msg, title);
            //ShowImpl(msg, title);
        }  

        private static void ShowImpl(string msg, string title = "提示")
        {
            MessageBox.Show(msg);
        }

        private static void ShowImplWithUserDefinedWindow(string msg, string title = "提示")
        {
            MessageWindow window = new MessageWindow(msg);
            IEnumerator iter = App.Current.Windows.GetEnumerator();
            while (iter.MoveNext())
            {
                if ((iter.Current as Window)?.IsActive == true)
                {
                    window.Owner = iter.Current as Window;
                    break;
                }
            }
            window.ShowInTaskbar = false;
            window.ShowDialog();
        }

    }
}
