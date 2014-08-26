
package com.workshare.compareservices._1_1.comparewebservice;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
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
 *         &lt;element name="Execute2Result" type="{http://workshare.com/compareservices/1.1/comparewebservice/}CompareResult"/>
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
    "execute2Result"
})
@XmlRootElement(name = "Execute2Response")
public class Execute2Response {

    @XmlElement(name = "Execute2Result", required = true)
    protected CompareResult execute2Result;

    /**
     * Gets the value of the execute2Result property.
     * 
     * @return
     *     possible object is
     *     {@link CompareResult }
     *     
     */
    public CompareResult getExecute2Result() {
        return execute2Result;
    }

    /**
     * Sets the value of the execute2Result property.
     * 
     * @param value
     *     allowed object is
     *     {@link CompareResult }
     *     
     */
    public void setExecute2Result(CompareResult value) {
        this.execute2Result = value;
    }

}
