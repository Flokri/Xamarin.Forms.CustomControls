using Android.Content.Res;
using Android.Widget;
using Native = Android.App;
using Graphics = Android.Graphics;

namespace Xamarin.Forms.CustomControls.Services
{
    public static partial class CalculateBounds
    {
        /// <summary>
        /// get the native width of the string
        /// </summary>
        /// <param name="text">the string to measure</param>
        /// <param name="fontSize">the font size of the displayed string</param>
        /// <returns>the width of the measured string</returns>
        public static double TextWidthNative(string text, float fontSize)
        {
            Graphics.Rect bounds = new Graphics.Rect();
            TextView textView = new TextView(Native.Application.Context) { TextSize = fontSize };
            textView.Paint.GetTextBounds(text.ToCharArray(), 0, text.Length, bounds);
            var length = bounds.Width();
            return length / Resources.System.DisplayMetrics.ScaledDensity;
        }

        /// <summary>
        /// get the height of the the string
        /// </summary>
        /// <param name="text">the string to measure</param>
        /// <param name="fontSize">the font size of the displayed string</param>
        /// <returns>the height of the measured string</returns>
        public static double TextHeightNative(string text, float fontSize)
        {
            Graphics.Rect bounds = new Graphics.Rect();
            TextView textView = new TextView(Native.Application.Context) { TextSize = fontSize };
            textView.Paint.GetTextBounds(text.ToCharArray(), 0, text.Length, bounds);
            var height = bounds.Height();
            return height / Resources.System.DisplayMetrics.ScaledDensity;
        }
    }
}
