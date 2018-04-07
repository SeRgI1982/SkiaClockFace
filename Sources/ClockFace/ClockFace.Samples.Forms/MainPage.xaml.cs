using System;
using Xamarin.Forms;

namespace ClockFace.Samples.Forms
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
	    {
	        var newStep = Math.Round(e.NewValue / 1d);
	        var slider = (Slider) sender;
	        slider.Value = newStep * 1d;
	    }
    }
}
