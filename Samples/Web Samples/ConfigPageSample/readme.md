ConfigPage Sample 
====================

The ConfigPageSample ASP application contains the full source code for the Administration Dashboard and demonstrates the more advanced features of the Workshare Compare service.

Requires Visual Studio 2012, .NET 4 Framwork. 

Publish "WebDeployPackage" to create a msdeploy zip for deployment. Deployment requires [Microsoft Web Deploy 3.5](http://www.iis.net/downloads/microsoft/web-deploy).


Configuration
---------------

The ConfigPage sample needs to be installed to a website root. The default location is "Default Web Site". 

### Parameters

The sample uses the location of Compare service binaries and logs. If the current installation of compare server is not configured with the default locations, they must be specified.
In web.config the parameters to configure the locations of the binaries and logs are: *HostFolder* and *LogsFolder*. Here is an extract of the Web.config


```xml
			<configuration>
				<appSettings>
					<add key="ChartImageHandler" value="storage=file;timeout=20;Url=~/TempImageFiles/;" />
					<add key="HostFolder" value="C:\Program Files\Workshare\Compare Service\bin" />
					<add key="LogsFolder" value="C:\Program Files\Workshare\Compare Service\logs" />
					<add key="HostLogFileName" value="compare_service_host.log" />
					<add key="AuditLogFileName" value="compare_service_audit.log" />
					<add key="SystemLogFileName" value="compare_service_system.log" />
```

Please note that if the log files are not present at the  specified log location, you will not be able to access the Config page application. 
This occurs for IIS deployment when the Compare web service has not been consumed yet. To avoid this issue, you can create the log files: compare_service_host.log, compare_service_audit.log and compare_service_system.log. 

#### Deploy to IIS and edit *Parameters.xml*

This is an extract of parameters.xml, the  file must be edited before deployment to IIS otherwise you will have to update the *Web.config*

```xml
	<parameters>
		<parameter name="Compare Service bin folder location" description="Please provide the Compare Service bin folder location"
				  defaultValue="C:\Program Files\Workshare\Compare Service\bin" tags="">
			<parameterEntry kind="XmlFile" scope="\\web.config$" match="/configuration/appSettings/add[@key='HostFolder']/@value" />
		</parameter>
		<parameter name="Log folder location" description="Please provide the Compare Service log folder location" 
				defaultValue="C:\Program Files\Workshare\Compare Service\logs" tags="">
			<parameterEntry kind="XmlFile" scope="\\web.config$" match="/configuration/appSettings/add[@key='LogsFolder']/@value" />
		</parameter>
		
```

#### Edit the *Web.config*

To specify the location of the binaries and logs please edit the values of *HostFolder* and *LogsFolder*.

```xml

	<configuration>
		<appSettings>
			<add key="ChartImageHandler" value="storage=file;timeout=20;Url=~/TempImageFiles/;"/>
			<add key="HostFolder" value="C:\Program Files\Workshare\Compare Service\bin"/>
			<add key="LogsFolder" value="C:\Program Files\Workshare\Compare Service\logs"/>
			<add key="HostLogFileName" value="compare_service_host.log"/>
			<add key="AuditLogFileName" value="compare_service_audit.log"/>
			<add key="SystemLogFileName" value="compare_service_system.log"/>
			<add key="DataFolder" value="/data"/>
			<add key="http_port" value="8080"/>


```


### Compare server hosted within IIS

To be able to consume the compare services hosted in IIS, you must edit the Web.config and update the value of *url.address*.

**Original web.config:** 


```xml
			<configuration>
				<appSettings>
					<add key="ChartImageHandler" value="storage=file;timeout=20;Url=~/TempImageFiles/;" />
					<add key="HostFolder" value="C:\Program Files\Workshare\Compare Service\bin" />
					<add key="LogsFolder" value="C:\Program Files\Workshare\Compare Service\logs" />
					<add key="HostLogFileName" value="compare_service_host.log" />
					<add key="AuditLogFileName" value="compare_service_audit.log" />
					<add key="SystemLogFileName" value="compare_service_system.log" />
					<add key="DataFolder" value="/data" />
					<add key="http_port" value="8080" />
					<add key="tcp_port" value="8090" />
					<add key="records_per_page" value="50" />
					<add key="url.address" value="/Comparer" /> 
```


**Updated web.config:**

```xml
			<configuration>
				<appSettings>
					<add key="ChartImageHandler" value="storage=file;timeout=20;Url=~/TempImageFiles/;" />
					<add key="HostFolder" value="C:\Program Files\Workshare\Compare Service\bin" />
					<add key="LogsFolder" value="C:\Program Files\Workshare\Compare Service\logs" />
					<add key="HostLogFileName" value="compare_service_host.log" />
					<add key="AuditLogFileName" value="compare_service_audit.log" />
					<add key="SystemLogFileName" value="compare_service_system.log" />
					<add key="DataFolder" value="/data" />
					<add key="http_port" value="8080" />
					<add key="tcp_port" value="8090" />
					<add key="records_per_page" value="50" />
					<add key="url.address" value="/comparewebservice.svc" />
					
```
