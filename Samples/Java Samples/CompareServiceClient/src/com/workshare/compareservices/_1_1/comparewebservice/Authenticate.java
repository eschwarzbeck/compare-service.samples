
package com.workshare.compareservices._1_1.comparewebservice;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
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
 *         &lt;element name="sDomain" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="sUser" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="sPass" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
    "sDomain",
    "sUser",
    "sPass"
})
@XmlRootElement(name = "Authenticate")
public class Authenticate {

    protected String sDomain;
    protected String sUser;
    protected String sPass;

    /**
     * Gets the value of the sDomain property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSDomain() {
        return sDomain;
    }

    /**
     * Sets the value of the sDomain property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSDomain(String value) {
        this.sDomain = value;
    }

    /**
     * Gets the value of the sUser property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSUser() {
        return sUser;
    }

    /**
     * Sets the value of the sUser property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSUser(String value) {
        this.sUser = value;
    }

    /**
     * Gets the value of the sPass property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSPass() {
        return sPass;
    }

    /**
     * Sets the value of the sPass property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSPass(String value) {
        this.sPass = value;
    }

}
