﻿using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Threading.Tasks;

using Xamarin.Forms.CustomControls.Animations;
using Xamarin.Forms.CustomControls.Extensions;
using Xamarin.Forms.CustomControls.Services;

namespace Xamarin.Forms.CustomControls.Entries
{
    public partial class AnimatedBorderEntry : FloatingLabelBase
    {
        #region instances
        private SKPath _borderPath;
        private DashedStroke _strokeDashStart;
        private DashedStroke _strokeDashEnd;
        private SKPaint _paint;

        private const uint ANIMATION_DURATION = 400;
        #endregion

        #region bindable properties
        public readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(AnimatedBorderEntry), Color.Default);
        public readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(AnimatedBorderEntry), Color.Default);
        public readonly BindableProperty GradientColorProperty = BindableProperty.Create(nameof(GradientColor), typeof(bool), typeof(AnimatedBorderEntry), false);
        #endregion

        public AnimatedBorderEntry()
        {
            InitializeComponent();

            borderlessEntry.BeforeTitleToPlaceholderAsync += async (sender, e) => await TitleToPlaceholderAsync();
            borderlessEntry.AfterPlaceholderToTitleAsync += async (sender, e) => await PlaceholderToTitleAsync();

            // create the paint for drawing
            _paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
            };

            // set the stroke width for the different platforms
#if __IOS__
            _paint.StrokeWidth = 3;
#endif
#if __ANDROID__
            _paint.StrokeWidth = 5;
#endif
        }

        #region privates
        /// <summary>
        /// Get called when drawing is required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkCanvasViewRequiredPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            // get and clear the canvas
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            // if the border path is null generate the path 
            _borderPath = _borderPath ?? GeneratePath(_paint);

            // set the dash effect to the generated border path and draw it on the canvas
            _paint.PathEffect = SKPathEffect.CreateDash(_strokeDashStart.Intervals, _strokeDashStart.Phase);
            canvas.DrawPath(_borderPath, _paint);
        }

        /// <summary>
        /// Draw the boarder
        /// </summary>
        /// <returns></returns>
        private async Task PlaceholderToTitleAsync()
        {
            _strokeDashStart = new DashedStroke(
                intervals: new float[] { 0, new SKPathMeasure(_borderPath).Length },
                phase: -0);

            _strokeDashEnd = new DashedStroke(
                intervals: new float[] { new SKPathMeasure(_borderPath).Length, new SKPathMeasure(_borderPath).Length },
                phase: -0);

            var anim = new DashedStrokeAnimation(
                from: _strokeDashStart,
                to: _strokeDashEnd,
                duration: ANIMATION_DURATION);

            await anim.Start((strokeDashToDraw) => SkCanvasView.InvalidateSurface());
        }


        /// <summary>
        /// Removes the border
        /// </summary>
        /// <returns></returns>
        private async Task TitleToPlaceholderAsync()
        {
            _strokeDashEnd = new DashedStroke(
                intervals: new float[] { 0, new SKPathMeasure(_borderPath).Length },
                phase: -0);

            _strokeDashStart = new DashedStroke(
                intervals: new float[] { new SKPathMeasure(_borderPath).Length, new SKPathMeasure(_borderPath).Length },
                phase: -0);

            var anim = new DashedStrokeAnimation(
                from: _strokeDashStart,
                to: _strokeDashEnd,
                duration: ANIMATION_DURATION);

            await anim.Start((strokeDashToDraw) => SkCanvasView.InvalidateSurface());
        }

        /// <summary>
        /// generate the path for the border
        /// </summary>
        /// <param name="paint">the paint object for the path</param>
        /// <returns>a skpath representing the border of the floating label entry</returns>
        private SKPath GeneratePath(SKPaint paint)
        {
            var path = new SKPath();

            Rectangle viewBounds = SkCanvasView.FromPixels(borderlessEntry.Bounds);

            var strokeWidth = (float)SkCanvasView.FromPixels(new Point(0, paint.StrokeWidth)).Y;
            float arcHeight = (float)viewBounds.Height + (float)strokeWidth;

            var textSize = SkCanvasView.FromPixels(new Size(
                width: CalculateBounds.GetTextWidth(borderlessEntry.Placeholder, borderlessEntry.TitleFontSize),
                height: CalculateBounds.GetTextHeight(borderlessEntry.Placeholder, borderlessEntry.TitleFontSize)));


            // move the point next to the placeholder label
            path.MoveTo(
                x: (float)viewBounds.X + (float)textSize.Width + 3,
                y: (float)viewBounds.Y);

            float xCurrent = path.LastPoint.X;
            float yCurrent = path.LastPoint.Y;

            // line above the entry
            path.LineTo((float)viewBounds.Width + (float)viewBounds.X, yCurrent);

            xCurrent = path.LastPoint.X;

            // draw the arc pointing down (right arc)
            path.ArcTo(
                oval: new SKRect(xCurrent - arcHeight / 2, yCurrent, xCurrent + arcHeight / 2, yCurrent + arcHeight),
                startAngle: -90,
                sweepAngle: 180,
                forceMoveTo: false);

            // draw the line below the entry
            path.LineTo((float)viewBounds.X, path.LastPoint.Y);

            xCurrent = path.LastPoint.X;
            yCurrent = path.LastPoint.Y;

            // draw the arc from below entry to above entry (left arc)
            path.ArcTo(
                oval: new SKRect(xCurrent - arcHeight / 2, yCurrent - arcHeight, xCurrent + arcHeight / 2, yCurrent),
                startAngle: 90,
                sweepAngle: 180,
                forceMoveTo: false);

            _borderPath = path;
            _strokeDashStart = new DashedStroke(
                intervals: new float[] { 0, new SKPathMeasure(_borderPath).Length },
                phase: -0);

            _strokeDashEnd = new DashedStroke(
                intervals: new float[] { new SKPathMeasure(_borderPath).Length, new SKPathMeasure(_borderPath).Length },
                phase: -0);

            CreateShader();

            return _borderPath;
        }

        /// <summary>
        /// Create the gradient color for the border path
        /// If only start or end color is set no gradient will be used.
        /// </summary>
        private void CreateShader()
        {
            if (!GradientColor)
            {
                _paint.Color = StartColor.ToSKColor();
            }
            else
            {
                _paint.Shader = SKShader.CreateLinearGradient(
                        start: new SKPoint(0, 0),
                        end: new SKPoint(SkCanvasView.CanvasSize.Width, SkCanvasView.CanvasSize.Height),
                        colors: new SKColor[] {
                            ((Color)StartColor).ToSKColor(),
                            ((Color)EndColor).ToSKColor()
                        },
                        colorPos: new float[] { 0, 1 },
                        mode: SKShaderTileMode.Clamp);
            }
        }
        #endregion

        #region properties
        /// <summary>
        /// the start color of the gradient border
        /// </summary>
        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        /// <summary>
        /// the end color of the gradient border
        /// </summary>
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        /// <summary>
        /// specifiy if the border should have a gradient color
        /// </summary>
        public bool GradientColor
        {
            get => (bool)GetValue(GradientColorProperty);
            set => SetValue(GradientColorProperty, value);
        }
        #endregion
    }
}