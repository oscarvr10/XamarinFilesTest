using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
		public static readonly int BufferSize = 4096;

		public DataService(IApiService apiService)
		{
			ApiService = apiService;
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

		public async Task DownloadFileAsync(string idFile, IProgress<DownloadProgressUtil> progress, CancellationToken token)
		{
			int receivedBytes = 0;
			int totalBytes = 0;
			HttpClient client = new HttpClient();
			try
			{
				var url = $"{Settings.API_GOOGLE_DRIVE}{idFile}?key={Settings.API_KEY}&alt=media";
				using (HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result)
				{
					response.EnsureSuccessStatusCode();
					totalBytes = response.Content.Headers.ContentLength.HasValue? (int)response.Content.Headers.ContentLength.Value : 0;

					Debug.WriteLine($"Total de KB a descargar: {(totalBytes/1024)}");


					bool canReport = totalBytes != 0 && progress != null;

					using (var stream = await response.Content.ReadAsStreamAsync())
					{
						byte[] buffer = new byte[BufferSize];
						for (;;)
						{
							int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
							if (bytesRead == 0 || token.IsCancellationRequested)
							{
								await Task.Yield();
								break;
							}
							else
							{
								//var newData = new byte[bytesRead];
								//buffer.ToList().CopyTo(0, newData, 0, bytesRead);
								receivedBytes += bytesRead;
								if (canReport)
								{
									DownloadProgressUtil args = new DownloadProgressUtil(receivedBytes, totalBytes);
									progress.Report(args);
								}
							}
						}
					}
				}
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
		}
	}
}