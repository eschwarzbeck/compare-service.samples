using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Workshare.Document.Services.Compare.QInterface;

namespace LocalFilestoreCompareQEventHandler
{
	[ComVisible(false)]
	public class LocalFilestoreCompareQEvents : ICompareQEvents
	{
		public void BeginExecute(Dictionary<string, string> dicQueueParams, out byte[] originalDocumentBuffer, out byte[] modifiedDocumentBuffer, out string renderingset)
		{
			originalDocumentBuffer = File.ReadAllBytes(dicQueueParams["OriginalDocument"]);
			modifiedDocumentBuffer = File.ReadAllBytes(dicQueueParams["ModifiedDocument"]);

			string sRenderingSetPath = dicQueueParams["RenderingSet"];

			if (sRenderingSetPath.Length > 0)
			{
				renderingset = File.ReadAllText(sRenderingSetPath);
			}
			else
			{
				renderingset = "";
			}
		}

		public void EndExecute(Dictionary<string, string> dicQueueParams, string changeSummaryXml, byte[] redlineDocumentBuffer)
		{
			File.WriteAllText(dicQueueParams["RedlineXML"], changeSummaryXml);

			File.WriteAllBytes(dicQueueParams["RedlineDocument"], redlineDocumentBuffer);
		}
	}
}
