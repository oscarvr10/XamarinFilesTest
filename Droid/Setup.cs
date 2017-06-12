using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Droid;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace XamarinFilesTest.Droid
{
	public class Setup : MvxFormsAndroidSetup
	{
		public Setup(Context applicationContext)
			: base(applicationContext)
		{
		}


		protected override IMvxApplication CreateApp()
		{
			return new App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}

		protected override IMvxAndroidViewPresenter CreateViewPresenter()
		{
			var presenter = new MvxFormsDroidPagePresenter();
			Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

			return presenter;
		}
	}
}
