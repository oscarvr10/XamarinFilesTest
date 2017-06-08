using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinFilesTest.Models;
using XamarinFilesTest.Services.Interfaces;
using XamarinFilesTest.Utils;

namespace XamarinFilesTest.Services
{
	public class DataService 
	{
		private IApiService ApiService { get; set; }
		private IDialogService DialogService { get; set; }
		public static readonly int BufferSize = 8192;

		public DataService(IApiService apiService, IDialogService dialogService)
		{
			DialogService = dialogService;
			ApiService = apiService;
		}

		public async Task<List<File>> GetFiles()
		{
			List<File> result = null;

			string response = await ApiService.PostAsync(new Uri(Settings.URL_FILES, UriKind.Absolute));

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

		public static async Task<int> DownloadFileAsync(string fileUrl, IProgress<DownloadProgressUtil> progress)
		{
			int receivedBytes = 0;
			int totalBytes = 0;
			HttpClient client = new HttpClient();
			try
			{
				using (HttpResponseMessage response = client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead).Result)
				{
					response.EnsureSuccessStatusCode();
					using (var stream = await response.Content.ReadAsStreamAsync())
					{
						byte[] buffer = new byte[BufferSize];

						totalBytes = Int32.Parse(stream.Length.ToString());

						for (;;)
						{
							int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
							if (bytesRead == 0)
							{
								await Task.Yield();
								break;
							}

							receivedBytes += bytesRead;
							if (progress != null)
							{
								DownloadProgressUtil args = new DownloadProgressUtil(fileUrl, receivedBytes, totalBytes);
								progress.Report(args);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return receivedBytes;
		}
	}
}