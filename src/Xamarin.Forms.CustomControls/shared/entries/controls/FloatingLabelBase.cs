namespace Xamarin.Forms.CustomControls.Entries
{
    /// <summary>
    /// a content view base for views containing the floating label
    /// </summary>
    public partial class FloatingLabelBase : ContentView
    {
        #region bindable properties
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
            typeof(FloatingLabelBase), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(FloatingLabelBase), string.Empty);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FloatingLabelBase), Color.Gray);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(FloatingLabelBase), Color.Gray);

        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(FloatingLabelBase), Color.Gray);

        public static readonly BindableProperty AnimatedProperty =
            BindableProperty.Create(nameof(Animated), typeof(bool), typeof(FloatingLabelBase), true);

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard),
            typeof(Keyboard), typeof(FloatingLabelBase), Keyboard.Default);

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(FloatingLabelBase), false);

        public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor),
            typeof(Color), typeof(FloatingLabelBase), Color.FromRgb(73, 110, 234));

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType),
            typeof(ReturnType), typeof(FloatingLabelBase), ReturnType.Default);
        #endregion

        #region properties
        /// <summary>
        /// The content of the entry
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The placeholder text of the entry
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// The default color of the entry text color
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// The color of the title
        /// </summary>
        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        /// <summary>
        /// Specifies if the transformation is animated
        /// </summary>
        public bool Animated
        {
            get => (bool)GetValue(AnimatedProperty);
            set => SetValue(AnimatedProperty, value);
        }

        /// <summary>
        /// Specifies the keyboard for the entry
        /// </summary>
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        /// <summary>
        /// Specifies if the entry contains a password
        /// </summary>
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        /// <summary>
        /// The color of the placeholder
        /// </summary>
        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// The color of the entry cursor
        /// </summary>
        public Color CursorColor
        {
            get => (Color)GetValue(CursorColorProperty);
            set => SetValue(CursorColorProperty, value);
        }

        /// <summary>
        /// The return type flag for the entry keyboard
        /// </summary>
        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }
        #endregion
    }
}