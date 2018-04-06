using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using SkiaSharp.Views.Android;

namespace ClockFace.Android
{
    public class ClockFaceView : SKCanvasView
    {
        private readonly ClockFace _clockFace;

        public ClockFaceView(Context context) : base(context)
        {
            _clockFace = new ClockFace();
            PaintSurface += OnPaintCanvas;
        }

        public ClockFaceView(Context context, IAttributeSet attributes) : base(context, attributes)
        {
            _clockFace = new ClockFace();
            PaintSurface += OnPaintCanvas;
        }

        public ClockFaceView(Context context, IAttributeSet attributes, int defStyleAtt) : base(context, attributes, defStyleAtt)
        {
            _clockFace = new ClockFace();
            PaintSurface += OnPaintCanvas;
        }

        public ClockFaceView(IntPtr ptr, JniHandleOwnership jni) : base(ptr, jni)
        {
            _clockFace = new ClockFace();
            PaintSurface += OnPaintCanvas;
        }

        
        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            _clockFace?.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }
    }
}
