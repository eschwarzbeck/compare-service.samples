
package com.workshare.compareservices._5_2.comparewebservice;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlElementDecl;
import javax.xml.bind.annotation.XmlRegistry;
import javax.xml.namespace.QName;
import com.workshare.compareservices._5_0.comparewebservice.ChunkedExecuteParams;
import com.workshare.compareservices._5_0.comparewebservice.CompareResults;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.workshare.compareservices._5_2.comparewebservice package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {

    private final static QName _PerformanceResults_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "PerformanceResults");
    private final static QName _BenchmarkResponseBenchmarkResult_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "BenchmarkResult");
    private final static QName _CompareCompareOptions_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "CompareOptions");
    private final static QName _InitialiseFileFileID_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "fileID");
    private final static QName _CompareResponseCompareResult_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "CompareResult");
    private final static QName _CompareExExecuteParams_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "executeParams");
    private final static QName _CompareExResponseCompareExResult_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "CompareExResult");
    private final static QName _AppendFileFileChunk_QNAME = new QName("http://workshare.com/compareservices/5.2/comparewebservice/", "fileChunk");

    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.workshare.compareservices._5_2.comparewebservice
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link CompareResponse }
     * 
     */
    public CompareResponse createCompareResponse() {
        return new CompareResponse();
    }

    /**
     * Create an instance of {@link InitialiseFile }
     * 
     */
    public InitialiseFile createInitialiseFile() {
        return new InitialiseFile();
    }

    /**
     * Create an instance of {@link CompareEx }
     * 
     */
    public CompareEx createCompareEx() {
        return new CompareEx();
    }

    /**
     * Create an instance of {@link CompareExResponse }
     * 
     */
    public CompareExResponse createCompareExResponse() {
        return new CompareExResponse();
    }

    /**
     * Create an instance of {@link PerformanceResults }
     * 
     */
    public PerformanceResults createPerformanceResults() {
        return new PerformanceResults();
    }

    /**
     * Create an instance of {@link AppendFile }
     * 
     */
    public AppendFile createAppendFile() {
        return new AppendFile();
    }

    /**
     * Create an instance of {@link AppendFileResponse }
     * 
     */
    public AppendFileResponse createAppendFileResponse() {
        return new AppendFileResponse();
    }

    /**
     * Create an instance of {@link BenchmarkResponse }
     * 
     */
    public BenchmarkResponse createBenchmarkResponse() {
        return new BenchmarkResponse();
    }

    /**
     * Create an instance of {@link InitialiseFileResponse }
     * 
     */
    public InitialiseFileResponse createInitialiseFileResponse() {
        return new InitialiseFileResponse();
    }

    /**
     * Create an instance of {@link Benchmark }
     * 
     */
    public Benchmark createBenchmark() {
        return new Benchmark();
    }

    /**
     * Create an instance of {@link ReleaseFile }
     * 
     */
    public ReleaseFile createReleaseFile() {
        return new ReleaseFile();
    }

    /**
     * Create an instance of {@link ReleaseAll }
     * 
     */
    public ReleaseAll createReleaseAll() {
        return new ReleaseAll();
    }

    /**
     * Create an instance of {@link Compare }
     * 
     */
    public Compare createCompare() {
        return new Compare();
    }

    /**
     * Create an instance of {@link ReleaseFileResponse }
     * 
     */
    public ReleaseFileResponse createReleaseFileResponse() {
        return new ReleaseFileResponse();
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link PerformanceResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "PerformanceResults")
    public JAXBElement<PerformanceResults> createPerformanceResults(PerformanceResults value) {
        return new JAXBElement<PerformanceResults>(_PerformanceResults_QNAME, PerformanceResults.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link PerformanceResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "BenchmarkResult", scope = BenchmarkResponse.class)
    public JAXBElement<PerformanceResults> createBenchmarkResponseBenchmarkResult(PerformanceResults value) {
        return new JAXBElement<PerformanceResults>(_BenchmarkResponseBenchmarkResult_QNAME, PerformanceResults.class, BenchmarkResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "CompareOptions", scope = Compare.class)
    public JAXBElement<String> createCompareCompareOptions(String value) {
        return new JAXBElement<String>(_CompareCompareOptions_QNAME, String.class, Compare.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "CompareOptions", scope = Benchmark.class)
    public JAXBElement<String> createBenchmarkCompareOptions(String value) {
        return new JAXBElement<String>(_CompareCompareOptions_QNAME, String.class, Benchmark.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "fileID", scope = InitialiseFile.class)
    public JAXBElement<String> createInitialiseFileFileID(String value) {
        return new JAXBElement<String>(_InitialiseFileFileID_QNAME, String.class, InitialiseFile.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link CompareResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "CompareResult", scope = CompareResponse.class)
    public JAXBElement<CompareResults> createCompareResponseCompareResult(CompareResults value) {
        return new JAXBElement<CompareResults>(_CompareResponseCompareResult_QNAME, CompareResults.class, CompareResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link ChunkedExecuteParams }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "executeParams", scope = CompareEx.class)
    public JAXBElement<ChunkedExecuteParams> createCompareExExecuteParams(ChunkedExecuteParams value) {
        return new JAXBElement<ChunkedExecuteParams>(_CompareExExecuteParams_QNAME, ChunkedExecuteParams.class, CompareEx.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link CompareResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "CompareExResult", scope = CompareExResponse.class)
    public JAXBElement<CompareResults> createCompareExResponseCompareExResult(CompareResults value) {
        return new JAXBElement<CompareResults>(_CompareExResponseCompareExResult_QNAME, CompareResults.class, CompareExResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link byte[]}{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.2/comparewebservice/", name = "fileChunk", scope = AppendFile.class)
    public JAXBElement<byte[]> createAppendFileFileChunk(byte[] value) {
        return new JAXBElement<byte[]>(_AppendFileFileChunk_QNAME, byte[].class, AppendFile.class, ((byte[]) value));
    }

}
