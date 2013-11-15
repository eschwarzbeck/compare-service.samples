using System.Net.Security;
using System.ServiceModel;

namespace StreamingService
{
	[ServiceContract( Namespace = Namespaces.Contract,  ProtectionLevel = ProtectionLevel.None)]
	public interface ILoadingService
	{
		[OperationContract(Action = "UploadFile", IsOneWay = true)]
		void UploadFile(FileUploadRequest request);

		[OperationContract(Action = "Ping")]
		string Ping();

		[OperationContract(Action = "UploadFile2")]
		FileUploadResponse UploadFile2(FileUploadRequest request);
	}
}