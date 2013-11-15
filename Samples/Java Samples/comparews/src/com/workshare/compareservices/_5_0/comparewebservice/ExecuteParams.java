/**
 * ExecuteParams.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class ExecuteParams  implements java.io.Serializable {
    private java.lang.String compareOptions;

    private byte[] modified;

    private byte[] original;

    private com.workshare.compareservices._5_0.comparewebservice.ResponseOptions responseOption;

    public ExecuteParams() {
    }

    public ExecuteParams(
           java.lang.String compareOptions,
           byte[] modified,
           byte[] original,
           com.workshare.compareservices._5_0.comparewebservice.ResponseOptions responseOption) {
           this.compareOptions = compareOptions;
           this.modified = modified;
           this.original = original;
           this.responseOption = responseOption;
    }


    /**
     * Gets the compareOptions value for this ExecuteParams.
     * 
     * @return compareOptions
     */
    public java.lang.String getCompareOptions() {
        return compareOptions;
    }


    /**
     * Sets the compareOptions value for this ExecuteParams.
     * 
     * @param compareOptions
     */
    public void setCompareOptions(java.lang.String compareOptions) {
        this.compareOptions = compareOptions;
    }


    /**
     * Gets the modified value for this ExecuteParams.
     * 
     * @return modified
     */
    public byte[] getModified() {
        return modified;
    }


    /**
     * Sets the modified value for this ExecuteParams.
     * 
     * @param modified
     */
    public void setModified(byte[] modified) {
        this.modified = modified;
    }


    /**
     * Gets the original value for this ExecuteParams.
     * 
     * @return original
     */
    public byte[] getOriginal() {
        return original;
    }


    /**
     * Sets the original value for this ExecuteParams.
     * 
     * @param original
     */
    public void setOriginal(byte[] original) {
        this.original = original;
    }


    /**
     * Gets the responseOption value for this ExecuteParams.
     * 
     * @return responseOption
     */
    public com.workshare.compareservices._5_0.comparewebservice.ResponseOptions getResponseOption() {
        return responseOption;
    }


    /**
     * Sets the responseOption value for this ExecuteParams.
     * 
     * @param responseOption
     */
    public void setResponseOption(com.workshare.compareservices._5_0.comparewebservice.ResponseOptions responseOption) {
        this.responseOption = responseOption;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof ExecuteParams)) return false;
        ExecuteParams other = (ExecuteParams) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.compareOptions==null && other.getCompareOptions()==null) || 
             (this.compareOptions!=null &&
              this.compareOptions.equals(other.getCompareOptions()))) &&
            ((this.modified==null && other.getModified()==null) || 
             (this.modified!=null &&
              java.util.Arrays.equals(this.modified, other.getModified()))) &&
            ((this.original==null && other.getOriginal()==null) || 
             (this.original!=null &&
              java.util.Arrays.equals(this.original, other.getOriginal()))) &&
            ((this.responseOption==null && other.getResponseOption()==null) || 
             (this.responseOption!=null &&
              this.responseOption.equals(other.getResponseOption())));
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
        if (getCompareOptions() != null) {
            _hashCode += getCompareOptions().hashCode();
        }
        if (getModified() != null) {
            for (int i=0;
                 i<java.lang.reflect.Array.getLength(getModified());
                 i++) {
                java.lang.Object obj = java.lang.reflect.Array.get(getModified(), i);
                if (obj != null &&
                    !obj.getClass().isArray()) {
                    _hashCode += obj.hashCode();
                }
            }
        }
        if (getOriginal() != null) {
            for (int i=0;
                 i<java.lang.reflect.Array.getLength(getOriginal());
                 i++) {
                java.lang.Object obj = java.lang.reflect.Array.get(getOriginal(), i);
                if (obj != null &&
                    !obj.getClass().isArray()) {
                    _hashCode += obj.hashCode();
                }
            }
        }
        if (getResponseOption() != null) {
            _hashCode += getResponseOption().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(ExecuteParams.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ExecuteParams"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("compareOptions");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "CompareOptions"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("modified");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "Modified"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "base64Binary"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("original");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "Original"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "base64Binary"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("responseOption");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ResponseOption"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ResponseOptions"));
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
