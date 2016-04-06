using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetWrap.Models;

namespace TweetWrap.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        double zoom;

        public MainViewModel()
        {
            Zoom = SettingsModel.Get("zoom", 1d);
        }

        // Zoom handles the scale factor of the web view. A value of 1 means 100% scale.
        public double Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                RaisePropertyChanged(() => Zoom);
                SettingsModel.Set("zoom", Zoom);
            }
        }
    }
}
