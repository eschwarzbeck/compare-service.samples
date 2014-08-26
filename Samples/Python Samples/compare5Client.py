from pysimplesoap.client import SoapClient
import pprint
from pysimplesoap.simplexml import SimpleXMLElement
import base64
from C5Helpers import printClient, printOperations, get_b64_content, get_content


wsdlUrl = "http://localhost:8080/Comparer?wsdl"
proxy={'proxy_host':'localhost','proxy_port':'8888'}

# Data
original_path = "data/Original.doc"
modified_path = "data/Modified.doc"
renderset_path = "data/Standard.set"
output_path = "result.rtf"
output_redlineMl="redlineMl.xml"

# responseOption may be: "Rtf", "DocX", "Pdf", see readme for all available formats
# RedlineMl and Rtf
responseOption="RedlinMlAndRtf"                

# Client
compare5Client = SoapClient(wsdl=wsdlUrl,trace=False)
# Proxy support requires httplib2 and socks 
# compare5Client = SoapClient(wsdl=wsdlUrl,proxy=proxy,trace=False)

# to list Compare server ports
#printClient(compare5Client,operations=True)

# to list Compare5 operations:
#printOperations(compare5Client.services['Comparer']['ports']['CompareWebServiceWCF'])

#Set port
compare5Client.service_port='Comparer','CompareWebServiceWCF'

# Compare documents:
# Operation: Execute
# Namespace: http://workshare.com/compareservices/5.0/comparewebservice/
# Input {'Execute': {'OriginalData': <class 'str'>, 'ModifiedData': <class 'str'>, 'ResponseOption': <class 'str'>, 'CompareOptions': <class 'str'>}}

# base64 encode data 
orginalData=get_b64_content(original_path, 'rb')
modifiedData=get_b64_content(modified_path, 'rb')

#content of .set file
compareOptions= get_content(renderset_path, 'r')

# Compare documents
result=compare5Client.Execute(OriginalData=orginalData,
                 ModifiedData=modifiedData,
                 ResponseOption=responseOption,
                 CompareOptions=compareOptions)

# Output: 
# {'ExecuteResult': {'Redline': <class 'str'>, 'RedlineMl': <class 'str'>, 'Summary': <class 'str'>}}}

# Retrieve response
redline = result["ExecuteResult"]["Redline"]
with open(output_path, "wb") as save_handle:
    save_handle.write(base64.b64decode(redline))

# Save RedlineMl
redlineMl = result["ExecuteResult"]["RedlineMl"]
with open(output_redlineMl, "wb") as save_handle:
    save_handle.write(redlineMl.encode('utf-16'))
