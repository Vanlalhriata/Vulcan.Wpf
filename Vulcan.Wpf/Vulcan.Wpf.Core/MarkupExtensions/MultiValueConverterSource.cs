using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Vulcan.Wpf.Core
{
    [MarkupExtensionReturnType(typeof(IMultiValueConverter))]
    public class MultiValueConverterSource : MarkupExtension
    {
        public string Alias { get; set; }

        public MultiValueConverterSource()
        {
        }

        public MultiValueConverterSource(string alias)
            : base()
        {
            this.Alias = alias;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrEmpty(Alias))
            {
                AppHelper.Logger.Log("Alias was not provided", this.GetType(), LogCategory.Error, LogPriority.Medium);
                return null;
            }

            // Converters are exported as IExportedObject
            return AppHelper.GetExportedObject(Alias);
        }
    }
}
