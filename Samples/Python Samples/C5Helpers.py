import base64
import sys

def printClient(client, operations=False):
    for service in client.services.values():

        for port in service['ports'].values():
            print()
            print(port['name']+ " (service: "+port['service_name']+")")
            print("\t - Location: "+port['location'])
            print("\t - Soap version: "+port['soap_ver'])
            print()
            if operations:
                print("\t - Operations: ")
                for op in port['operations'].values():
                    print('\t Name:', op['name'])
                    print('\t Namespace:', op['namespace'])
                    print('\t Docs:', op['documentation'].strip())
                    print('\t SOAPAction:', op['action'])
                    print('\t Input', op['input']) # args type declaration
                    print('\t Output', op['output']) # returns type declaration
                    print("")


def printOperations(port):
    print("Port %s operations:" %(port['name']))
    for op in port['operations'].values():
        print('\tName:', op['name'])
        print('\tNamespace:', op['namespace'])
        print('\tDocs:', op['documentation'].strip())
        print('\tSOAPAction:', op['action'])
        print('\tInput', op['input']) # args type declaration    
        print('\tOutput', op['output']) # returns type declaration
        print("")


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

    
