using System;
using System.Globalization;
using SkiaSharp;

namespace ClockFace
{
    public class ClockFace
    {
        private const int MinutesInHour = 60;
        private const float ExplodeOffset = 75;

        private readonly SKPaint _innerCircleFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#181919")
        };

        private readonly SKPaint _outerCircleFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#1AA4A5A6")
        };

        private readonly SKPaint _hourStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#181919"),
            IsAntialias = true,
            StrokeWidth = 7
        };

        private readonly SKPaint _minuteStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#4DA4A5A6"),
            IsAntialias = true,
            StrokeWidth = 4
        };

        private readonly SKPaint _redStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            Color = SKColor.Parse("#FF6861")
        };

        private readonly SKPaint _grayStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            Color = SKColor.Parse("#1AA4A5A6")
        };

        public float StartAngle { get; set; } = 240;

        public float SweepAngle { get; set; } = 240;

        public int HourIndicators { get; set; } = 8;

        public float HourTextSize { get; set; } = 75;

        public int MinutesIndicators { get; set; } = 15;

        public double Duration { get; set; }

        public void Draw(SKCanvas canvas, int width, int height)
        {
            canvas.Clear();

            DrawContent(canvas, width, height);
        }

        private void DrawContent(SKCanvas canvas, int width, int height)
        {
            // Set transforms
            canvas.Translate(width / 2, height / 2); // center is 0,0
            
            // Outer
            canvas.DrawCircle(0, 0, 100, _outerCircleFillPaint);

            // Hour and minute 
            float hourShiftAngle = SweepAngle / HourIndicators;
            float minutesShiftAngle = (hourShiftAngle / MinutesInHour) * MinutesIndicators;

            if (Math.Abs(minutesShiftAngle) < 0.01)
            {
                throw new Exception("Wrong value of minutes");
            }

            canvas.RotateDegrees(StartAngle);

            canvas.Save();

            int hoursCounter = 0;
            float radius = Math.Min(width / 2, height / 2) - 2 * ExplodeOffset;

            for (float angle = 0; angle < 360; angle += minutesShiftAngle)
            {
                if (angle <= HourIndicators * hourShiftAngle)
                {
                    var linePaint = angle % hourShiftAngle == 0 ? _hourStrokePaint : _minuteStrokePaint;
                    var lineYStart = angle % hourShiftAngle == 0 ? -radius - 20 : -radius - 40;
                    var lineYEnd = -radius - 70;

                    canvas.DrawLine(0, lineYStart, 0, lineYEnd, linePaint);

                    if (angle % hourShiftAngle == 0)
                    {
                        using (var paint = new SKPaint())
                        {
                            //paint.Typeface = SKTypeface.FromFamilyName(null, SKTypefaceStyle.Bold);
                            paint.TextSize = HourTextSize;

                            canvas.DrawText(hoursCounter.ToString(CultureInfo.InvariantCulture), new SKPoint(-20, lineYStart + 100), paint);
                            hoursCounter += 1;
                        }
                    }
                }
                else
                {
                    break;
                }

                canvas.RotateDegrees(minutesShiftAngle);
            }

            canvas.Restore();

            // Duration indicator
            var rotateAngle = Convert.ToSingle(Duration * SweepAngle / (HourIndicators * MinutesInHour));
            canvas.RotateDegrees(rotateAngle);

            var durationIndicatorShadowPath = SKPath.ParseSvgPathData($"M -8 0 L -2 {-radius - 30} L 2 {-radius - 30} L 14 0 Z");
            var durationIndicatorShadow = SKPath.ParseSvgPathData($"M -8 0 L -2 {-radius - 30} L 2 {-radius - 30} L 8 0 Z");
            canvas.DrawPath(durationIndicatorShadowPath, _grayStrokePaint);
            canvas.DrawPath(durationIndicatorShadow, _redStrokePaint);
            
            // Inner circle
            canvas.DrawCircle(0, 0, 50, _innerCircleFillPaint);
        }
    }
}
