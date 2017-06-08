using System;
using MvvmCross.Core.ViewModels;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest.ViewModels
{
	public class DetailViewModel: MvxViewModel
	{
		public IDataService DataService { get; set; }
		public IDialogService DialogService { get; set; }
		public DetailViewModel(IDataService dataService, IDialogService dialogService)
		{
			DataService = dataService;
			DialogService = dialogService;
		}
	}
}
