Web Client Sample
====================

This example provides a simple example of a user facing site that provides on-demand comparison functionality to authenticated users only.

Requires Visual Studio 2012, .NET 4 Framwork. 

Publish "WebDeployPackage" to create a msdeploy zip for deployment. Deployment requires [Microsoft Web Deploy 3.5](http://www.iis.net/downloads/microsoft/web-deploy).


Running the Web Client Sample
-------------------------------

1.	Launch website. The login screen is displayed.
2.	Enter your login credentials and click Log in.
3.	Select the Comparison tab.
4.	Click Select File and select the original document to be compared.
5.	Click Next.
6.	Click Select File and select the modified document to be compared.
7.	If you want to compare the original document against multiple documents, click Add File. An additional Modified Document field is added.
8.	Click Next.
9.	From the Select a rendering set dropdown list, select the rendering set you want applied to the comparison. 
10.	Click Next.
11.	From the Select output type dropdown list, select the type of output you require � a Redline document (in RTF, DOC, DOCX, PDF or WDF file format) with or without an XML summary or just an XML summary.
12.	Click Compare. The comparison is performed and the results screen is displayed. You can view the comparison document by clicking on the output file link.




Configuration
---------------

The Web Client application needs to be installed to a website root. The default location is "Default Web Site". 


### Authentication

The web client uses Windows Authentication. If you use IIS, make sure that *Anonymous authentication* is disabled and *Windows authentication* is enabled.


### Parameters

The sample uses the location of Compare service rendering sets. If the current installation of compare server is not configured with the default location, it must be specified.
In the Web.config, the parameter used for the location of the rendering sets is: *renderset.path*.


```xml
			<appSettings>
				<add key="EndpointConfig" value="CompareWebServiceWCF" />
				<add key="EndpointAddress" value="http://localhost:8080/Comparer/Compare5" />
				<add key="renderset.path" value="C:\ProgramData\Workshare\Compare Service\Rendering Sets" />
				<add key="UploadPath" value="Upload" />
```


### Compare server hosted within IIS

To be able to compare documents using the web client when compare server is deployed as a website, you must edit the Web.config and update the value of EndpointAddress.

**Original web.config:**

```xml
			<appSettings>
				<add key="EndpointConfig" value="CompareWebServiceWCF" />
				<add key="EndpointAddress" value="http://localhost:8080/Comparer/Compare5" />
				<add key="renderset.path" value="C:\ProgramData\Workshare\Compare Service\Rendering Sets" />
				<add key="UploadPath" value="Upload" />
```
				
**Updated web.config:**


```xml
			<appSettings>
				<add key="EndpointConfig" value="CompareWebServiceWCF" />
				<add key="EndpointAddress" value="http://localhost:8080/compareservice.svc/Compare5" />
				<add key="renderset.path" value="C:\ProgramData\Workshare\Compare Service\Rendering Sets" />
				<add key="UploadPath" value="Upload" />
```


