using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using XamarinFilesTest.Models;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
		public IDataService DataService { get; set; }
		public IDialogService DialogService { get; set; }

		public MainViewModel(IDataService dataService, IDialogService dialogService)
        {
			DataService = dataService;
			DialogService = dialogService;
        }

		private MvxObservableCollection<File> listFiles;
		public MvxObservableCollection<File> ListFiles
		{
			get { return listFiles; }
			set { SetProperty(ref listFiles, value); }
		}

		private File selectedFile;
		public File SelectedFile
		{
			get { return selectedFile; }
			set { SetProperty(ref selectedFile, value); }
		}

        
        public override Task Initialize()
        {
            return base.Initialize();
        }
        
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