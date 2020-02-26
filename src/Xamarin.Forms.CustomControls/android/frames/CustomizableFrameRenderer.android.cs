using Android.Content;
using Android.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls.Android.Frames;
using Xamarin.Forms.CustomControls.Frames;
using Xamarin.Forms.Platform.Android;

using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(CustomizableFrame), typeof(CustomizableFrameRenderer))]
namespace Xamarin.Forms.CustomControls.Android.Frames
{
    public class CustomizableFrameRenderer : FrameRenderer
    {
        #region instances 
        GradientDrawable _gradient;
        #endregion

        public CustomizableFrameRenderer(Context context) : base(context) { }

        #region overrides
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
            {
                var frame = e.NewElement as CustomizableFrame;

                if (!frame.GradientBackground)
                    Control.SetBackgroundColor(frame.StartColor.ToAndroid());
                else
                    Control.SetBackground(DrawGradient(e));

                SetCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(CustomizableFrame.CornerRadius) ||
                e.PropertyName == nameof(CustomizableFrame))
                SetCornerRadius();
        }
        #endregion

        #region privates
        /// <summary>
        /// Create the gradient for the button background
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private GradientDrawable DrawGradient(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            var frame = e.NewElement as CustomizableFrame;
            var orientation = frame.GradientOrientation == Orientation.Horizontal ?
                GradientDrawable.Orientation.LeftRight : GradientDrawable.Orientation.TopBottom;

            _gradient = new GradientDrawable(orientation, new[] {
                frame.StartColor.ToAndroid().ToArgb(),
                frame.EndColor.ToAndroid().ToArgb(),
            });

            _gradient.SetStroke(0, frame.StartColor.ToAndroid());

            return _gradient;
        }

        /// <summary>
        /// set the new corner radius for the frame
        /// </summary>
        private void SetCornerRadius()
        {
            if (Control.Background is GradientDrawable backgroundGradient)
            {
                var cornerRadius = (Element as CustomizableFrame)?.CornerRadius;
                if (!cornerRadius.HasValue)
                    return;

                var topLeftCorner = Context.ToPixels(cornerRadius.Value.TopLeft);
                var topRightCorner = Context.ToPixels(cornerRadius.Value.TopRight);
                var bottomLeftCorner = Context.ToPixels(cornerRadius.Value.BottomLeft);
                var bottomRightCorner = Context.ToPixels(cornerRadius.Value.BottomRight);

                backgroundGradient.SetCornerRadii(new[]
                    {
                        topLeftCorner,
                        topLeftCorner,

                        topRightCorner,
                        topRightCorner,

                        bottomRightCorner,
                        bottomRightCorner,

                        bottomLeftCorner,
                        bottomLeftCorner,
                    });
            }
        }
        #endregion
    }
}
