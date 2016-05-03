using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    /// <summary>
    /// Imports View subclasses and distributes them.
    /// This class is imported and used in FrameworkApp.
    /// </summary>
    [Export(typeof(ViewLocator))]
    public class ViewLocator : DynamicObject, IPartImportsSatisfiedNotification
    {
        [ImportMany]
        private IEnumerable<Lazy<View, IViewMetadata>> views { get; set; }

        [Import(typeof(ILogger), AllowDefault = true)]
        private ILogger logger;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;

            var lazy = views?.FirstOrDefault(v => v.Metadata.Alias == name);
            result = lazy?.Value;

            // Error handling
            if (null == views)
                logger?.Log("views is null at TryGetMember", this.GetType(), LogCategory.Error, LogPriority.High);
            if (null == lazy)
                logger?.Log($"View not found: {name}", this.GetType(), LogCategory.Warning, LogPriority.High);

            return null != result;
        }

        public void OnImportsSatisfied()
        {
            logger?.Log("Composition complete", this.GetType());
        }
    }
}
