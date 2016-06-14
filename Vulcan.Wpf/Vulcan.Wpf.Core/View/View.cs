using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Vulcan.Wpf.Core
{
    public class View : UserControl, IPartImportsSatisfiedNotification
    {
        [Import(typeof(ILogger), AllowDefault = true)]
        protected ILogger logger;

        public View()
        {
            this.Loaded += onLoaded;
        }

        private void onLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = this.DataContext as ViewModelBase;
            vm?.OnViewLoaded();
        }

        public void OnImportsSatisfied()
        {
            logger?.Log("Composition complete", this.GetType());
            this.onReady();
        }

        protected virtual void onReady()
        {
        }
    }
}
