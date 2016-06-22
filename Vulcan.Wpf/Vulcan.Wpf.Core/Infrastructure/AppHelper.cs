using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Vulcan.Wpf.Core
{
    /// <summary>
    /// Helper class to access FrameworkApp
    /// </summary>
    public static class AppHelper
    {
        private static FrameworkApp app
        {
            get
            {
                var application = Application.Current as FrameworkApp;

                if (null == application)
                    Logger?.Log("Application.Current is not a FrameworkApp", typeof(AppHelper), LogCategory.Error, LogPriority.High);

                return application;
            }
        }

        public static ILogger Logger { get; internal set; }
        
        public static void Compose(params IPartImportsSatisfiedNotification[] parts)
        {
            app?.CompositionContainer.ComposeParts(parts);
        }

        public static object GetExportedObject(string alias)
        {
            var result = DynamicHelper.GetDynamicMember(FrameworkApp.ObjectLocator, alias);
            return result;
        }

        public static View GetView(string alias)
        {
            var result = DynamicHelper.GetDynamicMember(FrameworkApp.ViewLocator, alias) as View;
            return result;
        }

        public static T GetExportedValue<T>()
        {
            if (null != app)
                return app.CompositionContainer.GetExportedValue<T>();
            else
                return default(T);
        }
    }

}
