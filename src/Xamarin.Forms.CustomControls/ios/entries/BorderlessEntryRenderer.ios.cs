using Xamarin.Forms;
using Xamarin.Forms.CustomControls.Entries;
using Xamarin.Forms.CustomControls.iOS.Entries;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace Xamarin.Forms.CustomControls.iOS.Entries
{
    /// <summary>
    /// ios renderer for the borderless entry
    /// </summary>
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // remove the border form the entry
            if (Control != null)
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}
