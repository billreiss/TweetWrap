using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using TweetWrap.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TweetWrap.Views
{
    public sealed partial class MainPage : Page
    {
        CoreApplicationViewTitleBar coreTitleBar;
        MainViewModel Model = new MainViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            this.SizeChanged += MainPage_SizeChanged;
            Model.PropertyChanged += Model_PropertyChanged;
            this.Loaded += MainPage_Loaded;
            coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(mainTitleBar);
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            ResizeWebView();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeWebView();
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Zoom")
            {
                ResizeWebView();
            }
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeWebView();
        }

        void ResizeWebView()
        {
            if (coreTitleBar == null) return;
            var tbHeight = coreTitleBar.Height;
            var rightInset = coreTitleBar.SystemOverlayRightInset;
            titleRoot.ColumnDefinitions[2].Width = new GridLength(rightInset);
            layoutRoot.RowDefinitions[0].Height = new GridLength(tbHeight);
            var wvHeight = this.ActualHeight - tbHeight;
            MyWebView.Width = this.ActualWidth / Model.Zoom;
            MyWebView.Height = wvHeight / Model.Zoom;
            scale.ScaleX = Model.Zoom;
            scale.ScaleY = Model.Zoom;
        }

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {

        }

        public ICommand Refresh
        {
            get 
            {
                return new RelayCommand(() =>
                {
                    MyWebView.Navigate(new Uri("https://tweetdeck.twitter.com"));
                });
            }
        }

        public ICommand About
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await new MessageDialog("TweetWrap is a simple wrapper around the TweetDeck web site. This project is open source and accepting code contributions at https://github.com/billreiss/TweetWrap", "About TweetWrap").ShowAsync();
                });
            }
        }
    }
}
