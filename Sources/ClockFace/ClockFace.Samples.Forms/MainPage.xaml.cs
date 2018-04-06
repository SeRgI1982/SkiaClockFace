using System;

namespace ClockFace.Samples.Forms
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private void OnInvalidateClicked(object sender, EventArgs e)
	    {
	        CanvasView.InvalidateSurface();
	    }
	}
}
