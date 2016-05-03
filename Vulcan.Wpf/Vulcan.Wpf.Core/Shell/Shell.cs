using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Vulcan.Wpf.Core
{
    /// <summary>
    /// Class to be used as the main Window in the WPF application
    /// </summary>
    public class Shell : Window
    {
        public static readonly DependencyProperty StartupViewProperty = DependencyProperty.Register("StartupViewName", typeof(string), typeof(Shell));
        public string StartUpView
        {
            get { return (string)GetValue(StartupViewProperty); }
            set { SetValue(StartupViewProperty, value); }
        }
        
        public Shell()
        {
            this.Loaded += Shell_Loaded;

            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.ResizeMode = ResizeMode.NoResize;

            this.SourceInitialized += onSourceInitialized;
        }

        private void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            var frame = VisualHelper.FindVisualChild<Frame>(this);

            if (null != frame)
            {
                AppNavigator.SetNavigationService(frame.NavigationService);
                if (null != StartUpView)
                    AppNavigator.NavigateTo(StartUpView);
                else
                    throw new ArgumentNullException(nameof(StartUpView));
            }
            else
                AppHelper.Logger.Log("Could not find Frame in Children", this.GetType(), LogCategory.Error, LogPriority.High);
        }

        #region Fullscreen with taskbar
        
        private void onSourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new WindowInteropHelper(this)).Handle;
            HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(FullscreenHandler.WindowProc));
        }

        #endregion Fullscreen with taskbar
    }
}
