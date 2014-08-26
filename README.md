compare-service.samples
=======================


This repository contains development samples for Workshare Compare Server. 
It is intended to be a a quick-start demonstration of how to build a 
Compare Server based solution, and does not necessarily demonstrate best practices.

A full set of documentation for Compare Server, including the developer's guide, 
can be found on the [Workshare Knowledge Base](http://workshare.force.com/knowledgebase/pkb_Home?l=en_US&c=Product%3AWorkshare_Compare_Server).

The following samples are available:

C# Sample Code
--------------
The following code is available in **Compare Service Samples.sln**.
### [Document.Services.Compare.Sample](Samples/C%23%20Samples/Document.Services.Compare.Sample/README.md)
*Samples/C# Samples/Document.Services.Compare.Sample*

C# application connects to the Workshare Compare service using a direct synchronous request/response method. 

### [AdvancedWebSample](Samples/Web Samples/AdvancedWebSample/readme.md)
*Samples/Web Samples/AdvancedWebSample*

ASP application demonstrates using the Workshare Compare service Control DLL to connect to the server and perform synchronous comparisons

### [BasicWebSample](Samples/Web Samples/BasicWebSample/readme.md)
*Samples/Web Samples/BasicWebSample*

ASP application demonstrates how to use WCF in order to connect directly to the Workshare Compare service.

### [ConfigPageSample](Samples/Web Samples/ConfigPageSample/readme.md)
*Samples/Web Samples/ConfigPageSample*

ASP application containing the full source code for the Administration Dashboard and demonstrates the more advanced features of the Workshare Compare service.

Sample Applications
-------------------
The following code is available in **Compare Server Web Applications.sln**.

### [WebAdmin](WebApplications/CompareServer.WebAdmin/readme.md)
*WebApplications/CompareServer.WebAdmin*

ASP application that provides a simple administrative interface showing how the web client site may be monitored and controlled by a group of administrative users.

### [WebClient](WebApplications/CompareServer.WebClient/readme.md)
*WebApplications/CompareServer.WebClient*

ASP application that provides a simple example of a user facing site that provides on-demand comparison functionality to authenticated users only


Other Languages
---------------
### comparews (java)
*Samples/Java Samples/comparews*

Java application has been precompiled using Java SDK1.5, and demonstrates utilizing the Workshare Compare service via the legacy Soap endpoint and Direct, synchronous, connection method.

### [compare5Client.py (python)](Samples/Python Samples/readme.md)
*Samples/Python Samples/compare5Client.py*

python script that connects to the Workshare Compare service using a direct synchronous request/response method



License
-------
Copyright 2014 Workshare 

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
