using System.IO;
using System.Net.Security;
using System.ServiceModel;

namespace StreamingService
{
	[MessageContract(WrapperNamespace = Namespaces.MessageContract, ProtectionLevel = ProtectionLevel.None)]
	public class FileUploadRequest
	{
		public FileUploadRequest()
		{
			Metadata = new FileMetadata();
		}

		[MessageHeader(MustUnderstand = true)]
		public FileMetadata Metadata { get; set; }

		[MessageBodyMember(Order = 1)]
		public Stream FileByteStream { get; set; }
	}
}