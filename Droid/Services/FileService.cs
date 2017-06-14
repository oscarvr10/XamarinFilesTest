using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest.Droid.Services
{
	public class FileService : IFileService
	{
		public FileService()
		{
		}

		public Task<bool> ExistsAsync(string filename)
		{
			var documentsPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
			var filePath = Path.Combine(documentsPath, "GoogleDriveFiles" , filename);

			return Task.Run(() => File.Exists(filePath));
		}


		public Task<string> GetPathFile(string filename)
		{
			var documentsPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
			var filePath = Path.Combine(documentsPath, "GoogleDriveFiles", filename);
			return Task.Run(() =>
			{
				return File.Exists(filePath) ? filePath : string.Empty;
			});
		}


		public void OpenDocumentFile(string filepath, string mimeType)
		{
			var uri = Android.Net.Uri.Parse("file://" + filepath);
			var intent = new Intent(Intent.ActionView);
			intent.SetDataAndType(uri, mimeType);
			intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
			Forms.Context.StartActivity(Intent.CreateChooser(intent, "Selecciona una Aplicación"));
		}


		public Task<bool> SaveAsync(string filename, string extension, byte[] bytes)
		{
			if (!Directory.Exists(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "GoogleDriveFiles")))
				Directory.CreateDirectory(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "GoogleDriveFiles"));

			var path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "GoogleDriveFiles", string.Concat(filename, extension));
			Debug.WriteLine("Ruta nuevo pdf: " + path);
			File.WriteAllBytes(path, bytes);

			return ExistsAsync(filename);
		}
	}
}
