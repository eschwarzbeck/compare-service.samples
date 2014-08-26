Compare service Java client sample
===================================

This is sample code for Compare server. This example demonstrates how a Java client can be written.


Prerequisites
---------

+ Java JDK
+ [Maven](http://maven.apache.org/)
	- [Installation instructions](http://maven.apache.org/download.cgi#Installation)


Content
---------

+ [src/](./src/): This is the source code folder  
	- Client code is in package: com.workshare.compareservices.client
	- Generated code:
		- com.workshare.compareservices._1_1.comparewebservice
		- com.workshare.compareservices._5_0.comparewebservice
		- com.workshare.compareservices._5_2.comparewebservice
		- com.microsoft.schemas._2003._10.serialization
		
+ [data/](./data/)
	- host.txt: text file default host address
	- Original.doc: sample original file for comparison
	- Modified.doc: sample modified file for comparison
	- Standard.set: sample rendering set
	
+ [pom.xml](./pom.xml): contains information about the project and configuration details used by Maven to build the project.


Running the Compare service Java client
-----------------------------------------

1. Go to  [Samples/Java Samples/CompareServiceClient](.) and run:


		mvn package


2. Go to [target/](./target) folder double click on the file *CompareServiceClient.jar* or from command line run:  


		java -jar target\CompareServiceClient.jar


2.	Select the original and modified documents to be compared.
	
3.	You can specify a custom rendering set to be loaded and sent along with the request. If Rendering Set is unchecked, the default server configuration rendering set will be used. Refer to the Workshare Compare service Rendering Set Guide for details on rendering set values.
4.	Select the type of output you require - Redline, ReadlineML and XML summary.
5.	Enter user name, password and domain details for a Windows account which is valid on the server. Although these are not used by the basic HTTP binding they are required to allow for future enhancements to the service authentication process.
6.	Select Compare. The requested output documents are generated on the server and returned to the client.



Additional resources
-----------------------------------------

### Compile the the Compare service Java client


		mvn compile 


### Generate executable file to run the Compare Service Java Client 

Create the package:

		mvn package

This command will compile the source code and generate an executable file in [target/](./target/).
To run the executable you can double click on the .jar file or run:		
		
		
		java -jar target\CompareServiceClient.jar


### Generate source from WSDL file 


The java source code to consume the compare services has been generated using CXF, an Apache tool.
Here are the instructions to generate the Java source code tree using the currently published service WSDL.
To generate the code using the default parameters run:


		mvn generate-sources -Pcxf-codegen

	
+ The parameter to change the wsdl location is *compareservice.wsdl*. The default value for the WSDL is: http://localhost:8080/comparer?wsdl 
+ The parameter to change the wsdl location is *cxf.SourceRoot*. The default folder to generate the source is target/generated/cxf


To change the default parameters, you can either update the pom.xml before running the mvn command or provide them with the command line.

For example: 

	mvn generate-sources -Pcxf-codegen -Dcxf.SourceRoot="C:\csclient\src\" -Dcompareservice.wsdl="http://cs-host.domain.co.uk:8080/CompareWebService.svc?wsdl" 



**Edit the pom.xml**

```xml
<project> ...

 <properties>
		...
		
  	<compareservice.wsdl>http://cs-host.domain.co.uk:8080/CompareWebService.svc?wsdl</compareservice.wsdl>
  	<cxf.SourceRoot>C:\csclient\src\</cxf.SourceRoot>
	
		...
  </properties>
</project>
```





