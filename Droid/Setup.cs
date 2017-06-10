using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Droid;
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
	}
}
