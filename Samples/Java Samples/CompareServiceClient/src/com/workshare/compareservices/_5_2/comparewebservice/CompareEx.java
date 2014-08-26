
package com.workshare.compareservices._5_2.comparewebservice;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
import com.workshare.compareservices._5_0.comparewebservice.ChunkedExecuteParams;


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
 *         &lt;element name="executeParams" type="{http://workshare.com/compareservices/5.0/comparewebservice/}ChunkedExecuteParams" minOccurs="0"/>
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
    "executeParams"
})
@XmlRootElement(name = "CompareEx")
public class CompareEx {

    @XmlElementRef(name = "executeParams", namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<ChunkedExecuteParams> executeParams;

    /**
     * Gets the value of the executeParams property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link ChunkedExecuteParams }{@code >}
     *     
     */
    public JAXBElement<ChunkedExecuteParams> getExecuteParams() {
        return executeParams;
    }

    /**
     * Sets the value of the executeParams property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link ChunkedExecuteParams }{@code >}
     *     
     */
    public void setExecuteParams(JAXBElement<ChunkedExecuteParams> value) {
        this.executeParams = value;
    }

}
