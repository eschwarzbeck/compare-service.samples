
package com.workshare.compareservices._5_2.comparewebservice;

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
 *         &lt;element name="guidFile" type="{http://schemas.microsoft.com/2003/10/Serialization/}guid" minOccurs="0"/>
 *         &lt;element name="fileChunk" type="{http://www.w3.org/2001/XMLSchema}base64Binary" minOccurs="0"/>
 *         &lt;element name="previousBytesSent" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/>
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
    "guidFile",
    "fileChunk",
    "previousBytesSent"
})
@XmlRootElement(name = "AppendFile")
public class AppendFile {

    protected String guidFile;
    @XmlElementRef(name = "fileChunk", namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<byte[]> fileChunk;
    protected Long previousBytesSent;

    /**
     * Gets the value of the guidFile property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getGuidFile() {
        return guidFile;
    }

    /**
     * Sets the value of the guidFile property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setGuidFile(String value) {
        this.guidFile = value;
    }

    /**
     * Gets the value of the fileChunk property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link byte[]}{@code >}
     *     
     */
    public JAXBElement<byte[]> getFileChunk() {
        return fileChunk;
    }

    /**
     * Sets the value of the fileChunk property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link byte[]}{@code >}
     *     
     */
    public void setFileChunk(JAXBElement<byte[]> value) {
        this.fileChunk = value;
    }

    /**
     * Gets the value of the previousBytesSent property.
     * 
     * @return
     *     possible object is
     *     {@link Long }
     *     
     */
    public Long getPreviousBytesSent() {
        return previousBytesSent;
    }

    /**
     * Sets the value of the previousBytesSent property.
     * 
     * @param value
     *     allowed object is
     *     {@link Long }
     *     
     */
    public void setPreviousBytesSent(Long value) {
        this.previousBytesSent = value;
    }

}
