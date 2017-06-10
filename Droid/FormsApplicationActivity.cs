using Android.OS;
using Android.App;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform;
using Android.Content.PM;
using Xamarin.Forms;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Forms.Droid;
using Acr.UserDialogs;

namespace XamarinFilesTest.Droid
{
[Activity(Label = "MvxFormsApplicationActivity",
	  	  Theme = "@style/MyTheme",
		  ScreenOrientation = ScreenOrientation.Portrait)]
	public class FormsApplicationActivity: MvxFormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

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
