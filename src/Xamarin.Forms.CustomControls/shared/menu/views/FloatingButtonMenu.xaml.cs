using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.CustomControls.Extensions;

namespace Xamarin.Forms.CustomControls.Menu
{
    public partial class FloatingButtonMenu : ContentView, INotifyPropertyChanged
    {
        #region instances
        private bool _isExtended;

        private double _itemWidth;

        public new event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region commands
        private ICommand _onActionButtonTapped;
        private ICommand _onExtendedTapped;
        #endregion

        #region bindable properties
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(FloatingButtonMenu), Color.Default);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FloatingButtonMenu), string.Empty, BindingMode.OneWay, null, (bindable, oldV, newV) => { ((FloatingButtonMenu)bindable).menuButton.Text = (string)newV; });
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(List<ActionItem>), typeof(FloatingButtonMenu), new List<ActionItem>(), BindingMode.OneWay, null,
            (bindable, oldV, newV) =>
                {
                    ((FloatingButtonMenu)bindable).actionButtonCollection.ItemsSource = (List<ActionItem>)newV;
                }
            );
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(FloatingButtonMenu), string.Empty, BindingMode.OneWay, null, (bindable, oldV, newV) => { ((FloatingButtonMenu)bindable).menuButton.FontFamily = (string)newV; });
        #endregion

        #region properties
        /// <summary>
        /// The background color of the selected item
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// The command that gets executed if the user tap a action button
        /// </summary>
        public ICommand OnActionButtonTapped
        {
            get => _onActionButtonTapped;
            set
            {
                _onActionButtonTapped = value;
                NotifyPorpertyChanged();
            }
        }

        /// <summary>
        /// The command thats gets exectuted if the user taps the menu button
        /// </summary>
        public ICommand OnExtendedTapped
        {
            get => _onExtendedTapped;
            set
            {
                _onExtendedTapped = value;
                NotifyPorpertyChanged();
            }
        }

        /// <summary>
        /// The icon of the menu button
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The collection of action items
        /// </summary>
        public List<ActionItem> ItemsSource
        {
            get => (List<ActionItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The font family of the menu button text
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// States wheter the menu is extended or not
        /// </summary>
        public bool IsExtended
        {
            get => _isExtended;
            set
            {
                _isExtended = value;
                NotifyPorpertyChanged();
            }
        }

        /// <summary>
        /// The width of an single action item
        /// </summary>
        public double CalculatedWidth
        {
            get => _itemWidth;
            set
            {
                _itemWidth = value;
                NotifyPorpertyChanged();
            }
        }
        #endregion

        #region constructor
        public FloatingButtonMenu()
        {
            InitializeComponent();

            OnExtendedTapped = new Command(() => Tapped(null, null));

            menu.SizeChanged += MenuSizeChanged;

            this.SizeChanged += ControlSizeChanged;

            OnActionButtonTapped = new Command((sender) => ActionButtonTapped(sender));

            SelectedColor = Color.FromHex("#e42c64");
        }
        #endregion

        #region publics
        #endregion

        #region privates
        private void NotifyPorpertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        #region events
        /// <summary>
        /// Get called when the user taps an action item
        /// </summary>
        /// <param name="sender">The action item the user taps</param>
        private async void ActionButtonTapped(object sender)
        {
            ActionItem tapped = (ActionItem)sender;

            // reset the is selected property
            ItemsSource.ForEach(i => i.IsSelected = false);

            // execute the code bevor the item command

            // execut the item command
            tapped.Cmd?.Execute(tapped.Param);

            // set the correct item as selected one
            ItemsSource.ForEach(i =>
            {
                if (i.Equals(tapped))
                    i.IsSelected = true;
            });

            // execute the code after the item command
            Tapped(null, null);
        }

        /// <summary>
        /// Gets called when the menu size is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuSizeChanged(object sender, EventArgs e)
        {
            actionButtonCollection.WidthRequest = menu.Width;

            if (ItemsSource.Count > 0)
            {
                try
                {
                    // get the width and add the left and right margin and one save pixel
                    CalculatedWidth = (menu.Width - actionButtonCollection.Margin.Right - actionButtonCollection.Margin.Left - 1) / ItemsSource.Count;
                }
                catch (ObjectDisposedException ex)
                {
                    if (!IsExtended)
                        CalculatedWidth = this.Height;
                }
            }
        }

        /// <summary>
        /// Gets called when the entire controll size gets changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSizeChanged(object sender, EventArgs e)
        {
            menu.CornerRadius = menu.Height / 2;
            menu.HeightRequest = this.Height;
            menu.WidthRequest = this.Height;
        }

        /// <summary>
        /// Gets called when the user taps the menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tapped(object sender, EventArgs e)
        {
            var test = this.menu.CornerRadius;

            if (IsExtended)
                CollapsMenu();
            else
                ExtendMenu();
        }

        #endregion

        #region privates
        /// <summary>
        /// Extend the menu button to the floating menu
        /// </summary>
        private async void ExtendMenu()
        {
            // get the end point of the translate animation
            double buttonRad = menu.Bounds.Width;
            double menuLength = grid.Bounds.Width / 2;
            double buttonEndX = (grid.Bounds.Width / 2) + (menuLength / 2) - buttonRad;

            var gridMargin = grid.Margin;

            await Task.WhenAll(
                    menu.TranslateTo(
                        x: (menu.Bounds.X - buttonEndX) * (-1),
                        y: grid.Bounds.Y,
                        length: 150),

                    menu.ExpandTo(menu.Width, menuLength, (w) => { menu.WidthRequest = w; }, 150, Easing.SinOut)
            );

            IsExtended = true;
        }

        /// <summary>
        /// Collaps the floating menu to an action button 
        /// </summary>
        private async void CollapsMenu()
        {
            await Task.WhenAll(
                menu.TranslateTo(
                x: 10,
                y: grid.Bounds.Y,
                length: 150),

                menu.ExpandTo(menu.Width, this.menu.Bounds.Height, (w) => { menu.WidthRequest = w; }, 150)
            );

            IsExtended = false;
        }
    }
    #endregion
}