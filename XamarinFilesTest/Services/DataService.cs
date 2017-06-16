using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XamarinFilesTest.Models;
using XamarinFilesTest.Services.Interfaces;
using XamarinFilesTest.Utils;

namespace XamarinFilesTest.Services
{
	public class DataService: IDataService 
	{
		private IApiService ApiService { get; set; }
		private IFileService FileService { get; set; }
		public static readonly int BufferSize = 4096;

		public DataService(IApiService apiService, IFileService fileService)
		{
			ApiService = apiService;
			FileService = fileService;

		}

		public async Task<List<File>> GetFiles()
		{
			List<File> result = null;

			string response = await ApiService.GetAsync(new Uri(Settings.URL_FILES));

			if (!string.IsNullOrEmpty(response))
			{
				try
				{
					result = ApiService.Deserialize<List<File>>(response);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			}

			return result;
		}

		public async Task<bool> DownloadFileAsync(string idFile, string filename, IProgress<DownloadProgressUtil> progress, CancellationToken token)
		{
			int receivedBytes = 0;
			int totalBytes = 0;
			string result = string.Empty;
			HttpClient client = new HttpClient();
			try
			{
				var url = $"{Settings.API_GOOGLE_DRIVE}{idFile}?key={Settings.API_KEY}&alt=media";
				using (HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token).Result)
				{
					response.EnsureSuccessStatusCode();
					totalBytes = response.Content.Headers.ContentLength.HasValue ? (int)response.Content.Headers.ContentLength.Value : 0;


					Debug.WriteLine($"Total a descargar: {totalBytes / 1024} KB");

					bool canReport = totalBytes != 0 && progress != null;

					using (var stream = await response.Content.ReadAsStreamAsync())
					{
						using (var ms = new MemoryStream())
						{
							byte[] buffer = new byte[BufferSize];
							for (;;)
							{
								if (token.IsCancellationRequested)
									token.ThrowIfCancellationRequested();
								int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);

								if (bytesRead == 0)
								{
									await Task.Yield();
									break;
								}

								receivedBytes += bytesRead;
								ms.Write(buffer, 0, bytesRead);

								if (canReport)
								{
									DownloadProgressUtil args = new DownloadProgressUtil(receivedBytes, totalBytes);
									progress.Report(args);
								}
							}

							Debug.WriteLine($"Longitud buffer: {ms.Length / 1024} KB");
							//Se crea archivo a partir del Stream descargado
							await FileService.SaveAsync(filename, ".pdf", ms.ToArray());
						}
					}
				}
				return true;
			}
			catch (OperationCanceledException ex)
			{
				Debug.WriteLine(ex.Message);
				throw new OperationCanceledException(ex.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return false;
		}
	}
}