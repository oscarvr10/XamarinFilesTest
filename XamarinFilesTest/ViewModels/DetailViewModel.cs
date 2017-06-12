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
using XamarinFilesTest.Views;

namespace XamarinFilesTest.ViewModels
{
	public class DetailViewModel : MvxViewModel<File>
	{
		public IDataService DataService { get; set; }
		public IUserDialogs DialogService { get; set; }

		private double percentDownload;
		public double PercentDownload
		{
			get { return percentDownload; }
			set { SetProperty(ref percentDownload, value); }
		}

		private File detailFile;
		public File DetailFile
		{
			get { return detailFile; }
			set { SetProperty(ref detailFile, value); }
		}

		CancellationTokenSource ctsToken = null;

		public DetailViewModel(IDataService dataService, IUserDialogs dialogService)
		{
			DataService = dataService;
			DialogService = dialogService;
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
					MessagingCenter.Send<DetailViewModel,double>(this, nameof(PercentDownload),PercentDownload);

					Debug.WriteLine($"Porcentaje de descarga: {PercentDownload}");
					if (args.IsFinished)
						DialogService.Alert($"Se ha finalizado la descarga de {DetailFile.documentName}", "Éxito", "Aceptar");
				};
				DetailFile.documentUrl = "https://drive.google.com/open?id=0B9aJA_iV4kHYM2dieHZhMHhyRVE";
				var urlArray = DetailFile.documentUrl.Split('=');
				var idFile = urlArray[1];
				ctsToken = new CancellationTokenSource();

				await DataService.DownloadFileAsync(idFile, progressReporter, ctsToken.Token);
			}
			catch (OperationCanceledException ex)
			{
				DialogService.Alert(ex.Message, "Aviso", "Aceptar");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			/*var archivo1 = "Arquitectura_SOA  https://drive.google.com/open?id=0B_l1vsi8qmsveGdCdkdINnM4LTQ";
			var archivo2 = "Database Management Systems 3rd Edition  https://drive.google.com/open?id=0B9aJA_iV4kHYM2dieHZhMHhyRVE";
			var archivo3 = "Patrones_de_diseño  https://drive.google.com/open?id=0B_l1vsi8qmsvYlM3cml4UllBZVE";*/
		}

		public override void Disappeared()
		{
			//ctsToken.Cancel();
			base.Disappeared();

		}

		public override void Disappearing()
		{
			//ctsToken.Cancel();
			base.Disappearing();
		}

	}
}
