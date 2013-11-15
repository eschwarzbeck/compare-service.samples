/**
 * CompareResponseFlags.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._1_1.comparewebservice;

public class CompareResponseFlags implements java.io.Serializable {
    private java.lang.String _value_;
    private static java.util.HashMap _table_ = new java.util.HashMap();

    // Constructor
    protected CompareResponseFlags(java.lang.String value) {
        _value_ = value;
        _table_.put(_value_,this);
    }

    public static final java.lang.String _Rtf = "Rtf";
    public static final java.lang.String _Xml = "Xml";
    public static final java.lang.String _Both = "Both";
    public static final java.lang.String _DocX = "DocX";
    public static final java.lang.String _Pdf = "Pdf";
    public static final java.lang.String _DocXWithXml = "DocXWithXml";
    public static final java.lang.String _PdfWithXml = "PdfWithXml";
    public static final CompareResponseFlags Rtf = new CompareResponseFlags(_Rtf);
    public static final CompareResponseFlags Xml = new CompareResponseFlags(_Xml);
    public static final CompareResponseFlags Both = new CompareResponseFlags(_Both);
    public static final CompareResponseFlags Pdf = new CompareResponseFlags(_Pdf);
    public static final CompareResponseFlags DocX = new CompareResponseFlags(_DocX);
    public static final CompareResponseFlags DocXWithXml = new CompareResponseFlags(_DocXWithXml);
    public static final CompareResponseFlags PdfWithXml = new CompareResponseFlags(_PdfWithXml);
    public java.lang.String getValue() { return _value_;}
    public static CompareResponseFlags fromValue(java.lang.String value)
          throws java.lang.IllegalArgumentException {
        CompareResponseFlags enumeration = (CompareResponseFlags)
            _table_.get(value);
        if (enumeration==null) throw new java.lang.IllegalArgumentException();
        return enumeration;
    }
    public static CompareResponseFlags fromString(java.lang.String value)
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
        new org.apache.axis.description.TypeDesc(CompareResponseFlags.class);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/1.1/comparewebservice/", "CompareResponseFlags"));
    }
    /**
     * Return type metadata object
     */
    public static org.apache.axis.description.TypeDesc getTypeDesc() {
        return typeDesc;
    }

}
