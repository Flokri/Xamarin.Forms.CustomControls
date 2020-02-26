using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControlsSample.viewmodels
{
    public class MainViewModel
    {
        #region services
        INavigationService _navigationService;
        #endregion

        #region commands
        public ICommand NavToEntryPage { get; set; }
        #endregion

        #region constructor
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavToEntryPage = new Command(() => _navigationService.NavigateAsync("EntrySampleView"));
        }
        #endregion
    }
}
