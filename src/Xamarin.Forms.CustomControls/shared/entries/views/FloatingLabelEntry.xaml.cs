﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms.CustomControls.Extensions;

namespace Xamarin.Forms.CustomControls.Entries
{
    public partial class FloatingLabelEntry : FloatingLabelBase
    {
        #region instances
        const int TITLE_FONT_SIZE = 13;
        const int PLACEHOLDER_FONT_SIZE = 18;

        private int _labelStartX;

        public event EventHandler Completed;

        public AsyncEvent<EventArgs> BeforeTitleToPlaceholderAsync;
        public AsyncEvent<EventArgs> TitleToPlaceholderAsync;
        public AsyncEvent<EventArgs> AfterTitleToPlaceholderAsync;

        public AsyncEvent<EventArgs> BeforePlaceholderToTitleAsync;
        public AsyncEvent<EventArgs> PlaceholderToTitleAsync;
        public AsyncEvent<EventArgs> AfterPlaceholderToTitleAsync;

        public AsyncEvent<EventArgs> TextChangedHandlerAsync;
        #endregion


        public FloatingLabelEntry()
        {
            InitializeComponent();

            LabelStartX = 5;

            PlaceholderLabel.FontSize = PLACEHOLDER_FONT_SIZE;

            // fixing the problem with the height changing when user taps the label
            this.SizeChanged += (sender, e) => HeightRequest = ((FloatingLabelEntry)sender).Height;

            @this.PropertyChanged += async (sender, args) =>
            {
                if (!args.PropertyName.Equals(nameof(AutoStartText))) return;
                if (!string.IsNullOrEmpty(AutoStartText)) return;

                Text = AutoStartText;
                await PlaceholderToTitle();
            };

            // invoke the eventhandler when text changes
            BorderlessEntry.TextChanged += async (sender, e) =>
                await (TextChangedHandlerAsync?.InvokeAsync(sender, e) ?? Task.CompletedTask);


            this.IsClippedToBounds = false;
        }

        #region event handler
        /// <summary>
        /// Handle when the entry is focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void HandleFocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                await (BeforePlaceholderToTitleAsync?.InvokeAsync(sender, e) ?? Task.CompletedTask);
                await PlaceholderToTitle();
                await (AfterPlaceholderToTitleAsync?.InvokeAsync(sender, e) ?? Task.CompletedTask);
            }
        }

        /// <summary>
        /// Handle when the entry is unfocused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void HandleUnfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                await (BeforeTitleToPlaceholderAsync?.InvokeAsync(sender, e) ?? Task.CompletedTask);
                await TitleToPlaceholder();
                await (AfterTitleToPlaceholderAsync?.InvokeAsync(sender, e) ?? Task.CompletedTask);
            }
        }

        /// <summary>
        /// Handle when the placeholder is tapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleTapped(object sender, EventArgs e) => BorderlessEntry.Focus();

        /// <summary>
        /// Handle when the entry action is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleCompleted(object sender, EventArgs e) => Completed?.Invoke(this, e);
        #endregion

        #region privates
        /// <summary>
        /// Transform the placeholder to title 
        /// </summary>
        /// <returns></returns>
        async Task PlaceholderToTitle()
        {
            if (Animated)
            {
                await Task.WhenAll(
                    PlaceholderLabel.TranslateTo(
                        x: 0,
                        y: BorderlessEntry.Y - PlaceholderLabel.Height,
                        length: 100),
                    PlaceholderLabel.SizeTo(
                        startSize: PlaceholderLabel.FontSize,
                        endSize: TITLE_FONT_SIZE,
                        callback: (t) => PlaceholderLabel.FontSize = t,
                        length: 100,
                        easing: Easing.BounceIn),
                    PlaceholderLabel.ColorTo(
                        startColor: PlaceholderColor,
                        endColor: TitleColor,
                        callback: (c) => PlaceholderLabel.TextColor = c,
                        length: 100),
                    (PlaceholderToTitleAsync?.InvokeAsync(this, new EventArgs())) ?? Task.CompletedTask);
            }
            else
            {
                PlaceholderLabel.TranslationX = 0;
                PlaceholderLabel.TranslationY = BorderlessEntry.Y - PlaceholderLabel.Height;
                PlaceholderLabel.FontSize = TITLE_FONT_SIZE;
            }
        }

        /// <summary>
        /// Transform the title to placeholder
        /// </summary>
        /// <returns></returns>
        async Task TitleToPlaceholder()
        {
            if (Animated)
            {
                await Task.WhenAll(
                    PlaceholderLabel.TranslateTo(
                        x: LabelStartX,
                        y: 0,
                        length: 100),
                    PlaceholderLabel.SizeTo(
                        startSize: PlaceholderLabel.FontSize,
                        endSize: PLACEHOLDER_FONT_SIZE,
                        callback: (t) => PlaceholderLabel.FontSize = t,
                        length: 100,
                        easing: Easing.BounceIn),
                    PlaceholderLabel.ColorTo(
                        startColor: TitleColor,
                        endColor: PlaceholderColor,
                        callback: (c) => PlaceholderLabel.TextColor = c,
                        length: 100),
                    (TitleToPlaceholderAsync?.InvokeAsync(this, new EventArgs())) ?? Task.CompletedTask);
            }
            else
            {
                PlaceholderLabel.TranslationX = 10;
                PlaceholderLabel.TranslationY = 0;
                PlaceholderLabel.FontSize = PLACEHOLDER_FONT_SIZE;
                Grid.Margin = new Thickness(0, 0, 0, 0);
            }
        }
        #endregion

        #region properties
        /// <summary>
        /// The font size of the title
        /// </summary>
        public int TitleFontSize
        {
            get => TITLE_FONT_SIZE;
        }

        /// <summary>
        /// Get or set the start x value of the placeholder label
        /// </summary>
        public int LabelStartX
        {
            get => _labelStartX;
            set
            {
                _labelStartX = value;
                PlaceholderLabel.TranslationX = LabelStartX;
            }
            #endregion
        }
    }
}