using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using XamarinFilesTest.Models;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest.ViewModels
{
	public class MainViewModel : MvxViewModel
	{
		public IDataService DataService { get; set; }
		public IUserDialogs DialogService { get; set; }

		public MainViewModel(IDataService dataService, IUserDialogs dialogService)
		{
			DialogService = dialogService;
			DataService = dataService;
		}

		private MvxObservableCollection<File> files;
		public MvxObservableCollection<File> Files
		{
			get { return files; }
			set { SetProperty(ref files, value); }
		}

		private File selectedFile;
		public File SelectedFile
		{
			get { return selectedFile; }
			set { SetProperty(ref selectedFile, value);}
		}
		private bool isLoading;
		public bool IsLoading
		{
			get { return isLoading; }
			set { SetProperty(ref isLoading, value); }
		}

		private bool hasResults = false;
		public bool HasResults
		{
			get { return hasResults; }
			set { SetProperty(ref hasResults, value); }
		}



		public override async void Start()
		{
			await GetFiles();
		}

		async Task GetFiles()
		{
			try
			{
				IsLoading = true;
				var result = await DataService.GetFiles();
				if (result != null && result.Count > 0)
				{
					Files = new MvxObservableCollection<File>(result);
					HasResults = true;	
				}

			}
			catch (System.Exception ex)
			{
				Debug.WriteLine(ex.Message);
				DialogService.Alert("Hubo un error al descargar la lista. Intèntalo nuevamente", "Error", "Ok");
			}
			finally 
			{
				IsLoading = false;
			}

		}

		public IMvxCommand GetFilesCommand => new MvxCommand(async () => await GetFiles());

		public IMvxCommand ShowDetailDownloadCommand => new MvxCommand(() =>
		{
			if (SelectedFile != null)
			{
				ShowViewModel<DetailViewModel>(SelectedFile);
				SelectedFile = null;
			}
		});
	}

}