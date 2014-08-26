
package com.workshare.compareservices._1_1.comparewebservice;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlSchemaType;
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
 *         &lt;element name="original" type="{http://www.w3.org/2001/XMLSchema}base64Binary" minOccurs="0"/>
 *         &lt;element name="modified" type="{http://www.w3.org/2001/XMLSchema}base64Binary" minOccurs="0"/>
 *         &lt;element name="flags" type="{http://workshare.com/compareservices/1.1/comparewebservice/}CompareResponseFlags"/>
 *         &lt;element name="compareOptions" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
    "original",
    "modified",
    "flags",
    "compareOptions"
})
@XmlRootElement(name = "Execute2")
public class Execute2 {

    protected byte[] original;
    protected byte[] modified;
    @XmlElement(required = true)
    @XmlSchemaType(name = "string")
    protected CompareResponseFlags flags;
    protected String compareOptions;

    /**
     * Gets the value of the original property.
     * 
     * @return
     *     possible object is
     *     byte[]
     */
    public byte[] getOriginal() {
        return original;
    }

    /**
     * Sets the value of the original property.
     * 
     * @param value
     *     allowed object is
     *     byte[]
     */
    public void setOriginal(byte[] value) {
        this.original = value;
    }

    /**
     * Gets the value of the modified property.
     * 
     * @return
     *     possible object is
     *     byte[]
     */
    public byte[] getModified() {
        return modified;
    }

    /**
     * Sets the value of the modified property.
     * 
     * @param value
     *     allowed object is
     *     byte[]
     */
    public void setModified(byte[] value) {
        this.modified = value;
    }

    /**
     * Gets the value of the flags property.
     * 
     * @return
     *     possible object is
     *     {@link CompareResponseFlags }
     *     
     */
    public CompareResponseFlags getFlags() {
        return flags;
    }

    /**
     * Sets the value of the flags property.
     * 
     * @param value
     *     allowed object is
     *     {@link CompareResponseFlags }
     *     
     */
    public void setFlags(CompareResponseFlags value) {
        this.flags = value;
    }

    /**
     * Gets the value of the compareOptions property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCompareOptions() {
        return compareOptions;
    }

    /**
     * Sets the value of the compareOptions property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCompareOptions(String value) {
        this.compareOptions = value;
    }

}
