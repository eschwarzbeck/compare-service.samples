using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Workshare.Document.Services.Compare.QInterface
{
    [ComVisible(false)]
    [Guid("D7686A44-2313-4e65-9946-43CD7D74CC14")]
    public interface ICompareQEvents
    {
        void BeginExecute(Dictionary<string, string> dicQueueParams, out byte[] originalDocumentBuffer, out byte[] modifiedDocumentBuffer, out string renderingset);
        void EndExecute(Dictionary<string, string> dicQueueParams, string changeSummaryXml, byte[] redlineDocumentBuffer);
    }
}
