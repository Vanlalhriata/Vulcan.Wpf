using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Vulcan.Wpf.Utils
{
    public class MenuItemExtensions : DependencyObject
    {
        #region Menu items as radio buttons

        private static Dictionary<MenuItem, string> menuItemToGroupMap = new Dictionary<MenuItem, string>();

        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached(
            "GroupName", typeof(string), typeof(MenuItemExtensions), new PropertyMetadata(String.Empty, onGroupNameChanged));

        public static string GetGroupName(MenuItem element)
        {
            return (string)element.GetValue(GroupNameProperty);
        }

        public static void SetGroupName(MenuItem element, string value)
        {
            element.SetValue(GroupNameProperty, value);
        }

        private static void onGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menuItem = d as MenuItem;
            if (null == menuItem)
                return;

            var oldGroupName = e.OldValue.ToString();
            var newGroupName = e.NewValue.ToString();

            if (String.IsNullOrEmpty(newGroupName))
            {
                // Remove the item from grouping
                removeItemFromGrouping(menuItem);
            }
            else
            {
                menuItem.IsCheckable = true;

                // Switch to a new group
                if (newGroupName != oldGroupName)
                {
                    // remove from old group
                    if (!String.IsNullOrEmpty(oldGroupName))
                        removeItemFromGrouping(menuItem);

                    menuItemToGroupMap.Add(menuItem, newGroupName);
                    menuItem.Click += onMenuItemClicked;
                }
            }
        }

        private static void removeItemFromGrouping(MenuItem menuItem)
        {
            if (menuItemToGroupMap.ContainsKey(menuItem))
                menuItemToGroupMap.Remove(menuItem);

            menuItem.Click -= onMenuItemClicked;
        }

        private static void onMenuItemClicked(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)e.OriginalSource;

            if (menuItem.IsChecked)
            {
                var groupName = GetGroupName(menuItem);
                menuItemToGroupMap.Where(p => p.Key != menuItem && p.Value == groupName)
                    .ToList().ForEach(p => p.Key.IsChecked = false);
            }
            else
                menuItem.IsChecked = true;
            
        }

        #endregion Menu items as radio buttons
    }
}
