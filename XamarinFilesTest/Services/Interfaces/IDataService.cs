using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinFilesTest.Models;
using XamarinFilesTest.Utils;

namespace XamarinFilesTest.Services.Interfaces
{
	public interface IDataService
	{
		Task<List<File>> GetFiles();
		Task<int> DownloadFileAsync(string fileUrl, IProgress<DownloadProgressUtil> progress);
	}
}
