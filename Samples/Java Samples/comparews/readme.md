Comparews Java application
=================================

This example demonstrates how a Java client can be written. The comparews Java application has been precompiled using Java SDK1.5, and demonstrates utilizing the Workshare Compare service via the legacy Soap endpoint and Direct, synchronous, connection method.


Running the COMPAREWS Application
-----------------------------------


1.	Go to  [Samples/Java Samples/comparews ](Samples/Java%20Samples/comparews )
	Note: The sample application is pre-compiled using Java SDK 1.5, and requires JRE version 1.5 or greater in order to run.
2.	Select the original and modified documents to be compared.
	Note: Only RTF and DOC source documents are accepted in the legacy SOAP interface.
3.	You can specify a custom rendering set to be loaded and sent along with the request. If Rendering Set is unchecked, the default server configuration rendering set will be used. Refer to the Workshare Compare service Rendering Set Guide for details on rendering set values.
4.	Select the type of output you require - Redline document (in RTF file format) and an XML summary.
5.	Enter user name, password and domain details for a Windows account which is valid on the server. Although these are not used by the basic HTTP binding they are required to allow for future enhancements to the service authentication process.
6.	Select Compare. The requested output documents are generated on the server and returned to the client.


Provided are additional BATCH scripts to aid in Java development:

+	compile JDK1_5.cmd – recompiles the src source code tree using JDK 1.5 (if installed)
+	compile JDK1_6.cmd – recompiles the src source code tree using JDK 1.6 (if installed)
+	wsdl2java.cmd – regenerates the Java source code tree using the currently published service WSDL on the local server.

	
