using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CustomControls.Helpers
{
    public static class FontAwesomeStyles
    {
        /// <summary>
        /// Get the name of the brands style of the fontaweosome font. 
        /// </summary>
        /// <returns>FontAwesome brand string</returns>
        public static string FontAwesomeBrands()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return "FontAwesome5Brands-Regular";
                case Device.Android:
                    return "FontAwesome5Brands.otf#Regular";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Get the name of the solid style of the fontaweosome font. 
        /// </summary>
        /// <returns>FontAwesome solid string</returns>
        public static string FontAwesomeSolid()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return "FontAwesome5Free-Solid";
                case Device.Android:
                    return "FontAwesome5Solid.otf#Regular";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Get the name of the regular style of the fontaweosome font. 
        /// </summary>
        /// <returns>FontAwesome regular string</returns>
        public static string FontAwesomeRegular()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return "FontAwesome5Free-Regular";
                case Device.Android:
                    return "FontAwesome5Regular.otf#Regular";
                default:
                    return "";
            }
        }
    }
}
