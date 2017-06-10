using System;
using Android.OS;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Forms.Presenter.Droid;
using MvvmCross.Platform;
using Xamarin.Forms.Platform.Android;

namespace XamarinFilesTest.Droid
{
	public class MvxFormsApplicationActivity: FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			var mvxFormsApp = new MvxFormsApp();
			LoadApplication(mvxFormsApp);

			var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
			presenter.MvxFormsApp = mvxFormsApp;

			Mvx.Resolve<IMvxAppStart>().Start();
		}
	}
}
