using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest.Services
{
	public class ApiService : IApiService
	{
		const string mediaType = "application/json";

		public ApiService() { }

		public T Deserialize<T>(string json)
		{
			return !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<T>(json) : default(T);
		}

		public async Task<string> GetAsync(Uri uri)
		{
			using (var http = new HttpClient()) 
			{ 
				try
                {
					//var content = new StringContent(null, Encoding.UTF8, mediaType);
					var response = await http.GetAsync(uri);

                    if (response != null)
                    {
						if (response.StatusCode == System.Net.HttpStatusCode.OK)
							return await response.Content.ReadAsStringAsync();
                    }
                    else
                        return null;
                }
                catch (Exception exception)
                {
					
					Debug.WriteLine(exception.Message);
                }
			}

			return null;
		}


		public string Serialize<T>(T objResponse)
		{
			 return JsonConvert.SerializeObject(objResponse);
		}
	}
}
