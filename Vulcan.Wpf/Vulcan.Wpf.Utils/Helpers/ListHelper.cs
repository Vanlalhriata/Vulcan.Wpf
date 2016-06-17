using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vulcan.Wpf.Core;

namespace Vulcan.Wpf.Utils
{
    public static class ListHelper
    {
        private static RelayCommand newClickedCommand;

        /// <summary>
        /// Add a new item to the IList parameter.
        /// </summary>
        public static ICommand NewClickedCommand
        {
            get
            {
                if (null == newClickedCommand)
                    newClickedCommand = new RelayCommand(onNewClicked);

                return newClickedCommand;
            }
        }

        
        private static void onNewClicked(object parameter)
        {
            if (null == parameter)
                throw new ArgumentNullException(nameof(parameter));

            var list = (System.Collections.IList)parameter;
            var type = list.GetType().GetGenericArguments().First();
            var newItem = Activator.CreateInstance(type);
            list.Add(newItem);
        }

        private static RelayCommand deleteClickedCommand;

        /// <summary>
        /// Delete the Framework element parameter's DataContext as an item from its parent ItemControl's ItemsSource.
        /// </summary>
        public static ICommand DeleteClickedCommand
        {
            get
            {
                if (null == deleteClickedCommand)
                    deleteClickedCommand = new RelayCommand(onDeleteClicked);

                return deleteClickedCommand;
            }
        }
        
        private static void onDeleteClicked(object parameter)
        {
            if (null == parameter)
                throw new ArgumentNullException(nameof(parameter));

            var dataContext = ((FrameworkElement)parameter).DataContext;
            if (null == dataContext)
                throw new ArgumentException("The parameter does not have a DataContext", nameof(parameter));

            var itemsControl = VisualHelper.FindVisualParent<ItemsControl>((DependencyObject)parameter);
            if (null == itemsControl)
                throw new ArgumentException("The parameter is not in an ItemsControl", nameof(parameter));

            var list = (System.Collections.IList)itemsControl.ItemsSource;

            if (list.Contains(dataContext))
                list.Remove(dataContext);
        }

    }
}
