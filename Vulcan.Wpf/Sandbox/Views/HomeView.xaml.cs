using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vulcan.Wpf.Core;

namespace Sandbox.Views
{
    [ViewExport(nameof(HomeView))]
    public partial class HomeView : View
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}
