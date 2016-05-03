using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Vulcan.Wpf.Utils
{
    /// <summary>
    /// AnimatedStackPanel animates items whose index has changed.
    /// Note that the panel uses the container's Tag property
    /// </summary>
    public class AnimatedStackPanel : StackPanel
    {
        private const double animationDelayStep = 25;   // milliseconds
        private const double animationDuration = 500;   // milliseconds

        private DoubleAnimation opacityAnimation;
        private DoubleAnimation translateYAnimation;

        public AnimatedStackPanel()
        {
            opacityAnimation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(animationDuration));
            opacityAnimation.EasingFunction = new QuinticEase() { EasingMode = EasingMode.EaseOut };

            translateYAnimation = new DoubleAnimation(70, 0, TimeSpan.FromMilliseconds(animationDuration));
            translateYAnimation.EasingFunction = new QuinticEase() { EasingMode = EasingMode.EaseOut };
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double cumulativeDelay = 0;

            foreach (FrameworkElement child in Children)
            {
                int index = Children.IndexOf(child);

                if (null == child.Tag || index != (int)child.Tag)
                {
                    child.Opacity = 0;
                    opacityAnimation.BeginTime = TimeSpan.FromMilliseconds(cumulativeDelay);
                    child.BeginAnimation(FrameworkElement.OpacityProperty, opacityAnimation, HandoffBehavior.Compose);

                    var translateTransform = getTranslateTransform(child);
                    translateYAnimation.BeginTime = TimeSpan.FromMilliseconds(cumulativeDelay);
                    translateTransform.BeginAnimation(TranslateTransform.YProperty, translateYAnimation, HandoffBehavior.Compose);

                    child.Tag = index;
                    cumulativeDelay += animationDelayStep;
                }
            }

            return base.ArrangeOverride(arrangeSize);
        }

        private static TranslateTransform getTranslateTransform(FrameworkElement child)
        {
            var transformGroup = child.RenderTransform as TransformGroup;

            if (null == transformGroup)
            {
                child.RenderTransformOrigin = new Point(0.5, 0.5);
                transformGroup = new TransformGroup();
                transformGroup.Children.Add(new ScaleTransform());
                transformGroup.Children.Add(new SkewTransform());
                transformGroup.Children.Add(new RotateTransform());
                transformGroup.Children.Add(new TranslateTransform());
                child.RenderTransform = transformGroup;
            }

            var result = (TranslateTransform)transformGroup.Children.Single(t => t is TranslateTransform);
            return result;
        }
    }
}
