using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    /// <summary>
    /// Imports ViewModelBase subclasses and distributes them.
    /// This class is imported and used in FrameworkApp.
    /// </summary>
    [Export(typeof(ViewModelLocator))]
    public class ViewModelLocator : DynamicObject, IPartImportsSatisfiedNotification
    {
        [ImportMany]
        private IEnumerable<Lazy<ViewModelBase, IViewModelMetadata>> viewModels { get; set; }

        [Import(typeof(ILogger), AllowDefault = true)]
        private ILogger logger;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            
            var lazy = viewModels?.FirstOrDefault(vm => vm.Metadata.Alias == name);
            result = lazy?.Value;

            // Error handling
            if (null == viewModels)
                logger?.Log("viewModels is null at TryGetMember", this.GetType(), LogCategory.Error, LogPriority.High);
            if (null == lazy)
                logger?.Log($"ViewModel not found: {name}", this.GetType(), LogCategory.Warning, LogPriority.High);

            return null != result;
        }

        public void OnImportsSatisfied()
        {
            logger?.Log("Composition complete", this.GetType());
        }
    }
}