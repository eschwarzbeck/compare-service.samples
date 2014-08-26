Advanced Web Sample
====================

The AdvancedWebSample ASP application demonstrates using the Workshare Compare service Control DLL to connect to the server and perform synchronous comparisons.

Requires Visual Studio 2012, .NET 4 Framwork. 

Publish "WebDeployPackage" to create a msdeploy zip for deployment. Deployment requires [Microsoft Web Deploy 3.5](http://www.iis.net/downloads/microsoft/web-deploy).

Running the Advanced Web Sample
-------------------------------

1.	Launch website. The login screen is displayed.
2.	Enter your login credentials and click Log in. The document selection screen is displayed.
3.	Select the original and modified documents to be compared.
4.	If you want to compare the original document against multiple documents, click Add File. An additional Modified Document field is added.
5.	From the Select Rendering Set dropdown list, select the rendering set you want applied to the comparison. 
6.	Select the type of output you require - a Redline document (in RTF, DOC, DOCX, PDF or WDF file format) with or without an XML summary or just an XML summary.
7.	Click Compare Now. The comparison is performed and the results screen is displayed. You can view the comparison document by clicking on the output file link. You can also view the original and modified documents.



Configuration
---------------

### Run from a remote machine

In order to run the advanced sample from a remote machine, please edit the *[Web.config](Web.config)*.
Here is an extract of the *[Web.config](Web.config)*:

```xml

	<configuration>
		<appSettings>
			<add key="renderset.path" value="data/renderset"/>
			<add key="tempdata.path" value="data/temp"/>
			<add key="cs.host" value="localhost"/>
			<add key="cs.port" value="8080"/>
	
```

In the code above, update the value of *cs.host* to the FQDN or IP address of your Compare Server. Change the value of *cs.port* to update the port number if necessary.

### Compare server hosted within IIS

In an IIS hosted configuration, Compare server is accessible only via HTTP and HTTPS. The service address is: 
	
		
			http[s]://[hostname]:[port]/compareservice.svc

			
Where hostname is the name or  IP address of the machine where Workshare Compare service is installed.

To be able to access the advanced web sample, the web.config must be updated.  
Once the website is deployed, open the web.config, find the `<appSettings>` node and update the *url.address* value to /comparewebservice.svc


**Original web.config :**

```xml
		<configuration>
			<appSettings>
				<add key="renderset.path" value="data/renderset"/>
				<add key="tempdata.path" value="data/temp"/>
				<add key="cs.host" value="localhost"/>
				<add key="cs.port" value="8080"/>
				<add key="transport.protocol" value="http"/>
				<!-- Blank out value if CS is running as a service -->
				<add key="url.address" value="/Comparer"/>
				
```

 
**Updated web.config :**


```xml
		<configuration>
			<appSettings>
				<add key="renderset.path" value="data/renderset"/>
				<add key="tempdata.path" value="data/temp"/>
				<add key="cs.host" value="localhost"/>
				<add key="cs.port" value="8080"/>
				<add key="transport.protocol" value="http"/>
				<!-- Blank out value if CS is running as a service -->
				<add key="url.address" value="/comparewebservice.svc"/>


```





