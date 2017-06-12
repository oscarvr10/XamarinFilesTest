using com.refractored.monodroidtoolkit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFilesTest.Views.CustomControls;
using XamarinFilesTest.Droid.Renderers;

[assembly:ExportRenderer(typeof(RadialProgressBar), typeof(CustomProgressRenderer))]
namespace XamarinFilesTest.Droid.Renderers
{
	//Custom Render que permite mostrar un Progress Bar Circular
	public class CustomProgressRenderer : ViewRenderer<RadialProgressBar, HoloCircularProgressBar>
	{

		public CustomProgressRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<RadialProgressBar> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || this.Element == null)
				return;


			var progress = new HoloCircularProgressBar(Forms.Context)
			{
				Max = Element.Max,
				Progress = Element.Progress,
				Indeterminate = Element.Indeterminate,
				ProgressColor = Element.ProgressColor.ToAndroid(),
				ProgressBackgroundColor = Element.ProgressBackgroundColor.ToAndroid(),
				IndeterminateInterval = Element.IndeterminateSpeed
			};

			SetNativeControl(progress);
		}


		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control == null || Element == null)
				return;

			if (e.PropertyName == RadialProgressBar.MaxProperty.PropertyName)
			{
				Control.Max = Element.Max;
			}
			else if (e.PropertyName == RadialProgressBar.ProgressProperty.PropertyName)
			{
				Control.Progress = Element.Progress;
			}
			else if (e.PropertyName == RadialProgressBar.IndeterminateProperty.PropertyName)
			{
				Control.Indeterminate = Element.Indeterminate;
			}
			else if (e.PropertyName == RadialProgressBar.ProgressBackgroundColorProperty.PropertyName)
			{
				Control.ProgressBackgroundColor = Element.ProgressBackgroundColor.ToAndroid();
			}
			else if (e.PropertyName == RadialProgressBar.ProgressColorProperty.PropertyName)
			{
				Control.ProgressColor = Element.ProgressColor.ToAndroid();
			}
			else if (e.PropertyName == RadialProgressBar.IndeterminateSpeedProperty.PropertyName)
			{
				Control.IndeterminateInterval = Element.IndeterminateSpeed;
			}

		}
	}
}
