
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
 *         &lt;element name="execParams" type="{http://workshare.com/compareservices/5.0/comparewebservice/}ExecuteParams" minOccurs="0"/>
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
    "execParams"
})
@XmlRootElement(name = "ExecuteEx")
public class ExecuteEx {

    @XmlElementRef(name = "execParams", namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<ExecuteParams> execParams;

    /**
     * Gets the value of the execParams property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link ExecuteParams }{@code >}
     *     
     */
    public JAXBElement<ExecuteParams> getExecParams() {
        return execParams;
    }

    /**
     * Sets the value of the execParams property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link ExecuteParams }{@code >}
     *     
     */
    public void setExecParams(JAXBElement<ExecuteParams> value) {
        this.execParams = value;
    }

}
