using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls.Android.Buttons;
using Xamarin.Forms.CustomControls.Buttons;
using Xamarin.Forms.Platform.Android;
using Native = Android;

[assembly: ExportRenderer(typeof(GradientButton), typeof(GradientButtonRenderer))]
namespace Xamarin.Forms.CustomControls.Android.Buttons
{
    class GradientButtonRenderer : ButtonRenderer
    {
        #region instances 
        GradientDrawable _gradient;
        #endregion

        #region constructor
        public GradientButtonRenderer(Context context) : base(context) { }
        #endregion

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                Control.Touch -= ButtonTouched;
            }

            if (Control != null)
            {
                try
                {
                    Control.Touch += ButtonTouched;
                    Control.StateListAnimator = new Native.Animation.StateListAnimator();
                    Control.SetBackground(DrawGradient(e));
                }
                catch
                {
                    // ignored
                }
            }
        }

        #region EventHandler
        /// <summary>
        /// Draw the gradient with the correct oppacity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTouched(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            if (e.Event.Action == MotionEventActions.Down)
            {
                _gradient.Alpha = 200;
                Control.SetBackground(_gradient);
            }
            else if (e.Event.Action == MotionEventActions.Up)
            {
                _gradient.Alpha = 255;
                Control.SetBackground(_gradient);
            }
        }
        #endregion

        #region privates
        /// <summary>
        /// Create the gradient for the button background
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private GradientDrawable DrawGradient(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            var button = e.NewElement as GradientButton;
            var orientation = button.GradientOrientation == Orientation.Horizontal ?
                GradientDrawable.Orientation.LeftRight : GradientDrawable.Orientation.TopBottom;

            _gradient = new GradientDrawable(orientation, new[] {
                button.StartColor.ToAndroid().ToArgb(),
                button.EndColor.ToAndroid().ToArgb(),
            });

            _gradient.SetCornerRadius(button.CornerRadius * 10);
            _gradient.SetStroke(0, button.StartColor.ToAndroid());

            return _gradient;
        }
        #endregion
    }
}
