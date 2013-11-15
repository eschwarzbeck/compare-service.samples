/**
 * Comparer.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._1_1.comparewebservice;

public interface Comparer extends javax.xml.rpc.Service {
    public java.lang.String getCompareWebServiceSoapAddress();

    public com.workshare.compareservices._1_1.comparewebservice.ILegacyComparer getCompareWebServiceSoap() throws javax.xml.rpc.ServiceException;

    public com.workshare.compareservices._1_1.comparewebservice.ILegacyComparer getCompareWebServiceSoap(java.net.URL portAddress) throws javax.xml.rpc.ServiceException;
    public java.lang.String getCompareWebServiceWCFAddress();

    public com.workshare.compareservices._5_0.comparewebservice.IComparer getCompareWebServiceWCF() throws javax.xml.rpc.ServiceException;

    public com.workshare.compareservices._5_0.comparewebservice.IComparer getCompareWebServiceWCF(java.net.URL portAddress) throws javax.xml.rpc.ServiceException;
}
