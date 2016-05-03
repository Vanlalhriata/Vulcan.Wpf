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
    public class ViewModelExportAttribute : ExportAttribute
    {
        public string Alias { get; private set; }

        public ViewModelExportAttribute(string alias)
            : base(typeof(ViewModelBase))
        {
            Alias = alias;
        }
    }
}
