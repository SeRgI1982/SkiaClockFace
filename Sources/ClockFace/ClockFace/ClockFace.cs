using System;
using System.Collections.Generic;
using SkiaSharp;

namespace ClockFace
{
    public class ClockFace
    {
        SKPaint backgroundFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Gray
        };

        SKPaint whiteStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Transparent,
            StrokeWidth = 5,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true
        };

        SKPaint whiteFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.White
        };

        SKPaint innerCircleFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#181919")
        };

        SKPaint outerCircleFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#1AA4A5A6")
        };

        SKPaint hourStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#181919"),
            StrokeWidth = 5
        };

        SKPaint minuteStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#4DA4A5A6"),
            StrokeWidth = 2
        };

        SKPaint blackStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 10,
            Color = SKColors.Transparent
        };

        SKPaint redStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            Color = SKColor.Parse("#FF6861")
        };

        SKPaint grayStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            Color = SKColor.Parse("#1AA4A5A6")
        };

        public void Draw(SKCanvas canvas, int width, int height)
        {
            canvas.Clear();

            DrawContent(canvas, width, height);
        }

        private void DrawContent(SKCanvas canvas, int width, int height)
        {
            //canvas.DrawPaint(backgroundFillPaint);

            // Set transforms
            canvas.Translate(width / 2, height / 2); // center is 0,0

            float explodeOffset = 75;
            float radius = Math.Min(width / 2, height / 2) - 2 * explodeOffset;
            //SKRect rect = new SKRect(-radius, -radius, radius, radius);
            //float startAngle = 150;
            //float sweepAngle = 240;

            //canvas.DrawOval(rect, whiteStrokePaint);

            //using (var path = new SKPath())
            //{
            //    path.AddArc(rect, startAngle, sweepAngle);
            //    canvas.DrawPath(path, blackStrokePaint);
            //}

            // Outer
            canvas.DrawCircle(0, 0, 100, outerCircleFillPaint);

            // Hour and minute marks
            var hours = new Dictionary<float, string>();
            hours.Add(240, "0");
            hours.Add(270, "1");
            hours.Add(300, "2");
            hours.Add(330, "3");
            hours.Add(0, "4");
            hours.Add(30, "5");
            hours.Add(60, "6");
            hours.Add(90, "7");
            hours.Add(120, "8");

            canvas.Save();

            for (float angle = 0; angle < 360; angle += 7.5f)
            {
                if (angle <= 120 || angle >= 240)
                {
                    //canvas.DrawCircle(0, -90, angle % 30 == 0 ? 4 : 2, redFillPaint);
                    var linePaint = angle % 30 == 0 ? hourStrokePaint : minuteStrokePaint;
                    var lineYStart = angle % 30 == 0 ? -radius - 20 : -radius - 40;
                    var lineYEnd = -radius - 70;

                    canvas.DrawLine(0, lineYStart, 0, lineYEnd, linePaint);

                    if (angle % 30 == 0)
                    {
                        using (SKPaint paint = new SKPaint())
                        {
                            //paint.Typeface = SKTypeface.FromFamilyName(null, SKTypefaceStyle.Bold);
                            paint.TextSize = 75;

                            canvas.DrawText(hours[angle], new SKPoint(-20, lineYStart + 100), paint);
                        }
                    }
                }

                canvas.RotateDegrees(7.5f);
            }

            canvas.Restore();

            // Tick

            Random rnd = new Random();
            var rotateAngle = 60 + rnd.Next(0, 240);
            canvas.RotateDegrees(rotateAngle);

            SKPath test1 = SKPath.ParseSvgPathData($"M -8 0 L -2 {radius + 32} L 2 {radius + 32} L 14 0 Z");
            SKPath test2 = SKPath.ParseSvgPathData($"M -8 0 L -2 {radius + 35} L 2 {radius + 35} L 8 0 Z");
            canvas.DrawPath(test1, grayStrokePaint);
            canvas.DrawPath(test2, redStrokePaint);
            //canvas.DrawLine(6, 0, 0, radius + 32, grayStrokePaint);
            //canvas.DrawLine(0, 0, 0, radius + 35, redStrokePaint);

            // Inner circle
            canvas.DrawCircle(0, 0, 50, innerCircleFillPaint);
        }
    }
}
