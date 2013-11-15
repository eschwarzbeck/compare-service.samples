/**
 * CompareResult.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._1_1.comparewebservice;

public class CompareResult  implements java.io.Serializable {
    private byte[] redline;

    private com.workshare.compareservices._1_1.comparewebservice.CompareResultSummary summary;

    public CompareResult() {
    }

    public CompareResult(
           byte[] redline,
           com.workshare.compareservices._1_1.comparewebservice.CompareResultSummary summary) {
           this.redline = redline;
           this.summary = summary;
    }


    /**
     * Gets the redline value for this CompareResult.
     * 
     * @return redline
     */
    public byte[] getRedline() {
        return redline;
    }


    /**
     * Sets the redline value for this CompareResult.
     * 
     * @param redline
     */
    public void setRedline(byte[] redline) {
        this.redline = redline;
    }


    /**
     * Gets the summary value for this CompareResult.
     * 
     * @return summary
     */
    public com.workshare.compareservices._1_1.comparewebservice.CompareResultSummary getSummary() {
        return summary;
    }


    /**
     * Sets the summary value for this CompareResult.
     * 
     * @param summary
     */
    public void setSummary(com.workshare.compareservices._1_1.comparewebservice.CompareResultSummary summary) {
        this.summary = summary;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof CompareResult)) return false;
        CompareResult other = (CompareResult) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.redline==null && other.getRedline()==null) || 
             (this.redline!=null &&
              java.util.Arrays.equals(this.redline, other.getRedline()))) &&
            ((this.summary==null && other.getSummary()==null) || 
             (this.summary!=null &&
              this.summary.equals(other.getSummary())));
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
        if (getRedline() != null) {
            for (int i=0;
                 i<java.lang.reflect.Array.getLength(getRedline());
                 i++) {
                java.lang.Object obj = java.lang.reflect.Array.get(getRedline(), i);
                if (obj != null &&
                    !obj.getClass().isArray()) {
                    _hashCode += obj.hashCode();
                }
            }
        }
        if (getSummary() != null) {
            _hashCode += getSummary().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(CompareResult.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", "CompareResult"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("redline");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", "Redline"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "base64Binary"));
        elemField.setMinOccurs(0);
        elemField.setNillable(false);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("summary");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", "Summary"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", ">CompareResult>Summary"));
        elemField.setMinOccurs(0);
        elemField.setNillable(false);
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
