using Prism.Commands;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CustomControls.Extensions;

namespace amarin.Forms.CustomControls.Buttons
{
    public class BuzzerButton : SKCanvasView
    {
        #region instances
        protected float _scaleFactor;
        private SKPaint _borderPaint;

        private byte _alpha = 255;
        #endregion

        #region bindables

        public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(
            nameof(ButtonText), typeof(string), typeof(BuzzerButton), defaultValue: default(string), propertyChanged: OnPropertyChangedInvalidate);

        public static readonly BindableProperty ButtonColorProperty = BindableProperty.Create(
            nameof(ButtonColor), typeof(Color), typeof(BuzzerButton), defaultValue: Color.Transparent, propertyChanged: OnPropertyChangedInvalidate);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize), typeof(float), typeof(BuzzerButton), defaultValue: default(float), propertyChanged: OnPropertyChangedInvalidate);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor), typeof(Color), typeof(BuzzerButton), defaultValue: Color.Black, propertyChanged: OnPropertyChangedInvalidate);

        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
            nameof(ButtonCommand), typeof(DelegateCommand), typeof(BuzzerButton), defaultValue: null);

        public DelegateCommand ButtonCommand
        {
            get => (DelegateCommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }
        #endregion

        #region constructor
        public BuzzerButton()
        {
            EnableTouchEvents = true;
            SetPaint();
        }
        #endregion

        #region eventhandler
        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is BuzzerButton instance)) return;
            if (oldValue != newValue) instance.InvalidateSurface();
        }
        #endregion

        #region override functions
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            var width = (float)Width;
            var scale = CanvasSize.Width / width;
            _scaleFactor = scale;

            var textSize = FontSize * scale;

            canvas.Clear();

            var buttonPaint = new SKPaint { Color = ButtonColor.ToSKColor().WithAlpha(_alpha), IsAntialias = true, Style = SKPaintStyle.Fill };

            var viewBounds = this.FromPixels(Bounds);

            var middlePoint = new SKPoint((float)viewBounds.Width / 2, (float)viewBounds.Height / 2);

            var borderRadius = (float)(viewBounds.Width / 2) - _borderPaint.StrokeWidth / 2;
            var buttonRadius = (float)(viewBounds.Width / 2) - _borderPaint.StrokeWidth / 2 - 8;

            _borderPaint.Color = ButtonColor.ToSKColor();

            canvas.DrawCircle(middlePoint, borderRadius, _borderPaint);
            canvas.DrawCircle(middlePoint, buttonRadius, buttonPaint);

            var textPaint = new SKPaint {TextSize = textSize, BlendMode = SKBlendMode.SrcOut};

            var textBounds = new SKRect();
            textPaint.MeasureText(ButtonText, ref textBounds);

            var xTextPos = (float)viewBounds.Width / 2 - textBounds.MidX;
            var yTextPos = (float)viewBounds.Height / 2 - textBounds.MidY;

            canvas.DrawText(ButtonText, xTextPos, yTextPos, textPaint);
        }

        protected override void OnTouch(SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed ||
                e.ActionType == SKTouchAction.Moved)
            {
                _alpha = 150;
                ButtonCommand?.Execute();
            }
            else _alpha = 255;

            e.Handled = true;

            InvalidateSurface();
        }

        #endregion

        #region private functions
        private void SetPaint()
        {
            _borderPaint = new SKPaint
            {
                StrokeWidth = 5,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };
        }
        #endregion

    }
}
