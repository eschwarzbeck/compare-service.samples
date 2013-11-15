/**
 * ExecuteEx.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class ExecuteEx  implements java.io.Serializable {
    private com.workshare.compareservices._5_0.comparewebservice.ExecuteParams execParams;

    public ExecuteEx() {
    }

    public ExecuteEx(
           com.workshare.compareservices._5_0.comparewebservice.ExecuteParams execParams) {
           this.execParams = execParams;
    }


    /**
     * Gets the execParams value for this ExecuteEx.
     * 
     * @return execParams
     */
    public com.workshare.compareservices._5_0.comparewebservice.ExecuteParams getExecParams() {
        return execParams;
    }


    /**
     * Sets the execParams value for this ExecuteEx.
     * 
     * @param execParams
     */
    public void setExecParams(com.workshare.compareservices._5_0.comparewebservice.ExecuteParams execParams) {
        this.execParams = execParams;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof ExecuteEx)) return false;
        ExecuteEx other = (ExecuteEx) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.execParams==null && other.getExecParams()==null) || 
             (this.execParams!=null &&
              this.execParams.equals(other.getExecParams())));
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
        if (getExecParams() != null) {
            _hashCode += getExecParams().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(ExecuteEx.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", ">ExecuteEx"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("execParams");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "execParams"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ExecuteParams"));
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
