using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Vulcan.Wpf.Utils
{
    /// <summary>
    /// When any child of the attached UIElement gets focus,
    /// all text in the child will get selected.
    /// </summary>
    public class SelectAllTextOnFocusMultiBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += HandleKeyboardFocus;
            //AssociatedObject.GotMouseCapture += HandleMouseCapture;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= HandleKeyboardFocus;
            //AssociatedObject.GotMouseCapture -= HandleMouseCapture;
        }

        private static void HandleKeyboardFocus(object sender,
            System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            var txt = e.NewFocus as TextBox;
            if (txt != null)
                txt.SelectAll();
        }

        //private static void HandleMouseCapture(object sender,
        //    System.Windows.Input.MouseEventArgs e)
        //{
        //    var txt = e.OriginalSource as TextBox;
        //    if (txt != null)
        //        txt.SelectAll();
        //}
    }
}
