using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls.Layouts;
using Xamarin.Forms.CustomControls.iOS.Layouts;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(KeyboardView), typeof(KeyboardViewRenderer))]
namespace Xamarin.Forms.CustomControls.iOS.Layouts
{
    public class KeyboardViewRenderer : ViewRenderer
    {
        #region instances
        NSObject _showObserver;
        NSObject _hideObserver;
        #endregion

        #region overrides
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            // disable the animation to prevent that the controls are flickering when the margin changes
            AnimationsEnabled = false;

            if (e.NewElement != null)
            {
                // initialte the observer
                _showObserver = _showObserver ?? UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
                _hideObserver = _hideObserver ?? UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
            }
            if (e.OldElement != null)
            {
                // dispose the observer
                _showObserver?.Dispose();
                _hideObserver?.Dispose();
            }
        }
        #endregion

        #region EventHandler
        /// <summary>
        /// Will be called when the keyboard will be shown and set the correct margin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnKeyboardShow(object sender, UIKeyboardEventArgs args)
        {
            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;

            if (Element != null)
            {
                // push the view up to keyboard heigth when keyboard is activated
                Element.Margin = new Thickness(0, 0, 0, keyboardSize.Height);
            }
        }

        /// <summary>
        /// Will be called when the keyboard will be dismissed and removes the margin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
            // if the element is not null, set the margin to zero when the keyboard is dismisssed
            if (Element != null)
                Element.Margin = new Thickness(0);
        }
        #endregion
    }
}
