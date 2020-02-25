using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls.Android.Entries;
using Xamarin.Forms.CustomControls.Entries;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace Xamarin.Forms.CustomControls.Android.Entries
{
    /// <summary>
    /// Custom renderer for the borderless entry
    /// </summary>
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
                Control.Background = null;
        }
    }
}
