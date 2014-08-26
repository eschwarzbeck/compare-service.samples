
package com.workshare.compareservices._5_2.comparewebservice;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlSchemaType;
import javax.xml.bind.annotation.XmlType;
import com.workshare.compareservices._5_0.comparewebservice.ResponseOptions;


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
 *         &lt;element name="guidOriginalFile" type="{http://schemas.microsoft.com/2003/10/Serialization/}guid" minOccurs="0"/>
 *         &lt;element name="guidModifiedFile" type="{http://schemas.microsoft.com/2003/10/Serialization/}guid" minOccurs="0"/>
 *         &lt;element name="ResponseOption" type="{http://workshare.com/compareservices/5.0/comparewebservice/}ResponseOptions" minOccurs="0"/>
 *         &lt;element name="CompareOptions" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
    "guidOriginalFile",
    "guidModifiedFile",
    "responseOption",
    "compareOptions"
})
@XmlRootElement(name = "Benchmark")
public class Benchmark {

    protected String guidOriginalFile;
    protected String guidModifiedFile;
    @XmlElement(name = "ResponseOption")
    @XmlSchemaType(name = "string")
    protected ResponseOptions responseOption;
    @XmlElementRef(name = "CompareOptions", namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> compareOptions;

    /**
     * Gets the value of the guidOriginalFile property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getGuidOriginalFile() {
        return guidOriginalFile;
    }

    /**
     * Sets the value of the guidOriginalFile property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setGuidOriginalFile(String value) {
        this.guidOriginalFile = value;
    }

    /**
     * Gets the value of the guidModifiedFile property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getGuidModifiedFile() {
        return guidModifiedFile;
    }

    /**
     * Sets the value of the guidModifiedFile property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setGuidModifiedFile(String value) {
        this.guidModifiedFile = value;
    }

    /**
     * Gets the value of the responseOption property.
     * 
     * @return
     *     possible object is
     *     {@link ResponseOptions }
     *     
     */
    public ResponseOptions getResponseOption() {
        return responseOption;
    }

    /**
     * Sets the value of the responseOption property.
     * 
     * @param value
     *     allowed object is
     *     {@link ResponseOptions }
     *     
     */
    public void setResponseOption(ResponseOptions value) {
        this.responseOption = value;
    }

    /**
     * Gets the value of the compareOptions property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getCompareOptions() {
        return compareOptions;
    }

    /**
     * Sets the value of the compareOptions property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setCompareOptions(JAXBElement<String> value) {
        this.compareOptions = value;
    }

}
