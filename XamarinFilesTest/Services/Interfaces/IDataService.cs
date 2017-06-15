using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XamarinFilesTest.Models;
using XamarinFilesTest.Utils;

namespace XamarinFilesTest.Services.Interfaces
{
	public interface IDataService
	{
		Task<List<File>> GetFiles();
		Task<bool> DownloadFileAsync(string idFile, string filename, IProgress<DownloadProgressUtil> progress, CancellationToken token);
	}
}
