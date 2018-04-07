using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ClockFace.Forms
{
    public class ClockFaceView : SKCanvasView
    {
        private readonly ClockFace _clockFace;

        public static readonly BindableProperty StartAngleProperty = BindableProperty.Create(
            nameof(StartAngle), typeof(float), typeof(ClockFaceView), 240f, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty SweepAngleProperty = BindableProperty.Create(
            nameof(SweepAngle), typeof(float), typeof(ClockFaceView), 240f, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty HourIndicatorsProperty = BindableProperty.Create(
            nameof(HourIndicators), typeof(int), typeof(ClockFaceView), 8, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty HourTextSizeProperty = BindableProperty.Create(
            nameof(HourTextSize), typeof(float), typeof(ClockFaceView), 72f, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty MinutesIndicatorsProperty = BindableProperty.Create(
            nameof(MinutesIndicators), typeof(int), typeof(ClockFaceView), 15, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(
            nameof(Duration), typeof(double), typeof(ClockFaceView), 0d, propertyChanged: OnPropertyChanged);

        public ClockFaceView()
        {
            _clockFace = new ClockFace();
            BackgroundColor = Color.Transparent;
            PaintSurface += OnPaintCanvas;
        }

        public float StartAngle
        {
            get => (float) GetValue(StartAngleProperty);
            set => SetValue(StartAngleProperty, value);
        }

        public float SweepAngle
        {
            get => (float) GetValue(SweepAngleProperty);
            set => SetValue(SweepAngleProperty, value);
        }

        public int HourIndicators
        {
            get => (int) GetValue(HourIndicatorsProperty);
            set => SetValue(HourIndicatorsProperty, value);
        }

        public float HourTextSize
        {
            get => (float) GetValue(HourTextSizeProperty);
            set => SetValue(HourTextSizeProperty, value);
        }

        public int MinutesIndicators
        {
            get => (int) GetValue(MinutesIndicatorsProperty);
            set => SetValue(MinutesIndicatorsProperty, value);
        }

        public double Duration
        {
            get => (double) GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            _clockFace.StartAngle = StartAngle;
            _clockFace.SweepAngle = SweepAngle;
            _clockFace.HourIndicators = HourIndicators;
            _clockFace.HourTextSize = HourTextSize;
            _clockFace.MinutesIndicators = MinutesIndicators;
            _clockFace.Duration = Duration;
            _clockFace.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var self = (ClockFaceView)bindable;
            self.InvalidateSurface();
        }
    }
}
