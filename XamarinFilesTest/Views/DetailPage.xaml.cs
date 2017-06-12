using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFilesTest.ViewModels;

namespace XamarinFilesTest.Views
{
	public partial class DetailPage : ContentPage
	{
		public DetailPage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Subscribe<DetailViewModel, double>(this, "PercentDownload", async (sender, arg) =>
			{
				await progressBar.ProgressTo(arg, 200, Easing.SinOut);
			});
		}
	}
}
