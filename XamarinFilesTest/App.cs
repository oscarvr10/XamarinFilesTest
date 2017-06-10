using System.Reflection;
using Acr.UserDialogs;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using XamarinFilesTest.Services;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
			CreatableTypes(typeof(ApiService).GetTypeInfo().Assembly)
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            RegisterAppStart<ViewModels.MainViewModel>();
        }
    }
}
