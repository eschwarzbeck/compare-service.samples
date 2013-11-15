/**
 * GetOptionsSetResponse.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class GetOptionsSetResponse  implements java.io.Serializable {
    private java.lang.String getOptionsSetResult;

    public GetOptionsSetResponse() {
    }

    public GetOptionsSetResponse(
           java.lang.String getOptionsSetResult) {
           this.getOptionsSetResult = getOptionsSetResult;
    }


    /**
     * Gets the getOptionsSetResult value for this GetOptionsSetResponse.
     * 
     * @return getOptionsSetResult
     */
    public java.lang.String getGetOptionsSetResult() {
        return getOptionsSetResult;
    }


    /**
     * Sets the getOptionsSetResult value for this GetOptionsSetResponse.
     * 
     * @param getOptionsSetResult
     */
    public void setGetOptionsSetResult(java.lang.String getOptionsSetResult) {
        this.getOptionsSetResult = getOptionsSetResult;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof GetOptionsSetResponse)) return false;
        GetOptionsSetResponse other = (GetOptionsSetResponse) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.getOptionsSetResult==null && other.getGetOptionsSetResult()==null) || 
             (this.getOptionsSetResult!=null &&
              this.getOptionsSetResult.equals(other.getGetOptionsSetResult())));
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
        if (getGetOptionsSetResult() != null) {
            _hashCode += getGetOptionsSetResult().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(GetOptionsSetResponse.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", ">GetOptionsSetResponse"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("getOptionsSetResult");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "GetOptionsSetResult"));
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
