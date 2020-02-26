using System;

using Xamarin.Forms.CustomControls.Extensions;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.CustomControls.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingButtonMenu : ContentView
    {
        #region instances
        private bool _isExtended;
        #endregion

        public FloatingButtonMenu()
        {
            InitializeComponent();
        }

        #region event handler

        private void Tapped(object sender, EventArgs e)
        {
            if (_isExtended)
                CollapsMenu();
            else
                ExtendMenu();
        }
        #endregion

        #region privates

        private void ExtendMenu()
        {
            // get the end point of the translate animation
            double buttonRad = menu.Bounds.Width;
            double menuLength = grid.Bounds.Width / 2;
            double buttonEndX = (grid.Bounds.Width / 2) + (menuLength / 2) - buttonRad;

            menu.TranslateTo(
                x: (menu.Bounds.X - buttonEndX) * (-1),
                y: grid.Bounds.Y,
                length: 150);

            // expand the button to the menu
            menu.ExpandTo(menu.Width, menuLength, (w) => { menu.WidthRequest = w; }, 200, Easing.BounceOut);

            _isExtended = true;
        }

        private void CollapsMenu()
        {
            // shrink the menu to the original size
            menu.ExpandTo(menu.Width, 50, (w) => { menu.WidthRequest = w; }, 300);

            menu.TranslateTo(
                x: 10,
                y: grid.Bounds.Y,
                length: 150);
            _isExtended = false;
        }
        #endregion
    }
}