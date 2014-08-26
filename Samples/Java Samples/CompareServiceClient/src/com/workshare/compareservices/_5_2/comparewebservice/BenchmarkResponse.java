
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
 *         &lt;element name="BenchmarkResult" type="{http://workshare.com/compareservices/5.2/comparewebservice/}PerformanceResults" minOccurs="0"/>
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
    "benchmarkResult"
})
@XmlRootElement(name = "BenchmarkResponse")
public class BenchmarkResponse {

    @XmlElementRef(name = "BenchmarkResult", namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", type = JAXBElement.class, required = false)
    protected JAXBElement<PerformanceResults> benchmarkResult;

    /**
     * Gets the value of the benchmarkResult property.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link PerformanceResults }{@code >}
     *     
     */
    public JAXBElement<PerformanceResults> getBenchmarkResult() {
        return benchmarkResult;
    }

    /**
     * Sets the value of the benchmarkResult property.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link PerformanceResults }{@code >}
     *     
     */
    public void setBenchmarkResult(JAXBElement<PerformanceResults> value) {
        this.benchmarkResult = value;
    }

}
