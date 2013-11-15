/**
 * Authenticate.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package com.workshare.compareservices._5_0.comparewebservice;

public class Authenticate  implements java.io.Serializable {
    private java.lang.String sRealm;

    private java.lang.String sUser;

    private java.lang.String sPassword;

    public Authenticate() {
    }

    public Authenticate(
           java.lang.String sRealm,
           java.lang.String sUser,
           java.lang.String sPassword) {
           this.sRealm = sRealm;
           this.sUser = sUser;
           this.sPassword = sPassword;
    }


    /**
     * Gets the sRealm value for this Authenticate.
     * 
     * @return sRealm
     */
    public java.lang.String getSRealm() {
        return sRealm;
    }


    /**
     * Sets the sRealm value for this Authenticate.
     * 
     * @param sRealm
     */
    public void setSRealm(java.lang.String sRealm) {
        this.sRealm = sRealm;
    }


    /**
     * Gets the sUser value for this Authenticate.
     * 
     * @return sUser
     */
    public java.lang.String getSUser() {
        return sUser;
    }


    /**
     * Sets the sUser value for this Authenticate.
     * 
     * @param sUser
     */
    public void setSUser(java.lang.String sUser) {
        this.sUser = sUser;
    }


    /**
     * Gets the sPassword value for this Authenticate.
     * 
     * @return sPassword
     */
    public java.lang.String getSPassword() {
        return sPassword;
    }


    /**
     * Sets the sPassword value for this Authenticate.
     * 
     * @param sPassword
     */
    public void setSPassword(java.lang.String sPassword) {
        this.sPassword = sPassword;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof Authenticate)) return false;
        Authenticate other = (Authenticate) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.sRealm==null && other.getSRealm()==null) || 
             (this.sRealm!=null &&
              this.sRealm.equals(other.getSRealm()))) &&
            ((this.sUser==null && other.getSUser()==null) || 
             (this.sUser!=null &&
              this.sUser.equals(other.getSUser()))) &&
            ((this.sPassword==null && other.getSPassword()==null) || 
             (this.sPassword!=null &&
              this.sPassword.equals(other.getSPassword())));
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
        if (getSRealm() != null) {
            _hashCode += getSRealm().hashCode();
        }
        if (getSUser() != null) {
            _hashCode += getSUser().hashCode();
        }
        if (getSPassword() != null) {
            _hashCode += getSPassword().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(Authenticate.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", ">Authenticate"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("SRealm");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "sRealm"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("SUser");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "sUser"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("SPassword");
        elemField.setXmlName(new javax.xml.namespace.QName("http://workshare.com/compareservices/5.0/comparewebservice/", "sPassword"));
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
