using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ObjectExportAttribute : ExportAttribute
    {
        public string Alias { get; private set; }

        public ObjectExportAttribute(string alias)
            : base(typeof(IExportedObject))
        {
            Alias = alias;
        }
    }
}
