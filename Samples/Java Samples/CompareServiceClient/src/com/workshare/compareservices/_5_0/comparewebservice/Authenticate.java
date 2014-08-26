
package com.workshare.compareservices._5_0.comparewebservice;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="sRealm" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="sUser" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="sPassword" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "sRealm",
    "sUser",
    "sPassword"
})
@XmlRootElement(name = "Authenticate")
public class Authenticate {

    @XmlElementRef(name = "sRealm", namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> sRealm;
    @XmlElementRef(name = "sUser", namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> sUser;
    @XmlElementRef(name = "sPassword", namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> sPassword;

    /**
     * Gets the value of the sRealm property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getSRealm() {
        return sRealm;
    }

    /**
     * Sets the value of the sRealm property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setSRealm(JAXBElement<String> value) {
        this.sRealm = value;
    }

    /**
     * Gets the value of the sUser property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getSUser() {
        return sUser;
    }

    /**
     * Sets the value of the sUser property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setSUser(JAXBElement<String> value) {
        this.sUser = value;
    }

    /**
     * Gets the value of the sPassword property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getSPassword() {
        return sPassword;
    }

    /**
     * Sets the value of the sPassword property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setSPassword(JAXBElement<String> value) {
        this.sPassword = value;
    }

}
