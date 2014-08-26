Web Admin Sample
====================

The WebAdmin ASP application provides a simple administrative interface showing how the web client site may be monitored and controlled by a group of administrative users.

Requires Visual Studio 2012, .NET 4 Framwork. 

Publish "WebDeployPackage" to create a msdeploy zip for deployment. Deployment requires [Microsoft Web Deploy 3.5](http://www.iis.net/downloads/microsoft/web-deploy).


Configuration
---------------

The Web Admin application needs to be installed to a website root if deployed to IIS. The default location is "Default Web Site".

### Authentication

The web admin sample uses Windows Authentication. If you use IIS, make sure that *Anonymous authentication* is disabled and *Windows authentication* is enabled.


### Parameters

The sample uses the location of Compare service rendering sets and logs. If the current installation of compare server is not configured with the default locations, they must be specified.
In the Web.config, *LogsDirectory* is the location of the logs, *renderset.path* is the rendering sets location


```xml
		<appSettings>
			<add key="EndpointConfig" value="CompareWebServiceWCF" />
			<add key="EndpointAddress" value="http://localhost:8080/Comparer/Compare5" />
			<add key="LogsDirectory" value="C:\ProgramData\Workshare\Compare Service\logs" />
			<add key="RecordsPerPage" value="10" />
			<add key="AdminRole" value="Administrators" />
			<add key="renderset.path" value="C:\ProgramData\Workshare\Compare Service\Rendering Sets" />
```


### Compare server hosted within IIS

To be able to consume the compare services hosted in IIS using the web admin application, you must edit the Web.config and update the value of *EndpointAddress*.

**Original web.config:**
```xml
		<appSettings>
			<add key="EndpointConfig" value="CompareWebServiceWCF" />
			<add key="EndpointAddress" value="http://localhost:8080/Comparer/Compare5" />
			<add key="LogsDirectory" value="C:\ProgramData\Workshare\Compare Service\logs" />
			<add key="RecordsPerPage" value="10" />
			<add key="AdminRole" value="Administrators" />
			<add key="renderset.path" value="C:\ProgramData\Workshare\Compare Service\Rendering Sets" />
```

**Updated web.config:**


```xml
		<appSettings>
			<add key="EndpointConfig" value="CompareWebServiceWCF" />
			<add key="EndpointAddress" value="http://localhost:8080/comparewebservice.svc/Compare5" />
			<add key="LogsDirectory" value="C:\ProgramData\Workshare\Compare Service\logs" />
			<add key="RecordsPerPage" value="10" />
			<add key="AdminRole" value="Administrators" />
			<add key="renderset.path" value="C:\ProgramData\Workshare\Compare Service\Rendering Sets" />
```


