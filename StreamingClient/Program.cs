using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Principal;

using StreamingClient.Properties;
using StreamingClient.ServiceProxy;

namespace StreamingClient
{
	internal class Program
	{
		// REMEMBER IF YOU UPDATE THE PROXY YOU HAVE TO MANUAL CHANGE THE BYTE[] TO STREAM

		private static void Main(string[] args)
		{
			// Make sure we trust all certificates!  NOT TO BE USED IN PRODUCTION
			ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

			Console.WriteLine(@"Test Streaming Application");
			try
			{
				using (var context = new LoadingServiceClient())
				{
					//context.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
					context.ChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
					var user = context.Ping();
					if (string.IsNullOrEmpty(user) == false)
					{
						Console.WriteLine(@"Current Connect User is {0}", user);
						LoadFile(context, Resources.Original, "Original.doc");
						LoadFile(context, Resources.modifed, "Modified.doc");
						LoadFile(context, Resources.HS_hiddentext, "HS_hiddentext.doc");
						LoadFile(context, Resources._200000_paragraph, "LargeDoc1.docx");
						LoadFile(context, Resources._200000_paragraph_modified, "LargeDoc2.docx");

						LoadFile2(context, Resources.Original, "TestSession.doc");
					}
					else
					{
						Console.WriteLine(@"Ping FAILED to return with a valid result!");
					}
				}
				// Load new context
				LoadFileNewContext("LargeDoc1.docx");
				LoadFileNewContext("LargeDoc2.docx");
				LoadFileNewContext2("LargeDoc1.docx");
				LoadFileNewContext2("en_sql_server_2008_r2_enterprise_x86_x64_ia64_dvd_520517.iso");
				
			}
			catch (Exception ex)
			{
				Console.WriteLine(@"Problem with service - {0}", ex.Message);
				Console.WriteLine(@"Exception Type       - {0}", ex.GetBaseException().GetType().FullName);
			}
		}

		private static void LoadFile(ILoadingService context, byte[] data, string fileName)
		{
			using ( var stream = new MemoryStream(data))
			{
				var meta = new FileMetadata
				           	{
				           		FileName = fileName,
				           		FileSize = data.Length
				           	};
				var start = DateTime.Now;
				Console.WriteLine(@"Uploading {0} with a size of {1} bytes @ {2}", meta.FileName, meta.FileSize, start.ToString("HH:mm:ss:ffff", CultureInfo.CurrentCulture.DateTimeFormat));

				var request = new FileUploadRequest(meta, stream);

				context.UploadFile(request);
				var taken = DateTime.Now.Subtract(start);
				Console.WriteLine(@"Uploading {0} has completed in {1}ms", meta.FileName, taken.TotalMilliseconds);
			}
		}

		private static void LoadFile2(ILoadingService context, byte[] data, string fileName)
		{
			using (var stream = new MemoryStream(data))
			{
				var meta = new FileMetadata
				{
					FileName = fileName,
					FileSize = data.Length
				};
				var start = DateTime.Now;
				Console.WriteLine(@"Uploading {0} with a size of {1} bytes @ {2}", meta.FileName, meta.FileSize, start.ToString("HH:mm:ss:ffff", CultureInfo.CurrentCulture.DateTimeFormat));

				var request = new FileUploadRequest(meta, stream);

				var response = context.UploadFile2(request);
				var taken = DateTime.Now.Subtract(start);
				Console.WriteLine(@"Uploading {0} has completed in {1}ms with a reponse of {2}", meta.FileName, taken.TotalMilliseconds, response.SessionFileName);
			}
		}

		private static void LoadFileNewContext(string fileName)
		{
			string path = Path.Combine(@".\Resources", fileName);
			if (File.Exists(path) == false)
			{
				Console.WriteLine(@"Large Document {0} was not found!", path);
				return;
			}
			using (var context = new LoadingServiceClient())
			{
				context.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
				context.ChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
				using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					var meta = new FileMetadata
					           	{
					           		FileName = fileName,
					           		FileSize = Convert.ToInt32(stream.Length)
					           	};
					var start = DateTime.Now;
					Console.WriteLine(@"Uploading {0} with a size of {1} bytes @ {2}", meta.FileName, meta.FileSize, start.ToString("HH:mm:ss:ffff", CultureInfo.CurrentCulture.DateTimeFormat));

					//var request = new FileUploadRequest(meta, stream);

					context.UploadFile(meta, stream);
					var taken = DateTime.Now.Subtract(start);
					Console.WriteLine(@"Uploading {0} has completed in {1}ms", meta.FileName, taken.TotalMilliseconds);
				}
			}
		}

		private static void LoadFileNewContext2(string fileName)
		{
			string path = Path.Combine(@".\Resources", fileName);
			if (File.Exists(path) == false)
			{
				Console.WriteLine(@"Large Document {0} was not found!", path);
				return;
			}
			using (var context = new LoadingServiceClient())
			{
				context.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
				context.ChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
				using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					var meta = new FileMetadata
					{
						FileName = fileName,
						FileSize = stream.Length
					};
					var start = DateTime.Now;
					Console.WriteLine(@"Uploading {0} with a size of {1} bytes @ {2}", meta.FileName, meta.FileSize, start.ToString("HH:mm:ss:ffff", CultureInfo.CurrentCulture.DateTimeFormat));

					//var request = new FileUploadRequest(meta, stream);

					var response = context.UploadFile2(meta, stream);
					var taken = DateTime.Now.Subtract(start);
					Console.WriteLine(@"Uploading {0} has completed in {1}ms with a session name {2}", meta.FileName, taken.TotalMilliseconds, response);
				}
			}
		}
	}
}