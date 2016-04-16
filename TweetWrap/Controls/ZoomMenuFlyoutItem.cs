using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace TweetWrap.Controls
{
    public class ZoomMenuFlyoutItem : MenuFlyoutItem
    {
        Button zoomInButton;
        Button zoomOutButton;
        double zoomIncrement = .05d;

        public ZoomMenuFlyoutItem()
        {
            this.DefaultStyleKey = typeof(ZoomMenuFlyoutItem);
        }



        public string ZoomPercent
        {
            get { return (string)GetValue(ZoomPercentProperty); }
            private set { SetValue(ZoomPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZoomPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomPercentProperty =
            DependencyProperty.Register("ZoomPercent", typeof(string), typeof(ZoomMenuFlyoutItem), new PropertyMetadata(null));



        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Zoom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(double), typeof(ZoomMenuFlyoutItem), new PropertyMetadata(0, OnZoomPropertyChanged));

        private static void OnZoomPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zm = d as ZoomMenuFlyoutItem;
            zm.ZoomPercent = string.Format("{0}%", (int)((double)e.NewValue * 100)); 
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            zoomInButton = GetTemplateChild("zoomIn") as Button;
            zoomOutButton = GetTemplateChild("zoomOut") as Button;
            zoomInButton.Click += ZoomInButton_Click;
            zoomOutButton.Click += ZoomOutButton_Click;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (Zoom >= .2)
            {
                Zoom = Add(Zoom, -zoomIncrement);
            }
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (Zoom < 3)
            {
                Zoom = Add(Zoom, zoomIncrement);
            }
        }

        private double Add(double zoom, double zoomIncrement)
        {
            return Math.Round((zoom + zoomIncrement) * 100) / 100;
        }
    }
}
