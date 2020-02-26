using CoreAnimation;
using CoreGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls.Frames;
using Xamarin.Forms.CustomControls.iOS.Frames;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomizableFrame), typeof(CustomizableFrameRenderer))]
namespace Xamarin.Forms.CustomControls.iOS.Frames
{
    public class CustomizableFrameRenderer : FrameRenderer
    {
        #region overrides
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            SetCornerRadius();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(CustomizableFrame.CornerRadius) ||
                e.PropertyName == nameof(CustomizableFrame))
            {
                SetCornerRadius();
            }
        }

        /// <summary>
        /// Draw the gradient background of the frame
        /// </summary>
        /// <param name="rect"></param>
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (Element != null)
            {
                if (Element is CustomizableFrame)
                {
                    var frame = Element as CustomizableFrame;

                    if (!frame.GradientBackground)
                    {
                        frame.BackgroundColor = frame.StartColor;
                        return;
                    }

                    var gradientLayer = new CAGradientLayer();
                    gradientLayer.Frame = rect;
                    gradientLayer.Colors = new CGColor[] {
                        frame.StartColor.ToCGColor(),
                        frame.EndColor.ToCGColor()
                    };

                    // horizontal gradient
                    if (frame.GradientOrientation == Orientation.Horizontal)
                    {
                        gradientLayer.StartPoint = new CGPoint(0.0, 0.5);
                        gradientLayer.EndPoint = new CGPoint(1.0, 0.5);
                    }
                    // vertical gradient
                    else if (frame.GradientOrientation == Orientation.Vertical)
                    {
                        gradientLayer.StartPoint = new CGPoint(0.5, 0.0);
                        gradientLayer.EndPoint = new CGPoint(0.5, 1.0);
                    }

                    NativeView.Layer.InsertSublayer(gradientLayer, 0);
                }
            }
        }
        #endregion

        #region privates
        private void SetCornerRadius()
        {
            var cornerRadius = (Element as CustomizableFrame)?.CornerRadius;
            if (!cornerRadius.HasValue)
                return;

            var roundedCornerRadius = RetrieveCommonCornerRadius(cornerRadius.Value);
            if (roundedCornerRadius <= 0)
                return;

            var roundedCorners = RetrieveRoundedCorners(cornerRadius.Value);

            var path = UIBezierPath.FromRoundedRect(Bounds, roundedCorners, new CGSize(roundedCornerRadius, roundedCornerRadius));
            var mask = new CAShapeLayer { Path = path.CGPath };
            NativeView.Layer.Mask = mask;
        }

        private double RetrieveCommonCornerRadius(CornerRadius cornerRadius)
        {
            var commonCornerRadius = cornerRadius.TopLeft;
            if (commonCornerRadius <= 0)
            {
                commonCornerRadius = cornerRadius.TopRight;
                if (commonCornerRadius <= 0)
                {
                    commonCornerRadius = cornerRadius.BottomLeft;
                    if (commonCornerRadius <= 0)
                    {
                        commonCornerRadius = cornerRadius.BottomRight;
                    }
                }
            }

            return commonCornerRadius;
        }

        private UIRectCorner RetrieveRoundedCorners(CornerRadius cornerRadius)
        {
            var roundedCorners = default(UIRectCorner);

            if (cornerRadius.TopLeft > 0)
            {
                roundedCorners |= UIRectCorner.TopLeft;
            }

            if (cornerRadius.TopRight > 0)
            {
                roundedCorners |= UIRectCorner.TopRight;
            }

            if (cornerRadius.BottomLeft > 0)
            {
                roundedCorners |= UIRectCorner.BottomLeft;
            }

            if (cornerRadius.BottomRight > 0)
            {
                roundedCorners |= UIRectCorner.BottomRight;
            }

            return roundedCorners;
        }
        #endregion
    }
}
