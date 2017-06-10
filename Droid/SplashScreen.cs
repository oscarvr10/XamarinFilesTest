using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;
using Xamarin.Forms;

namespace XamarinFilesTest.Droid
{
	[Activity(
		Label = "XamarinFilesTest.Droid"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenActivity
	{
		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}

		private bool isInitializationComplete = false;
		public override void InitializationComplete()
		{
			if (!isInitializationComplete)
			{
				isInitializationComplete = true;
				StartActivity(typeof(FormsApplicationActivity));
			}
		}

		protected override void OnCreate(Android.OS.Bundle bundle)
		{
			Forms.Init(this, bundle);
			base.OnCreate(bundle);;
		}
	}
}
