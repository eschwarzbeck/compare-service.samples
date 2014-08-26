
package com.workshare.compareservices._5_0.comparewebservice;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlEnumValue;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for ResponseOptions.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * &lt;simpleType name="ResponseOptions">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="Rtf"/>
 *     &lt;enumeration value="Xml"/>
 *     &lt;enumeration value="RtfWithSummary"/>
 *     &lt;enumeration value="Wdf"/>
 *     &lt;enumeration value="WdfWithSummary"/>
 *     &lt;enumeration value="Doc"/>
 *     &lt;enumeration value="DocWithSummary"/>
 *     &lt;enumeration value="DocX"/>
 *     &lt;enumeration value="DocXWithSummary"/>
 *     &lt;enumeration value="Pdf"/>
 *     &lt;enumeration value="PdfWithSummary"/>
 *     &lt;enumeration value="RedlinMl"/>
 *     &lt;enumeration value="RedlinMlAndRtf"/>
 *     &lt;enumeration value="RedlinMlAndSummary"/>
 *     &lt;enumeration value="RedlinMlAndRtfAndSummary"/>
 *     &lt;enumeration value="RedlinMlAndDoc"/>
 *     &lt;enumeration value="RedlinMlAndDocAndSummary"/>
 *     &lt;enumeration value="RedlinMlAndDocX"/>
 *     &lt;enumeration value="RedlinMlAndDocXAndSummary"/>
 *     &lt;enumeration value="RedlinMlAndPdf"/>
 *     &lt;enumeration value="RedlinMlAndPdfAndSummary"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "ResponseOptions")
@XmlEnum
public enum ResponseOptions {

    @XmlEnumValue("Rtf")
    RTF("Rtf"),
    @XmlEnumValue("Xml")
    XML("Xml"),
    @XmlEnumValue("RtfWithSummary")
    RTF_WITH_SUMMARY("RtfWithSummary"),
    @XmlEnumValue("Wdf")
    WDF("Wdf"),
    @XmlEnumValue("WdfWithSummary")
    WDF_WITH_SUMMARY("WdfWithSummary"),
    @XmlEnumValue("Doc")
    DOC("Doc"),
    @XmlEnumValue("DocWithSummary")
    DOC_WITH_SUMMARY("DocWithSummary"),
    @XmlEnumValue("DocX")
    DOC_X("DocX"),
    @XmlEnumValue("DocXWithSummary")
    DOC_X_WITH_SUMMARY("DocXWithSummary"),
    @XmlEnumValue("Pdf")
    PDF("Pdf"),
    @XmlEnumValue("PdfWithSummary")
    PDF_WITH_SUMMARY("PdfWithSummary"),
    @XmlEnumValue("RedlinMl")
    REDLIN_ML("RedlinMl"),
    @XmlEnumValue("RedlinMlAndRtf")
    REDLIN_ML_AND_RTF("RedlinMlAndRtf"),
    @XmlEnumValue("RedlinMlAndSummary")
    REDLIN_ML_AND_SUMMARY("RedlinMlAndSummary"),
    @XmlEnumValue("RedlinMlAndRtfAndSummary")
    REDLIN_ML_AND_RTF_AND_SUMMARY("RedlinMlAndRtfAndSummary"),
    @XmlEnumValue("RedlinMlAndDoc")
    REDLIN_ML_AND_DOC("RedlinMlAndDoc"),
    @XmlEnumValue("RedlinMlAndDocAndSummary")
    REDLIN_ML_AND_DOC_AND_SUMMARY("RedlinMlAndDocAndSummary"),
    @XmlEnumValue("RedlinMlAndDocX")
    REDLIN_ML_AND_DOC_X("RedlinMlAndDocX"),
    @XmlEnumValue("RedlinMlAndDocXAndSummary")
    REDLIN_ML_AND_DOC_X_AND_SUMMARY("RedlinMlAndDocXAndSummary"),
    @XmlEnumValue("RedlinMlAndPdf")
    REDLIN_ML_AND_PDF("RedlinMlAndPdf"),
    @XmlEnumValue("RedlinMlAndPdfAndSummary")
    REDLIN_ML_AND_PDF_AND_SUMMARY("RedlinMlAndPdfAndSummary");
    private final String value;

    ResponseOptions(String v) {
        value = v;
    }

    public String value() {
        return value;
    }

    public static ResponseOptions fromValue(String v) {
        for (ResponseOptions c: ResponseOptions.values()) {
            if (c.value.equals(v)) {
                return c;
            }
        }
        throw new IllegalArgumentException(v);
    }

}
