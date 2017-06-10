namespace XamarinFilesTest.Utils
{
	public class DownloadProgressUtil
	{
		public int BytesDownloaded { get; set; }

		public int Size { get; set; }

		public float PercentCompleted { get { return (float)BytesDownloaded / Size; } }

		public string Filename { get; private set; }

		public bool IsFinished { get { return BytesDownloaded == Size; } }

		public DownloadProgressUtil(string fileName, int bytesDownloaded, int size)
		{	
			BytesDownloaded = bytesDownloaded;
			Size = size;
			Filename = fileName;
		}
	}
}
