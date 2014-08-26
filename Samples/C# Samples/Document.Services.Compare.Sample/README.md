Document.Services.Compare.Sample
=================================

This example demonstrates how to write a C# WinForm client in order to compare documents using the Workshare Compare service. The sample utilizes the CompareWebServiceWCF interface (as denoted by the /Compare5 host URL) and demonstrates the full feature set of the service.

Requires Visual Studio 2012, .NET 4 Framework


Running the Document.Services.Compare.Sample Application
--------------------------------------------------------

1.	Enter the host in the Host field:
	+ **IIS Hosting**: http://localhost:8080/comparewebservice.svc/Compare5 
	+ **Windows Service**: http://localhost:8080/Comparer/Compare5
2.	Select the original and modified documents to be compared. Note: Input file formats include RTF, DOC, PDF and HTML.
3.	You can specify a custom client-side Options Set (i.e. a rendering set filename) along with the request, or use a default server-side configuration by selecting from the dropdown. Refer to the Workshare Compare service Rendering Set Guide for details on rendering set values.
4.	Select the type of output you require. You can select one of the following:
	+	**Redline**: The comparison can be in RTF, DOC, DOCX or PDF format.
	+	**WDF**: The comparison is in the Workshare DeltaFile format which can be opened in the Workshare Professional Compare module.
5.	If required, you can also select Summary to include an XML summary in the output.
6.	If security is activated, Enter user name, password and domain details for a Windows account which is valid on the server.
7.	Select Compare. The requested output documents are generated on the server and returned to the client.

