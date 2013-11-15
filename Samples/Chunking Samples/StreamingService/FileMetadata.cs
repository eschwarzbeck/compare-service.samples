using System.Runtime.Serialization;

namespace StreamingService
{
	[DataContract(Namespace = Namespaces.DataContract)]
	public class FileMetadata
	{
		[DataMember(IsRequired = true)]
		public string FileName { get; set; }

		[DataMember(IsRequired = true)]
		public long FileSize { get; set; }
	}
}