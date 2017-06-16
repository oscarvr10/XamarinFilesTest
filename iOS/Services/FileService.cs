using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using QuickLook;
using UIKit;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest.iOS.Services
{
	public class FileService : IFileService
	{
		public FileService()
		{
		}

		public Task<bool> ExistsAsync(string filename)
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var filePath = Path.Combine(documentsPath, filename);

			return Task.Run(() => File.Exists(filePath));
		}


		public Task<string> GetPathFile(string filename)
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var filePath = Path.Combine(documentsPath, filename);
			Debug.WriteLine($"Ruta archivo a buscar: {filePath}");
			return Task.Run(() =>
			{
				return File.Exists(filePath) ? filePath : string.Empty;
			});
		}


		public Task<bool> SaveAsync(string filename, string extension, byte[] bytes)
		{
			try
			{
				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				var filePath = Path.Combine(documentsPath, string.Concat(filename, extension));
				Debug.WriteLine("Ruta nuevo pdf: " + filePath);
				Debug.WriteLine($"Tamaño nuevo pdf: {bytes.Length / 1024} KB");
				File.WriteAllBytes(filePath, bytes);
			}
			catch (Exception ex)
			{
				Debug.Write(ex.Message);
			}

			return ExistsAsync(filename);
		}


		public void OpenDocumentFile(string filepath, string mimeType)
		{
			var fileinfo = new FileInfo(filepath);
			var previewController = new QLPreviewController
			{
				DataSource = new PreviewControllerDataSource(fileinfo.FullName, fileinfo.Name)
			};

			var controller = FindNavigationController();

			controller?.PresentViewController(previewController, true, null);
		}


		UINavigationController FindNavigationController()
		{
			foreach (var window in UIApplication.SharedApplication.Windows)
			{
				if (window.RootViewController.NavigationController != null)
					return window.RootViewController.NavigationController;

				var value = CheckSubs(window.RootViewController.ChildViewControllers);
				if (value != null)
					return value;
			}

			return null;
		}


		UINavigationController CheckSubs(UIViewController[] controllers)
		{
			foreach (var controller in controllers)
			{
				if (controller.NavigationController != null)
					return controller.NavigationController;

				var value = CheckSubs(controller.ChildViewControllers);

				return value;
			}

			return null;
		}
	}



	public class DocumentItem : QLPreviewItem
	{
		readonly string _uri;

		public DocumentItem(string title, string uri)
		{
			ItemTitle = title;
			_uri = uri;
		}

		public override string ItemTitle { get; }

		public override NSUrl ItemUrl => NSUrl.FromFilename(_uri);
	}

	public class PreviewControllerDataSource : QLPreviewControllerDataSource
	{
		readonly string _url;
		readonly string _filename;

		public PreviewControllerDataSource(string url, string filename)
		{
			_url = url;
			_filename = filename;
		}

		public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
		{
			return new DocumentItem(_filename, _url);
		}

		public override nint PreviewItemCount(QLPreviewController controller)
		{
			return 1;
		}
	}
}
