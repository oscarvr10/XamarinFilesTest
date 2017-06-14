using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Droid;
using MvvmCross.Forms.Droid.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.Plugins;
using XamarinFilesTest.Droid.Services;
using XamarinFilesTest.Services.Interfaces;

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

		public override void LoadPlugins(IMvxPluginManager pluginManager)
		{
			base.LoadPlugins(pluginManager);
			Mvx.RegisterType<IFileService, FileService>();
		}
	}
}
