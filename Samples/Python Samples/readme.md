Python samples for Compare Server 
=================================

This is sample code for Compare server. This sample compares "data/Original.doc" with "data/Modified.doc", rendering the comparision to "result.rtf" based on the rendering set defined in "data/Standard.set"


Prerequisites
-------------
- pysimplesoap 1.14+ (earlier versions do not support <import...> tag in wsdl)
 - supports python 2.7 & 3.x
 - see https://code.google.com/p/pysimplesoap/


Contents
--------
- example.py (example code)
- data/original.doc, data/modified.doc (sample documents)
- data/standard.set (sample rendering set)

Instructions
------------

### Configure Compare Server to send reference itself correctly in SOAP:
To call Compare Server from a remote machine, you will need to make the following modification:

1. Open C:\Program Files\Workshare\Compare Service\bin\Workshare.CompareService.ServiceHost.exe.config
2. Change "localhost" to the FQDN of your Compare Server.

### Perform a comparison:

1. Modify "wsdl_url" variable in example.py

2. Run: python example.py

