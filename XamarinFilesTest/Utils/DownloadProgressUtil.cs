namespace XamarinFilesTest.Utils
{
	public class DownloadProgressUtil
	{
		public int BytesDownloaded { get; set; }

		public int Total { get; set; }

		public double PercentCompleted { get { return (double)BytesDownloaded / Total; } }

		public bool IsFinished { get { return BytesDownloaded == Total; } }

		public DownloadProgressUtil(int bytesDownloaded, int total)
		{	
			BytesDownloaded = bytesDownloaded;
			Total = total;
		}
	}
}
