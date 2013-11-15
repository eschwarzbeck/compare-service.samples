using System.Net.Security;
using System.ServiceModel;

namespace StreamingService
{
	[MessageContract(WrapperNamespace = Namespaces.MessageContract, ProtectionLevel = ProtectionLevel.None)]
	public class FileUploadResponse
	{
		[MessageBodyMember(Order = 1)]
		public string SessionFileName { get; set; }
	}
}