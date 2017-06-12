using Android.OS;
using Android.App;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform;
using Android.Content.PM;
using Xamarin.Forms;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Droid.Presenters;
using Acr.UserDialogs;
using Xamarin.Forms.Platform.Android;

namespace XamarinFilesTest.Droid
{
[Activity(Label = "MvxFormsApplicationActivity",
	  	  Theme = "@style/MyTheme",
	      Icon = "@drawable/icon",
		  ScreenOrientation = ScreenOrientation.Portrait)]
	public class FormsApplicationActivity: FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			ToolbarResource = Resource.Layout.toolbar;

			Forms.Init(this, bundle);

			var app = new MvxFormsApplication();
			UserDialogs.Init(this);
			var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
			presenter.MvxFormsApp = app;

			LoadApplication(app);

			Mvx.Resolve<IMvxAppStart>().Start();
		}
	}
}
