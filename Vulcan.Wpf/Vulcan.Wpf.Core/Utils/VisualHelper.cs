using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Vulcan.Wpf.Core
{
    public static class VisualHelper
    {
        /// <summary>
        /// Obtains the first descendent element that matches the type.
        /// </summary>
        public static T FindVisualChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            if (null == parent)
                return null;

            int numChildren = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numChildren; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var result = (child as T) ?? FindVisualChild<T>(child);

                if (null != result)
                    return result;
            }

            return null;
        }

        /// <summary>
        /// Obtains the first ancestor element that matches the type.
        /// </summary>
        public static T FindVisualParent<T>(DependencyObject child)
           where T : DependencyObject
        {

            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return FindVisualParent<T>(parentObject);
            }
        }

        /// <summary>
        /// Obtains immediate children of the element. 
        /// </summary>
        public static IEnumerable<DependencyObject> GetChildren(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                yield return VisualTreeHelper.GetChild(parent, i);
        }

        /// <summary>
        /// Obtains immediate children of the provided object by examining the visual tree and filtering on element type
        /// </summary>
        public static IEnumerable<T> GetChildren<T>(DependencyObject parent)
            where T : FrameworkElement
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                    if (child != null && child is T)
                        yield return (T)child;
                }
            }
        }

        /// <summary>
        /// Obtains children and their children and so on of the provided object by examining the visual tree. 
        /// </summary>
        public static IEnumerable<DependencyObject> GetAllChildren(DependencyObject parent)
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject childElement = VisualTreeHelper.GetChild(parent, i);
                yield return childElement;
                foreach (var subChildElement in GetAllChildren(childElement).ToArray())
                {
                    yield return subChildElement;
                }
            }
        }

        /// <summary>
        /// Obtains children and their children and so on of the provided object by examining the visual tree and filtering on the element type
        /// </summary>
        public static IEnumerable<DependencyObject> GetAllChildren<T>(DependencyObject parent)
            where T : DependencyObject
        {
            return GetAllChildren(parent).ToArray().Where(args => args is T);
        }

        /// <summary>
        /// Obtains parents of the provided object by examining the visual tree. 
        /// </summary>
        public static IEnumerable<DependencyObject> GetParents(DependencyObject element)
        {
            var parent = VisualTreeHelper.GetParent(element);

            if (parent != null)
            {
                yield return parent;

                foreach (var grandParent in GetParents(parent).ToArray())
                {
                    yield return grandParent;
                }
            }
        }

        /// <summary>
        /// Obtains parents of the provided object by examining the visual tree and filtering on the element type
        /// </summary>
        public static IEnumerable<DependencyObject> GetParents<T>(DependencyObject element)
            where T : DependencyObject
        {
            return GetParents(element).ToArray().Where(args => args is T);
        }

        /// <summary>
        /// Find element among immediate children by name.
        /// </summary>
        public static FrameworkElement FindName(DependencyObject reference, string name)
        {
            return FindName<FrameworkElement>(reference, name);
        }

        /// <summary>
        /// Find element among immediate children by name and type
        /// </summary>
        public static T FindName<T>(DependencyObject reference, string name)
            where T : FrameworkElement
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (reference == null)
            {
                throw new ArgumentNullException("reference");
            }

            return FindNameInternal<T>(reference, name);
        }

        private static T FindNameInternal<T>(DependencyObject reference, string name)
            where T : FrameworkElement
        {
            foreach (DependencyObject obj in GetChildren(reference))
            {
                T elem = obj as T;
                if (elem != null && elem.Name == name)
                {
                    return elem;
                }

                elem = FindNameInternal<T>(obj, name);
                if (elem != null)
                {
                    return elem;
                }
            }
            return null;
        }
    }
}
