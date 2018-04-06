using System;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ClockFace.Forms
{
    public class ClockFaceView : SKCanvasView
    {
        private readonly ClockFace _clockFace;

        public ClockFaceView()
        {
            _clockFace = new ClockFace();
            BackgroundColor = Color.Transparent;
            PaintSurface += OnPaintCanvas;
        }

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            _clockFace.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }
    }
}
