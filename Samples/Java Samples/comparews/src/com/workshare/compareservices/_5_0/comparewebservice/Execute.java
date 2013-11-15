/**
 * Execute.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class Execute  implements java.io.Serializable {
    private byte[] originalData;

    private byte[] modifiedData;

    private com.workshare.compareservices._5_0.comparewebservice.ResponseOptions responseOption;

    private java.lang.String compareOptions;

    public Execute() {
    }

    public Execute(
           byte[] originalData,
           byte[] modifiedData,
           com.workshare.compareservices._5_0.comparewebservice.ResponseOptions responseOption,
           java.lang.String compareOptions) {
           this.originalData = originalData;
           this.modifiedData = modifiedData;
           this.responseOption = responseOption;
           this.compareOptions = compareOptions;
    }


    /**
     * Gets the originalData value for this Execute.
     * 
     * @return originalData
     */
    public byte[] getOriginalData() {
        return originalData;
    }


    /**
     * Sets the originalData value for this Execute.
     * 
     * @param originalData
     */
    public void setOriginalData(byte[] originalData) {
        this.originalData = originalData;
    }


    /**
     * Gets the modifiedData value for this Execute.
     * 
     * @return modifiedData
     */
    public byte[] getModifiedData() {
        return modifiedData;
    }


    /**
     * Sets the modifiedData value for this Execute.
     * 
     * @param modifiedData
     */
    public void setModifiedData(byte[] modifiedData) {
        this.modifiedData = modifiedData;
    }


    /**
     * Gets the responseOption value for this Execute.
     * 
     * @return responseOption
     */
    public com.workshare.compareservices._5_0.comparewebservice.ResponseOptions getResponseOption() {
        return responseOption;
    }


    /**
     * Sets the responseOption value for this Execute.
     * 
     * @param responseOption
     */
    public void setResponseOption(com.workshare.compareservices._5_0.comparewebservice.ResponseOptions responseOption) {
        this.responseOption = responseOption;
    }


    /**
     * Gets the compareOptions value for this Execute.
     * 
     * @return compareOptions
     */
    public java.lang.String getCompareOptions() {
        return compareOptions;
    }


    /**
     * Sets the compareOptions value for this Execute.
     * 
     * @param compareOptions
     */
    public void setCompareOptions(java.lang.String compareOptions) {
        this.compareOptions = compareOptions;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof Execute)) return false;
        Execute other = (Execute) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.originalData==null && other.getOriginalData()==null) || 
             (this.originalData!=null &&
              java.util.Arrays.equals(this.originalData, other.getOriginalData()))) &&
            ((this.modifiedData==null && other.getModifiedData()==null) || 
             (this.modifiedData!=null &&
              java.util.Arrays.equals(this.modifiedData, other.getModifiedData()))) &&
            ((this.responseOption==null && other.getResponseOption()==null) || 
             (this.responseOption!=null &&
              this.responseOption.equals(other.getResponseOption()))) &&
            ((this.compareOptions==null && other.getCompareOptions()==null) || 
             (this.compareOptions!=null &&
              this.compareOptions.equals(other.getCompareOptions())));
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
        if (getOriginalData() != null) {
            for (int i=0;
                 i<java.lang.reflect.Array.getLength(getOriginalData());
                 i++) {
                java.lang.Object obj = java.lang.reflect.Array.get(getOriginalData(), i);
                if (obj != null &&
                    !obj.getClass().isArray()) {
                    _hashCode += obj.hashCode();
                }
            }
        }
        if (getModifiedData() != null) {
            for (int i=0;
                 i<java.lang.reflect.Array.getLength(getModifiedData());
                 i++) {
                java.lang.Object obj = java.lang.reflect.Array.get(getModifiedData(), i);
                if (obj != null &&
                    !obj.getClass().isArray()) {
                    _hashCode += obj.hashCode();
                }
            }
        }
        if (getResponseOption() != null) {
            _hashCode += getResponseOption().hashCode();
        }
        if (getCompareOptions() != null) {
            _hashCode += getCompareOptions().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(Execute.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", ">Execute"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("originalData");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "OriginalData"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "base64Binary"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("modifiedData");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ModifiedData"));
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
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("compareOptions");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "CompareOptions"));
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
