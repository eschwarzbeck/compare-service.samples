import base64
import sys
from pysimplesoap.client import SoapClient

wsdl_url = "http://localhost:8080/Comparer?wsdl"
original_path = "data/Original.doc"
modified_path = "data/Modified.doc"
renderset_path = "data/Standard.set"
output_path = "result.rtf"

# preferred_redline_format may be: "Rtf", "DocX", "Pdf"
preferred_redline_format = "Rtf" 

def get_b64_content(path, file_mode):
    with open(path, file_mode) as handle:
        file_content_b64 = ""
        for line in handle:
            if sys.version_info[0] < 3:
                file_content_b64 += base64.b64encode(line)
            else:
                file_content_b64 += base64.b64encode(line).decode('ascii')
        return file_content_b64

def get_content(path, file_mode):
    with open(path, file_mode) as handle:
        file_content = ""
        for line in handle:
            file_content += line
        return file_content

# base64 encode data sent to compare server
original_file_content_b64 = get_b64_content(original_path, 'rb')
modified_file_content_b64 = get_b64_content(modified_path, 'rb')
renderset_content = get_content(renderset_path, 'r')

# compare documents...        
# renderset is optional, and may be substituted with an empty string
# eg. response = client.Execute2(original_file_content_b64, modified_file_content_b64, preferred_redline_format, "")
client = SoapClient(wsdl=wsdl_url)
response = client.Execute2(original_file_content_b64, modified_file_content_b64, preferred_redline_format, renderset_content)

# retrieve response
result = response["Execute2Result"]["Redline"]
with open(output_path, "wb") as save_handle:
    save_handle.write(base64.b64decode(result))
