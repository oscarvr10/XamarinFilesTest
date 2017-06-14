using System.Threading.Tasks;

namespace XamarinFilesTest.Services.Interfaces
{
	public interface IFileService
	{
		Task<bool> ExistsAsync(string filename);

		Task<string> GetPathFile( string filename);

		Task <bool>SaveAsync(string filename, string extension, byte[] bytes);

		void OpenDocumentFile(string filepath, string mimeType);
	}
}
