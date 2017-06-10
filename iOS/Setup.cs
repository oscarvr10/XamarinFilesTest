using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Core;
using MvvmCross.Forms.iOS.Presenters;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform.Platform;
using UIKit;
using Xamarin.Forms;

namespace XamarinFilesTest.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }
        
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
			return new XamarinFilesTest.App();
        }
        
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

		protected override IMvxIosViewPresenter CreatePresenter()
		{
			Forms.Init();

			var xamarinFormsApp = new MvxFormsApplication();

			return new MvxFormsIosPagePresenter(Window, xamarinFormsApp);
		}
    }
}
