using System.Threading.Tasks;
using Acr.UserDialogs;
using XamarinFilesTest.Services.Interfaces;

namespace XamarinFilesTest
{
	public class DialogService : IDialogService
	{
		public Task ShowAsync(string message, string title, string buttonLabel)
		{
			return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
	    }
	}
}
