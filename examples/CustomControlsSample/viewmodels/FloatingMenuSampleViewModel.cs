using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls;
using Xamarin.Forms.CustomControls.Helpers;
using Xamarin.Forms.CustomControls.Menu;

namespace CustomControlsSample.viewmodels
{
    public class FloatingMenuSampleViewModel : BindableBase
    {
        #region instances
        private List<ActionItem> _items;
        #endregion

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

            Items = new List<ActionItem>
            {
                new ActionItem{Icon = FontAwesomeIcon.Home, FontFamily = FontAwesomeStyles.FontAwesomeSolid(), IsSelected=true},
                new ActionItem{Icon = FontAwesomeIcon.Search, FontFamily = FontAwesomeStyles.FontAwesomeSolid()},
                new ActionItem{Icon = FontAwesomeIcon.Cog, FontFamily = FontAwesomeStyles.FontAwesomeSolid()},
                new ActionItem{Icon = FontAwesomeIcon.User, FontFamily = FontAwesomeStyles.FontAwesomeSolid()},
            };
        }
        #endregion

        #region properties
        public List<ActionItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        #endregion
    }
}
