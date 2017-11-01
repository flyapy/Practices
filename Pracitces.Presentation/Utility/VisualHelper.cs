using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Practices.Presentation.Utility
{
    class VisualHelper
    {
        public static void ActionOnAllElement(Visual visual, Action<Visual> action)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (childVisual != null)
                {
                    action(childVisual);
                    ActionOnAllElement(childVisual, action);
                }
            }
        }
    }
}
