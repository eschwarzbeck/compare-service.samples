
package com.workshare.compareservices._5_2.comparewebservice;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;
import javax.xml.datatype.Duration;


/**
 * <p>Java class for PerformanceResults complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="PerformanceResults">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="ComparisonTime" type="{http://schemas.microsoft.com/2003/10/Serialization/}duration" minOccurs="0"/>
 *         &lt;element name="ModifiedConversionTime" type="{http://schemas.microsoft.com/2003/10/Serialization/}duration" minOccurs="0"/>
 *         &lt;element name="ModifiedPreProcessingTime" type="{http://schemas.microsoft.com/2003/10/Serialization/}duration" minOccurs="0"/>
 *         &lt;element name="OriginalConversionTime" type="{http://schemas.microsoft.com/2003/10/Serialization/}duration" minOccurs="0"/>
 *         &lt;element name="OriginalPreProcessingTime" type="{http://schemas.microsoft.com/2003/10/Serialization/}duration" minOccurs="0"/>
 *         &lt;element name="ResultsProcessingTime" type="{http://schemas.microsoft.com/2003/10/Serialization/}duration" minOccurs="0"/>
 *         &lt;element name="TotalExecutionTime" type="{http://schemas.microsoft.com/2003/10/Serialization/}duration" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "PerformanceResults", propOrder = {
    "comparisonTime",
    "modifiedConversionTime",
    "modifiedPreProcessingTime",
    "originalConversionTime",
    "originalPreProcessingTime",
    "resultsProcessingTime",
    "totalExecutionTime"
})
public class PerformanceResults {

    @XmlElement(name = "ComparisonTime")
    protected Duration comparisonTime;
    @XmlElement(name = "ModifiedConversionTime")
    protected Duration modifiedConversionTime;
    @XmlElement(name = "ModifiedPreProcessingTime")
    protected Duration modifiedPreProcessingTime;
    @XmlElement(name = "OriginalConversionTime")
    protected Duration originalConversionTime;
    @XmlElement(name = "OriginalPreProcessingTime")
    protected Duration originalPreProcessingTime;
    @XmlElement(name = "ResultsProcessingTime")
    protected Duration resultsProcessingTime;
    @XmlElement(name = "TotalExecutionTime")
    protected Duration totalExecutionTime;

    /**
     * Gets the value of the comparisonTime property.
     * 
     * @return
     *     possible object is
     *     {@link Duration }
     *     
     */
    public Duration getComparisonTime() {
        return comparisonTime;
    }

    /**
     * Sets the value of the comparisonTime property.
     * 
     * @param value
     *     allowed object is
     *     {@link Duration }
     *     
     */
    public void setComparisonTime(Duration value) {
        this.comparisonTime = value;
    }

    /**
     * Gets the value of the modifiedConversionTime property.
     * 
     * @return
     *     possible object is
     *     {@link Duration }
     *     
     */
    public Duration getModifiedConversionTime() {
        return modifiedConversionTime;
    }

    /**
     * Sets the value of the modifiedConversionTime property.
     * 
     * @param value
     *     allowed object is
     *     {@link Duration }
     *     
     */
    public void setModifiedConversionTime(Duration value) {
        this.modifiedConversionTime = value;
    }

    /**
     * Gets the value of the modifiedPreProcessingTime property.
     * 
     * @return
     *     possible object is
     *     {@link Duration }
     *     
     */
    public Duration getModifiedPreProcessingTime() {
        return modifiedPreProcessingTime;
    }

    /**
     * Sets the value of the modifiedPreProcessingTime property.
     * 
     * @param value
     *     allowed object is
     *     {@link Duration }
     *     
     */
    public void setModifiedPreProcessingTime(Duration value) {
        this.modifiedPreProcessingTime = value;
    }

    /**
     * Gets the value of the originalConversionTime property.
     * 
     * @return
     *     possible object is
     *     {@link Duration }
     *     
     */
    public Duration getOriginalConversionTime() {
        return originalConversionTime;
    }

    /**
     * Sets the value of the originalConversionTime property.
     * 
     * @param value
     *     allowed object is
     *     {@link Duration }
     *     
     */
    public void setOriginalConversionTime(Duration value) {
        this.originalConversionTime = value;
    }

    /**
     * Gets the value of the originalPreProcessingTime property.
     * 
     * @return
     *     possible object is
     *     {@link Duration }
     *     
     */
    public Duration getOriginalPreProcessingTime() {
        return originalPreProcessingTime;
    }

    /**
     * Sets the value of the originalPreProcessingTime property.
     * 
     * @param value
     *     allowed object is
     *     {@link Duration }
     *     
     */
    public void setOriginalPreProcessingTime(Duration value) {
        this.originalPreProcessingTime = value;
    }

    /**
     * Gets the value of the resultsProcessingTime property.
     * 
     * @return
     *     possible object is
     *     {@link Duration }
     *     
     */
    public Duration getResultsProcessingTime() {
        return resultsProcessingTime;
    }

    /**
     * Sets the value of the resultsProcessingTime property.
     * 
     * @param value
     *     allowed object is
     *     {@link Duration }
     *     
     */
    public void setResultsProcessingTime(Duration value) {
        this.resultsProcessingTime = value;
    }

    /**
     * Gets the value of the totalExecutionTime property.
     * 
     * @return
     *     possible object is
     *     {@link Duration }
     *     
     */
    public Duration getTotalExecutionTime() {
        return totalExecutionTime;
    }

    /**
     * Sets the value of the totalExecutionTime property.
     * 
     * @param value
     *     allowed object is
     *     {@link Duration }
     *     
     */
    public void setTotalExecutionTime(Duration value) {
        this.totalExecutionTime = value;
    }

}
