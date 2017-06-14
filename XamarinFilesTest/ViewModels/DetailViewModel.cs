using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using XamarinFilesTest.Models;
using XamarinFilesTest.Services.Interfaces;
using XamarinFilesTest.Utils;

namespace XamarinFilesTest.ViewModels
{
	public class DetailViewModel : MvxViewModel<File>
	{
		public IDataService DataService { get; set; }
		public IUserDialogs DialogService { get; set; }
		public IFileService FileService { get; set; }
		CancellationTokenSource ctsToken = null;

		private double percentDownload;
		public double PercentDownload
		{
			get { return percentDownload; }
			set { SetProperty(ref percentDownload, value); }
		}

		private bool fileCreated;
		public bool FileCreated
		{
			get { return fileCreated; }
			set { SetProperty(ref fileCreated, value); }
		}

		private File detailFile;
		public File DetailFile
		{
			get { return detailFile; }
			set { SetProperty(ref detailFile, value); }
		}

		public IMvxCommand ShowFileDownloadedCommand => new MvxCommand(async () => await ShowFile());


		public DetailViewModel(IDataService dataService, IUserDialogs dialogService, IFileService fileService)
		{
			DataService = dataService;
			DialogService = dialogService;
			FileService = fileService;
		}


		public override Task Initialize(File selectedFile)
		{
			DetailFile = selectedFile;
			DownloadFile();

			return base.Initialize();
		}


		async void DownloadFile()
		{
			try
			{
				Progress<DownloadProgressUtil> progressReporter = new Progress<DownloadProgressUtil>();

				//Suscripción al evento ProgressChanged para actualizar el porcentaje de descargar en el progress bar
				progressReporter.ProgressChanged += (s, args) =>
				{
					PercentDownload = args.PercentCompleted;
					MessagingCenter.Send<DetailViewModel, double>(this, nameof(PercentDownload), PercentDownload);

					//Debug.WriteLine($"Porcentaje de descarga: {PercentDownload * 100 }%");
					if (args.IsFinished)
						DialogService.Alert($"Se ha finalizado la descarga de {DetailFile.documentName}", "Éxito", "Aceptar");
				};

				var urlArray = DetailFile.documentUrl.Split('=');
				var idFile = urlArray[1];
				ctsToken = new CancellationTokenSource();
				Application.Current.Properties["documentName"] = DetailFile.documentName;
				await DataService.DownloadFileAsync(idFile, DetailFile.documentName, progressReporter, ctsToken.Token);


			}
			catch (OperationCanceledException ex)
			{
				DialogService.Alert(ex.Message, "Aviso", "Aceptar");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		async Task ShowFile()
		{
			string filename = string.Concat(Application.Current.Properties["documentName"], ".pdf");
			string filepath = await FileService.GetPathFile(filename);
			Debug.WriteLine("Ruta archivo: " + filepath);
			if (!string.IsNullOrEmpty(filepath))
				FileService.OpenDocumentFile(filepath, "application/pdf");
		}

	}
}
