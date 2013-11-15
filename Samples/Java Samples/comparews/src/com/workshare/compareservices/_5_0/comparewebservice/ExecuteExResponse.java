/**
 * ExecuteExResponse.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class ExecuteExResponse  implements java.io.Serializable {
    private com.workshare.compareservices._5_0.comparewebservice.CompareResults executeExResult;

    public ExecuteExResponse() {
    }

    public ExecuteExResponse(
           com.workshare.compareservices._5_0.comparewebservice.CompareResults executeExResult) {
           this.executeExResult = executeExResult;
    }


    /**
     * Gets the executeExResult value for this ExecuteExResponse.
     * 
     * @return executeExResult
     */
    public com.workshare.compareservices._5_0.comparewebservice.CompareResults getExecuteExResult() {
        return executeExResult;
    }


    /**
     * Sets the executeExResult value for this ExecuteExResponse.
     * 
     * @param executeExResult
     */
    public void setExecuteExResult(com.workshare.compareservices._5_0.comparewebservice.CompareResults executeExResult) {
        this.executeExResult = executeExResult;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof ExecuteExResponse)) return false;
        ExecuteExResponse other = (ExecuteExResponse) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.executeExResult==null && other.getExecuteExResult()==null) || 
             (this.executeExResult!=null &&
              this.executeExResult.equals(other.getExecuteExResult())));
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
        if (getExecuteExResult() != null) {
            _hashCode += getExecuteExResult().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(ExecuteExResponse.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", ">ExecuteExResponse"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("executeExResult");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ExecuteExResult"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "CompareResults"));
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
