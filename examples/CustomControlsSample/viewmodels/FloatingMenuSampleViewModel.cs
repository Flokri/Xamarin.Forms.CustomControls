using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControlsSample.viewmodels
{
    public class FloatingMenuSampleViewModel
    {
        #region services
        INavigationService _navigationService;
        #endregion

        #region commands
        public ICommand GoBack { get; set; }
        #endregion

        #region constructor
        public FloatingMenuSampleViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBack = new Command(() => _navigationService.GoBackAsync());
        }
        #endregion
    }
}
