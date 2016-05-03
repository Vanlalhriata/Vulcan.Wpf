using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    /// <summary>
    /// The base class for most objects that will take part in MEF composition
    /// </summary>
    public abstract class MvvmObject : ObservableObject, IPartImportsSatisfiedNotification
    {
        [Import(typeof(ILogger), AllowDefault = true)]
        protected ILogger logger;

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
