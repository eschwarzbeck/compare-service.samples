Compare Server ASP samples
===========================

Prerequisites
-------------

- Compare Server
- IIS
- Visual Studio 2012
- To deploy to a remote machine, Web deploy is needed. see: [http://www.iis.net/downloads/microsoft/web-deploy](http://www.iis.net/downloads/microsoft/web-deploy)
	
Contents
--------

- [Advanced web sample](AdvancedWebSample)
- [Basic web sample](BasicWebSample) 
- [Config page sample](ConfigPageSample) 
- Web Client 
- Web Admin


Instructions
------------

For the IIS configuration, please note that the compare service is accessible via: http://[Host]:[Port]/comparewebservice.svc


The Web samples can be run from Visual studio or deployed to IIS.

### Deploy to IIS

The samples can be deployed to IIS via the *publish* feature of Visual studio.

Select the web application that you want to deploy, right click then choose *Publish...*. There are two profiles defined:

- WebDeploy: It uses the Web Deploy publish method. The web application will be installed directly to IIS.
	
- WebDeployPackage: It uses the Web Deploy Package publish method. The web application will be deployed as a .zip package.



#### Default deployment settings

Sample name 		| Default parameters
--------------------| -------------
Advanced web sample | Application path : Default Web Site/AdvWebSample
Basic web sample 	| Application path : Default Web Site/BasicWebSample
Config page sample	| Application path : Default Web Site (needs to be deployed to the website root) <br/> Compare service binaries location: C:\Program Files\Workshare\Compare Service\bin <br/> Compare service binaries location:  C:\Program Files\Workshare\Compare Service\logs
Web Admin sample	| Application path : Default Web Site <br/> logs directory: C:\ProgramData\Workshare\Compare Service\logs <br>  Rendering sets path: C:\ProgramData\Workshare\Compare Service\Rendering Sets
Web Client sample	| Application path : Default Web Site <br/> Rendering sets path: C:\ProgramData\Workshare\Compare Service\Rendering Sets



