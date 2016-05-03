using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Vulcan.Wpf.Core
{
    /// <summary>
    /// Class to replace the Application in a WPF application
    /// </summary>
    public class FrameworkApp : Application
    {
        public static ObjectLocator ObjectLocator { get; private set; }
        public static ViewModelLocator ViewModelLocator { get; private set; }
        public static ViewLocator ViewLocator { get; private set; }

        internal CompositionContainer CompositionContainer { get; set; }

        private ILogger logger;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, "."));

            CompositionContainer = new CompositionContainer(aggregateCatalog);

            logger = CompositionContainer.GetExportedValue<ILogger>();
            logger?.Log("Starting application", this.GetType());
            AppHelper.Logger = logger;
            
            var resourceDictionaries = CompositionContainer.GetExportedValues<ResourceDictionary>();
            foreach (var resourceDictionary in resourceDictionaries)
                this.Resources.MergedDictionaries.Add(resourceDictionary);

            ObjectLocator = CompositionContainer.GetExportedValue<ObjectLocator>();
            ViewModelLocator = CompositionContainer.GetExportedValue<ViewModelLocator>();
            ViewLocator = CompositionContainer.GetExportedValue<ViewLocator>();
            
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            logger?.Log($"Closing application{Environment.NewLine}-----", this.GetType());
            base.OnExit(e);
        }
    }
}
