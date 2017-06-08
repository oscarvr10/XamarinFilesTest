using System;
using System.Threading.Tasks;

namespace XamarinFilesTest.Services.Interfaces
{
	public interface IApiService
	{
		string Serialize<T>(T objResponse);

		T Deserialize<T>(string json);

		Task<string> PostAsync(Uri uri);
	}
}
