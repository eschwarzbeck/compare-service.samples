using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.ServiceModel;

namespace StreamingService
{
	public class LoadingService 
		: ILoadingService
	{
		#region Implementation of ILoadingService

		public void UploadFile(FileUploadRequest request)
		{
			Debug.WriteLine("Upload Request Received", "STREAMSERVICE");
			FileStream targetStream;
			var sourceStream = request.FileByteStream;

			const string uploadFolder = @"C:\temp\";
			var filename = request.Metadata.FileName;
			var filePath = Path.Combine(uploadFolder, filename);
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture,
			                              "Saving File to {0}",
			                              filePath),
			                "STREAMSERVICE");

			var size = 0;
			using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				//read from the input stream in 4K chunks
				//and save to output stream
				const int bufferLen = 4096;
				var buffer = new byte[bufferLen];
				int count;
				while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
				{
					size += count;
					targetStream.Write(buffer, 0, count);
					Debug.WriteLine(string.Format(CultureInfo.CurrentCulture,
												  "Uploaded {0} bytes to service",
												  size),
									"STREAMSERVICE");
				}
				targetStream.Close();
				sourceStream.Close();
			}
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture,
			                              "Saved {0} bytes to file",
			                              size),
			                "STREAMSERVICE");
		}

		public FileUploadResponse UploadFile2(FileUploadRequest request)
		{
			Debug.WriteLine("Upload2 Request Received", "STREAMSERVICE");
			string serverName = string.Format(CultureInfo.CurrentCulture,
			                                  "{0}-{1}.{2}",
			                                  Path.GetFileNameWithoutExtension(request.Metadata.FileName),
			                                  Guid.NewGuid(),
			                                  Path.GetExtension(request.Metadata.FileName));
			request.Metadata.FileName = serverName;
			UploadFile(request);
			var response = new FileUploadResponse
			               	{
			               		SessionFileName = serverName
			               	};
			return response;
		}

		public string Ping()
		{
			Debug.WriteLine("Ping Request Received", "STREAMSERVICE");
			if (OperationContext.Current.ServiceSecurityContext != null)
			{
				// return who we think we are
				if (OperationContext.Current.ServiceSecurityContext.IsAnonymous)
				{
					Debug.WriteLine("IsAnonymous User", "STREAMSERVICE");
					return "Unknown User";
				}
				Debug.WriteLine(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name, "STREAMSERVICE");
				return OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
			}
			else
			{
				Debug.WriteLine("No Security Context Found", "STREAMSERVICE");
				return "No Security Context Found";
			}
		}

		#endregion
	}
}