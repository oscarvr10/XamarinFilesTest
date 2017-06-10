using System;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest.ViewModels
{
	public class DetailViewModel: MvxViewModel
	{
		public IDataService DataService { get; set; }
		public IUserDialogs DialogService { get; set; }
		public DetailViewModel(IDataService dataService, IUserDialogs dialogService)
		{
			DataService = dataService;
			DialogService = dialogService;
		}

		public override System.Threading.Tasks.Task Initialize()
		{
			return base.Initialize();
		}
	}
}
