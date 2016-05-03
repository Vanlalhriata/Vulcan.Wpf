using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{

    // TODO: Do not use ObjectExport !!
    // Objects can be exported and imported normally without needing this extra infrastructure.
    // This class is used for converter export so maybe create a ConvererLocator instead
    // NOTE: Objects that are [ObjectExport]ed cannot be [Import]ed because of different contracts

    /// <summary>
    /// Imports IExportedObject implementations and distributes them.
    /// This class is imported and used in FrameworkApp.
    /// </summary>
    [Export(typeof(ObjectLocator))]
    public class ObjectLocator : DynamicObject, IPartImportsSatisfiedNotification
    {
        [ImportMany]
        private IEnumerable<Lazy<IExportedObject, IExportedObjectMetadata>> exportedObjects;

        [Import(typeof(ILogger), AllowDefault = true)]
        private ILogger logger;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;

            var lazy = exportedObjects?.FirstOrDefault(obj => obj.Metadata.Alias == name);
            result = lazy?.Value;

            // Error handling
            if (null == exportedObjects)
                logger?.Log("exportedObjects is null at TryGetMember", this.GetType(), LogCategory.Error, LogPriority.High);
            if (null == lazy)
                logger?.Log($"ExportedObject not found: {name}", this.GetType(), LogCategory.Warning, LogPriority.High);

            return null != result;
        }

        public void OnImportsSatisfied()
        {
            logger?.Log("Composition complete", this.GetType());
        }
    }
}
