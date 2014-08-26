
package com.workshare.compareservices._5_2.comparewebservice;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
import com.workshare.compareservices._5_0.comparewebservice.CompareResults;


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
 *         &lt;element name="CompareExResult" type="{http://workshare.com/compareservices/5.0/comparewebservice/}CompareResults" minOccurs="0"/>
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
    "compareExResult"
})
@XmlRootElement(name = "CompareExResponse")
public class CompareExResponse {

    @XmlElementRef(name = "CompareExResult", namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<CompareResults> compareExResult;

    /**
     * Gets the value of the compareExResult property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link CompareResults }{@code >}
     *     
     */
    public JAXBElement<CompareResults> getCompareExResult() {
        return compareExResult;
    }

    /**
     * Sets the value of the compareExResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link CompareResults }{@code >}
     *     
     */
    public void setCompareExResult(JAXBElement<CompareResults> value) {
        this.compareExResult = value;
    }

}
