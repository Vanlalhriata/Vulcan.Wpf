using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Vulcan.Wpf.Core
{
    public static class AppNavigator
    {
        private static NavigationService navigationService;

        internal static void SetNavigationService(NavigationService navigationServiceParam)
        {
            navigationService = navigationServiceParam;
        }

        public static void NavigateTo(string viewName)
        {
            if (null == navigationService)
            {
                AppHelper.Logger.Log("navigationService was not assigned. Check if Shell has a Frame", typeof(AppNavigator), LogCategory.Error, LogPriority.Medium);
                return;
            }

            if (!String.IsNullOrEmpty(viewName))
            {
                AppHelper.Logger.Log($"Navigating to {viewName}", typeof(AppNavigator));

                var view = AppHelper.GetView(viewName);

                if (null != view)
                    navigationService.Navigate(view);
            }
            else
                throw new ArgumentNullException(nameof(viewName), "target was null in AppNavigator.NavigateTo(object target)");
        }
    }
}
