using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Vulcan.Wpf.Core
{
    public static class VisualHelper
    {
        public static T FindVisualChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            if (null == parent)
                return null;

            int numChildren = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numChildren; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var result = (child as T) ?? FindVisualChild<T>(child);

                if (null != result)
                    return result;
            }

            return null;
        }
    }
}
