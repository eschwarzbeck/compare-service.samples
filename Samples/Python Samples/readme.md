Python samples for Compare Server 5
====================================

This is a sample code for Compare server. This sample compares [Original.doc](data/Original.doc) with 
[Modified.doc](data/Modified.doc) , rendering the comparison to "result.rtf" and "redlineMl.xml" 
based on the rendering set defined in [Standard.set](data/Standard.set). 



Prerequisites
-------------

- The Workshare fork of pysimplesoap: see [workshare/pysimplesoap](https://github.com/lea-n/pysimplesoap)
	+ supports python 2.7 & 3.x
	+ Clone the repository and to install run: 
		
			python setup.py install
			
	+ For pysimplesoap original project see: [https://code.google.com/p/pysimplesoap/](https://code.google.com/p/pysimplesoap/)
		
	
- Optionally, httplib2 and socksipy ([socks.py](socks.py)) are needed for proxy support
	+ The official repository for httplib2: [https://github.com/jcgregorio/httplib2](https://github.com/jcgregorio/httplib2).
	To install httplib2, Run:
		
				pip install httplib2
				
	+ SocksiPy - A Python SOCKS module: [http://sourceforge.net/projects/socksipy/](http://sourceforge.net/projects/socksipy/)
	
		<!-- SocksiPy branch https://code.google.com/p/socksipy-branch/ -->
	
See: [https://pip.pypa.io/en/latest/installing.html](https://pip.pypa.io/en/latest/installing.html) for pip installation.


Contents
--------

- [compare5Client.py](compare5Client.py): client to consume Compare server 5 web service
- [C5Helpers.py](C5Helpers.py): helpers for sample code 
- [socks.py](socks.py): socksipy library for proxy support


Instructions
------------


### Perform a comparison:

1. Modify *wsdlUrl* variable in [compare5Client.py](compare5Client.py)

2. Run: python [compare5Client.py](compare5Client.py)


#### Response option available formats:

- Rtf, Xml, RtfWithSummary, Wdf, 
WdfWithSummary, Doc, DocWithSummary, 
DocX, DocXWithSummary, Pdf, PdfWithSummary,
RedlinMl, RedlinMlAndRtf, RedlinMlAndSummary, 
RedlinMlAndRtfAndSummary, RedlinMlAndDoc, 
RedlinMlAndDocAndSummary, RedlinMlAndDocX, 
RedlinMlAndDocXAndSummary, RedlinMlAndPdf, 
RedlinMlAndPdfAndSummary 

