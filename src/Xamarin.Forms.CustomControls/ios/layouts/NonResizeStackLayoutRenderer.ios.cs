using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls.iOS.Layouts;
using Xamarin.Forms.CustomControls.Layouts;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NonResizeStackLayout), typeof(NonResizeStackLayoutRenderer))]
namespace Xamarin.Forms.CustomControls.iOS.Layouts
{
    class NonResizeStackLayoutRenderer : ViewRenderer<StackLayout, UIView>
    {
        #region overrides
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                e.NewElement.SizeChanged += StopResizing;
            if (e.OldElement != null)
                e.OldElement.SizeChanged -= StopResizing;
        }
        #endregion

        #region EventHandler
        /// <summary>
        /// Remove the margin from all child elements to stop resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void StopResizing(object sender, EventArgs args) =>
            ((NonResizeStackLayout)sender).Children.ToList().ForEach(x => x.Margin = new Thickness(0));
        #endregion 
    }
}
