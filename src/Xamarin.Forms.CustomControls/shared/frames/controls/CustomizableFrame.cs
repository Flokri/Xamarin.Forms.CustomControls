using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CustomControls.Frames
{
    public class CustomizableFrame : Frame
    {
        #region bindables
        public new static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(CustomizableFrame), new CornerRadius(0));

        public static readonly BindableProperty GradientBackgroundProperty =
            BindableProperty.Create(nameof(GradientBackground), typeof(bool), typeof(CustomizableFrame), false);

        public static readonly BindableProperty GradientOrientationProperty =
            BindableProperty.Create(nameof(GradientOrientation), typeof(Orientation), typeof(CustomizableFrame), Orientation.Horizontal);

        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(CustomizableFrame), default(Color));

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(CustomizableFrame), default(Color));
        #endregion

        #region constructor
        public CustomizableFrame()
        {
            // this will fix the height and width requests
            this.Padding = 0;
        }
        #endregion

        #region properties
        /// <summary>
        /// the corner radius of the 
        /// </summary>
        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Specify if the background color should be a gradient
        /// </summary>
        public bool GradientBackground
        {
            get => (bool)GetValue(GradientBackgroundProperty);
            set => SetValue(GradientBackgroundProperty, value);
        }

        /// <summary>
        /// The orientation of the background gradient.
        /// </summary>
        public Orientation GradientOrientation
        {
            get => (Orientation)GetValue(GradientOrientationProperty);
            set => SetValue(GradientOrientationProperty, value);
        }

        /// <summary>
        /// The start color of the gradient background
        /// </summary>
        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        /// <summary>
        /// The end color of the gradient background
        /// </summary>
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }
        #endregion
    }
}
