/**
 * ILegacyComparer.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._1_1.comparewebservice;

public interface ILegacyComparer extends java.rmi.Remote {
    public boolean authenticate(java.lang.String sDomain, java.lang.String sUser, java.lang.String sPass) throws java.rmi.RemoteException;
    public void execute(com.workshare.compareservices._1_1.comparewebservice.CompareResponseFlags flags, java.lang.String compareOptions) throws java.rmi.RemoteException;
    public com.workshare.compareservices._1_1.comparewebservice.CompareResult execute2(byte[] original, byte[] modified, com.workshare.compareservices._1_1.comparewebservice.CompareResponseFlags flags, java.lang.String compareOptions) throws java.rmi.RemoteException;
}
