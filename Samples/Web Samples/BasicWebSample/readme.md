Basic Web Sample
====================

The BasicWebSample ASP application demonstrates how to use WCF in order to connect directly to the Workshare Compare service.

Requires Visual Studio 2012, .NET 4 Framwork. 

Publish "WebDeployPackage" to create a msdeploy zip for deployment. Deployment requires [Microsoft Web Deploy 3.5](http://www.iis.net/downloads/microsoft/web-deploy).


Running the Basic Web Sample
-------------------------------

1.	Launch website. The login screen is displayed.
2.	Enter your login credentials and click Authenticate. The document selection screen is displayed.
3.	Select the original and modified documents to be compared.
4.	From the Rendering Set dropdown list, select the rendering set you want applied to the comparison. 
5.	Click Do Compare. The comparison is performed and the results screen is displayed. You can view the comparison document by clicking on the output file link. You can also view the original and modified documents.


Configuration
---------------

### Run from a remote machine

The basic web application is configured to run on the same serve as Compare Server. To be able to run 
the sample from a remote location:

- Make sure that the compare service port is accessible from a remote location.
- Update the web.config and for each endpoint, change *localhost* to the FQDN or IP address of your Compare Server.

Below is an example with compare-server.domain.com  as the FQDN of compare server, and port 8080.	
		
```xml
		<client>
			<endpoint address="http://compare-server.domain.com:8080/Comparer/Compare5"
				  binding="basicHttpBinding" bindingConfiguration="Compare5WebService"
				  contract="CompareProxy.IComparer" name="CompareWebServiceWCF" />
			<endpoint address="http://compare-server.domain.com:8080/Comparer"
				  binding="basicHttpBinding" bindingConfiguration="CompareWebServiceSoap"
				  contract="CompareProxy.ILegacyComparer" name="CompareWebServiceSoap" />
		</client>

```  



### Compare server hosted within IIS

To configure the basic web application for compare server hosted within IIS, in the web.config find the `<client>` node, and update the addresses of the endpoints.
 
**Original web.config:**

```xml
		<client>
			<endpoint address="http://localhost:8080/Comparer/Compare5"
				  binding="basicHttpBinding" bindingConfiguration="Compare5WebService"
				  contract="CompareProxy.IComparer" name="CompareWebServiceWCF" />
			<endpoint address="http://localhost:8080/Comparer"
				  binding="basicHttpBinding" bindingConfiguration="CompareWebServiceSoap"
				  contract="CompareProxy.ILegacyComparer" name="CompareWebServiceSoap" />
		</client>

```  
 
**Updated web.config:**


```xml
		<client>
			<endpoint address="http://localhost:8080/comparewebservice.svc/Compare5"
				  binding="basicHttpBinding" bindingConfiguration="Compare5WebService"
				  contract="CompareProxy.IComparer" name="CompareWebServiceWCF" />
			<endpoint address="http://localhost:8080/comparewebservice.svc"
				  binding="basicHttpBinding" bindingConfiguration="CompareWebServiceSoap"
				  contract="CompareProxy.ILegacyComparer" name="CompareWebServiceSoap" />
		</client>
  
```
