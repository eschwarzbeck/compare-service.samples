/**
 * GetVersionResponse.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class GetVersionResponse  implements java.io.Serializable {
    private java.lang.String getVersionResult;

    public GetVersionResponse() {
    }

    public GetVersionResponse(
           java.lang.String getVersionResult) {
           this.getVersionResult = getVersionResult;
    }


    /**
     * Gets the getVersionResult value for this GetVersionResponse.
     * 
     * @return getVersionResult
     */
    public java.lang.String getGetVersionResult() {
        return getVersionResult;
    }


    /**
     * Sets the getVersionResult value for this GetVersionResponse.
     * 
     * @param getVersionResult
     */
    public void setGetVersionResult(java.lang.String getVersionResult) {
        this.getVersionResult = getVersionResult;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof GetVersionResponse)) return false;
        GetVersionResponse other = (GetVersionResponse) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.getVersionResult==null && other.getGetVersionResult()==null) || 
             (this.getVersionResult!=null &&
              this.getVersionResult.equals(other.getGetVersionResult())));
        __equalsCalc = null;
        return _equals;
    }

    private boolean __hashCodeCalc = false;
    public synchronized int hashCode() {
        if (__hashCodeCalc) {
            return 0;
        }
        __hashCodeCalc = true;
        int _hashCode = 1;
        if (getGetVersionResult() != null) {
            _hashCode += getGetVersionResult().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(GetVersionResponse.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", ">GetVersionResponse"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("getVersionResult");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "GetVersionResult"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
    }

    /**
     * Return type metadata object
     */
    public static org.apache.axis.description.TypeDesc getTypeDesc() {
        return typeDesc;
    }

    /**
     * Get Custom Serializer
     */
    public static org.apache.axis.encoding.Serializer getSerializer(
           java.lang.String mechType, 
           java.lang.Class _javaType,  
           javax.xml.namespace.QName _xmlType) {
        return 
          new  org.apache.axis.encoding.ser.BeanSerializer(
            _javaType, _xmlType, typeDesc);
    }

    /**
     * Get Custom Deserializer
     */
    public static org.apache.axis.encoding.Deserializer getDeserializer(
           java.lang.String mechType, 
           java.lang.Class _javaType,  
           javax.xml.namespace.QName _xmlType) {
        return 
          new  org.apache.axis.encoding.ser.BeanDeserializer(
            _javaType, _xmlType, typeDesc);
    }

}
