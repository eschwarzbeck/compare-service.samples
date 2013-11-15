/**
 * ComparerLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._1_1.comparewebservice;

public class ComparerLocator extends org.apache.axis.client.Service implements com.workshare.compareservices._1_1.comparewebservice.Comparer {

    public ComparerLocator() {
    }


    public ComparerLocator(org.apache.axis.EngineConfiguration config) {
        super(config);
    }

    public ComparerLocator(java.lang.String wsdlLoc, javax.xml.namespace.QName sName) throws javax.xml.rpc.ServiceException {
        super(wsdlLoc, sName);
    }

    // Use to get a proxy class for CompareWebServiceSoap
    private java.lang.String CompareWebServiceSoap_address = "http://ln1-dev006.wsdev.net/WCS/CompareWebService.svc";

    public java.lang.String getCompareWebServiceSoapAddress() {
        return CompareWebServiceSoap_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String CompareWebServiceSoapWSDDServiceName = "CompareWebServiceSoap";

    public java.lang.String getCompareWebServiceSoapWSDDServiceName() {
        return CompareWebServiceSoapWSDDServiceName;
    }

    public void setCompareWebServiceSoapWSDDServiceName(java.lang.String name) {
        CompareWebServiceSoapWSDDServiceName = name;
    }

    public com.workshare.compareservices._1_1.comparewebservice.ILegacyComparer getCompareWebServiceSoap() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(CompareWebServiceSoap_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getCompareWebServiceSoap(endpoint);
    }

    public com.workshare.compareservices._1_1.comparewebservice.ILegacyComparer getCompareWebServiceSoap(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            com.workshare.compareservices._1_1.comparewebservice.CompareWebServiceSoapStub _stub = new com.workshare.compareservices._1_1.comparewebservice.CompareWebServiceSoapStub(portAddress, this);
            _stub.setPortName(getCompareWebServiceSoapWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

    public void setCompareWebServiceSoapEndpointAddress(java.lang.String address) {
        CompareWebServiceSoap_address = address;
    }


    // Use to get a proxy class for CompareWebServiceWCF
    private java.lang.String CompareWebServiceWCF_address = "http://ln1-dev006.wsdev.net/WCS/CompareWebService.svc/Compare5";

    public java.lang.String getCompareWebServiceWCFAddress() {
        return CompareWebServiceWCF_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String CompareWebServiceWCFWSDDServiceName = "CompareWebServiceWCF";

    public java.lang.String getCompareWebServiceWCFWSDDServiceName() {
        return CompareWebServiceWCFWSDDServiceName;
    }

    public void setCompareWebServiceWCFWSDDServiceName(java.lang.String name) {
        CompareWebServiceWCFWSDDServiceName = name;
    }

    public com.workshare.compareservices._5_0.comparewebservice.IComparer getCompareWebServiceWCF() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(CompareWebServiceWCF_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getCompareWebServiceWCF(endpoint);
    }

    public com.workshare.compareservices._5_0.comparewebservice.IComparer getCompareWebServiceWCF(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            com.workshare.compareservices._5_0.comparewebservice.CompareWebServiceWCFStub _stub = new com.workshare.compareservices._5_0.comparewebservice.CompareWebServiceWCFStub(portAddress, this);
            _stub.setPortName(getCompareWebServiceWCFWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

    public void setCompareWebServiceWCFEndpointAddress(java.lang.String address) {
        CompareWebServiceWCF_address = address;
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        try {
            if (com.workshare.compareservices._1_1.comparewebservice.ILegacyComparer.class.isAssignableFrom(serviceEndpointInterface)) {
                com.workshare.compareservices._1_1.comparewebservice.CompareWebServiceSoapStub _stub = new com.workshare.compareservices._1_1.comparewebservice.CompareWebServiceSoapStub(new java.net.URL(CompareWebServiceSoap_address), this);
                _stub.setPortName(getCompareWebServiceSoapWSDDServiceName());
                return _stub;
            }
            if (com.workshare.compareservices._5_0.comparewebservice.IComparer.class.isAssignableFrom(serviceEndpointInterface)) {
                com.workshare.compareservices._5_0.comparewebservice.CompareWebServiceWCFStub _stub = new com.workshare.compareservices._5_0.comparewebservice.CompareWebServiceWCFStub(new java.net.URL(CompareWebServiceWCF_address), this);
                _stub.setPortName(getCompareWebServiceWCFWSDDServiceName());
                return _stub;
            }
        }
        catch (java.lang.Throwable t) {
            throw new javax.xml.rpc.ServiceException(t);
        }
        throw new javax.xml.rpc.ServiceException("There is no stub implementation for the interface:  " + (serviceEndpointInterface == null ? "null" : serviceEndpointInterface.getName()));
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(javax.xml.namespace.QName portName, Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        if (portName == null) {
            return getPort(serviceEndpointInterface);
        }
        java.lang.String inputPortName = portName.getLocalPart();
        if ("CompareWebServiceSoap".equals(inputPortName)) {
            return getCompareWebServiceSoap();
        }
        else if ("CompareWebServiceWCF".equals(inputPortName)) {
            return getCompareWebServiceWCF();
        }
        else  {
            java.rmi.Remote _stub = getPort(serviceEndpointInterface);
            ((org.apache.axis.client.Stub) _stub).setPortName(portName);
            return _stub;
        }
    }

    public javax.xml.namespace.QName getServiceName() {
        return new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", "Comparer");
    }

    private java.util.HashSet ports = null;

    public java.util.Iterator getPorts() {
        if (ports == null) {
            ports = new java.util.HashSet();
            ports.add(new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", "CompareWebServiceSoap"));
            ports.add(new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", "CompareWebServiceWCF"));
        }
        return ports.iterator();
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(java.lang.String portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        
if ("CompareWebServiceSoap".equals(portName)) {
            setCompareWebServiceSoapEndpointAddress(address);
        }
        else 
if ("CompareWebServiceWCF".equals(portName)) {
            setCompareWebServiceWCFEndpointAddress(address);
        }
        else 
{ // Unknown Port Name
            throw new javax.xml.rpc.ServiceException(" Cannot set Endpoint Address for Unknown Port" + portName);
        }
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(javax.xml.namespace.QName portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        setEndpointAddress(portName.getLocalPart(), address);
    }

}
