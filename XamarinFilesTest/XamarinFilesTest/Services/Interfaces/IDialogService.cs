using System.Threading.Tasks;

namespace XamarinFilesTest.Services.Interfaces
{
	public interface IDialogService
	{
		Task ShowAsync(string message, string title, string buttonLabel);
	}
}
