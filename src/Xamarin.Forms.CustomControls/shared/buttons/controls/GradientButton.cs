namespace Xamarin.Forms.CustomControls.Buttons
{
    /// <summary>
    /// A button with a gradient background
    /// </summary>
    public class GradientButton : Button
    {
        #region bindable properties
        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(
                nameof(StartColor),
                typeof(Color),
                typeof(GradientButton),
                default(Color));

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(
                nameof(EndColor),
                typeof(Color),
                typeof(GradientButton),
                default(Color));

        public static readonly BindableProperty GradientOrientationProperty =
            BindableProperty.Create(
                nameof(GradientOrientation),
                typeof(Orientation),
                typeof(GradientButton),
                default(Orientation));
        #endregion

        #region properties
        /// <summary>
        /// The start color of the gradient background
        /// </summary>
        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        /// <summary>
        ///  The end color of the gradient background
        /// </summary>
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        /// <summary>
        ///  The gradient color orientation
        /// </summary>
        public Orientation GradientOrientation
        {
            get => (Orientation)GetValue(GradientOrientationProperty);
            set => SetValue(GradientOrientationProperty, value);
        }
        #endregion
    }
}
