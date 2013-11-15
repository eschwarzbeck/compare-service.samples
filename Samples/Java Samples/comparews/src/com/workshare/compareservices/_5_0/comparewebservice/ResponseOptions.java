/**
 * ResponseOptions.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class ResponseOptions implements java.io.Serializable {
    private java.lang.String _value_;
    private static java.util.HashMap _table_ = new java.util.HashMap();

    // Constructor
    protected ResponseOptions(java.lang.String value) {
        _value_ = value;
        _table_.put(_value_,this);
    }

    public static final java.lang.String _Rtf = "Rtf";
    public static final java.lang.String _Xml = "Xml";
    public static final java.lang.String _RtfWithSummary = "RtfWithSummary";
    public static final java.lang.String _Wdf = "Wdf";
    public static final java.lang.String _WdfWithSummary = "WdfWithSummary";
    public static final ResponseOptions Rtf = new ResponseOptions(_Rtf);
    public static final ResponseOptions Xml = new ResponseOptions(_Xml);
    public static final ResponseOptions RtfWithSummary = new ResponseOptions(_RtfWithSummary);
    public static final ResponseOptions Wdf = new ResponseOptions(_Wdf);
    public static final ResponseOptions WdfWithSummary = new ResponseOptions(_WdfWithSummary);
    public java.lang.String getValue() { return _value_;}
    public static ResponseOptions fromValue(java.lang.String value)
          throws java.lang.IllegalArgumentException {
        ResponseOptions enumeration = (ResponseOptions)
            _table_.get(value);
        if (enumeration==null) throw new java.lang.IllegalArgumentException();
        return enumeration;
    }
    public static ResponseOptions fromString(java.lang.String value)
          throws java.lang.IllegalArgumentException {
        return fromValue(value);
    }
    public boolean equals(java.lang.Object obj) {return (obj == this);}
    public int hashCode() { return toString().hashCode();}
    public java.lang.String toString() { return _value_;}
    public java.lang.Object readResolve() throws java.io.ObjectStreamException { return fromValue(_value_);}
    public static org.apache.axis.encoding.Serializer getSerializer(
           java.lang.String mechType, 
           java.lang.Class _javaType,  
           javax.xml.namespace.QName _xmlType) {
        return 
          new org.apache.axis.encoding.ser.EnumSerializer(
            _javaType, _xmlType);
    }
    public static org.apache.axis.encoding.Deserializer getDeserializer(
           java.lang.String mechType, 
           java.lang.Class _javaType,  
           javax.xml.namespace.QName _xmlType) {
        return 
          new org.apache.axis.encoding.ser.EnumDeserializer(
            _javaType, _xmlType);
    }
    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(ResponseOptions.class);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ResponseOptions"));
    }
    /**
     * Return type metadata object
     */
    public static org.apache.axis.description.TypeDesc getTypeDesc() {
        return typeDesc;
    }

}
