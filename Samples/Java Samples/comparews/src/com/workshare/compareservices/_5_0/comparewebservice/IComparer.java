/**
 * IComparer.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public interface IComparer extends java.rmi.Remote {
    public java.lang.Boolean authenticate(java.lang.String sRealm, java.lang.String sUser, java.lang.String sPassword) throws java.rmi.RemoteException;
    public com.workshare.compareservices._5_0.comparewebservice.CompareResults execute(byte[] originalData, byte[] modifiedData, com.workshare.compareservices._5_0.comparewebservice.ResponseOptions responseOption, java.lang.String compareOptions) throws java.rmi.RemoteException;
    public com.workshare.compareservices._5_0.comparewebservice.CompareResults executeEx(com.workshare.compareservices._5_0.comparewebservice.ExecuteParams execParams) throws java.rmi.RemoteException;
    public java.lang.String getVersion() throws java.rmi.RemoteException;
    public java.lang.String getCompositorVersion() throws java.rmi.RemoteException;
    public java.lang.String getOptionsSet() throws java.rmi.RemoteException;
    public java.lang.Boolean setOptionsSet(java.lang.String sOptionsSet) throws java.rmi.RemoteException;
}
