
package com.workshare.compareservices._1_1.comparewebservice;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlEnumValue;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for CompareResponseFlags.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * &lt;simpleType name="CompareResponseFlags">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="Rtf"/>
 *     &lt;enumeration value="Xml"/>
 *     &lt;enumeration value="Both"/>
 *     &lt;enumeration value="DocX"/>
 *     &lt;enumeration value="Pdf"/>
 *     &lt;enumeration value="DocXWithXml"/>
 *     &lt;enumeration value="PdfWithXml"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "CompareResponseFlags")
@XmlEnum
public enum CompareResponseFlags {

    @XmlEnumValue("Rtf")
    RTF("Rtf"),
    @XmlEnumValue("Xml")
    XML("Xml"),
    @XmlEnumValue("Both")
    BOTH("Both"),
    @XmlEnumValue("DocX")
    DOC_X("DocX"),
    @XmlEnumValue("Pdf")
    PDF("Pdf"),
    @XmlEnumValue("DocXWithXml")
    DOC_X_WITH_XML("DocXWithXml"),
    @XmlEnumValue("PdfWithXml")
    PDF_WITH_XML("PdfWithXml");
    private final String value;

    CompareResponseFlags(String v) {
        value = v;
    }

    public String value() {
        return value;
    }

    public static CompareResponseFlags fromValue(String v) {
        for (CompareResponseFlags c: CompareResponseFlags.values()) {
            if (c.value.equals(v)) {
                return c;
            }
        }
        throw new IllegalArgumentException(v);
    }

}
