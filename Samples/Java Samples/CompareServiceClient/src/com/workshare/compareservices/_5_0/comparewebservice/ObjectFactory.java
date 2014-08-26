
package com.workshare.compareservices._5_0.comparewebservice;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlElementDecl;
import javax.xml.bind.annotation.XmlRegistry;
import javax.xml.namespace.QName;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.workshare.compareservices._5_0.comparewebservice package. 
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

    private final static QName _ChunkedExecuteParams_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ChunkedExecuteParams");
    private final static QName _ExecuteParams_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ExecuteParams");
    private final static QName _ResponseOptions_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ResponseOptions");
    private final static QName _DocumentInfo_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "DocumentInfo");
    private final static QName _CompareResults_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "CompareResults");
    private final static QName _PingResponsePingResult_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "PingResult");
    private final static QName _GetVersionResponseGetVersionResult_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "GetVersionResult");
    private final static QName _GetCompositorVersionResponseGetCompositorVersionResult_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "GetCompositorVersionResult");
    private final static QName _CompareResultsRedline_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "Redline");
    private final static QName _CompareResultsSummary_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "Summary");
    private final static QName _CompareResultsRedlineMl_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "RedlineMl");
    private final static QName _ExecuteOriginalData_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "OriginalData");
    private final static QName _ExecuteCompareOptions_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "CompareOptions");
    private final static QName _ExecuteModifiedData_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ModifiedData");
    private final static QName _SetOptionsSetSOptionsSet_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "sOptionsSet");
    private final static QName _ChunkedExecuteParamsModifiedDocumentInfo_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ModifiedDocumentInfo");
    private final static QName _ChunkedExecuteParamsOriginalDocumentInfo_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "OriginalDocumentInfo");
    private final static QName _DocumentInfoDocumentSource_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "DocumentSource");
    private final static QName _DocumentInfoDocumentDescription_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "DocumentDescription");
    private final static QName _ExecuteParamsCompareOptionInfo_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "CompareOptionInfo");
    private final static QName _ExecuteParamsOriginal_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "Original");
    private final static QName _ExecuteParamsModified_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "Modified");
    private final static QName _ExecuteResponseExecuteResult_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ExecuteResult");
    private final static QName _AuthenticateSPassword_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "sPassword");
    private final static QName _AuthenticateSRealm_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "sRealm");
    private final static QName _AuthenticateSUser_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "sUser");
    private final static QName _GetOptionsSetResponseGetOptionsSetResult_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "GetOptionsSetResult");
    private final static QName _ExecuteExResponseExecuteExResult_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "ExecuteExResult");
    private final static QName _ExecuteExExecParams_QNAME = new QName("http://workshare.com/compareservices/5.0/comparewebservice/", "execParams");

    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.workshare.compareservices._5_0.comparewebservice
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link AuthenticateResponse }
     * 
     */
    public AuthenticateResponse createAuthenticateResponse() {
        return new AuthenticateResponse();
    }

    /**
     * Create an instance of {@link ExecuteExResponse }
     * 
     */
    public ExecuteExResponse createExecuteExResponse() {
        return new ExecuteExResponse();
    }

    /**
     * Create an instance of {@link CompareResults }
     * 
     */
    public CompareResults createCompareResults() {
        return new CompareResults();
    }

    /**
     * Create an instance of {@link ExecuteEx }
     * 
     */
    public ExecuteEx createExecuteEx() {
        return new ExecuteEx();
    }

    /**
     * Create an instance of {@link ExecuteParams }
     * 
     */
    public ExecuteParams createExecuteParams() {
        return new ExecuteParams();
    }

    /**
     * Create an instance of {@link Execute }
     * 
     */
    public Execute createExecute() {
        return new Execute();
    }

    /**
     * Create an instance of {@link GetOptionsSetResponse }
     * 
     */
    public GetOptionsSetResponse createGetOptionsSetResponse() {
        return new GetOptionsSetResponse();
    }

    /**
     * Create an instance of {@link ExecuteResponse }
     * 
     */
    public ExecuteResponse createExecuteResponse() {
        return new ExecuteResponse();
    }

    /**
     * Create an instance of {@link PingResponse }
     * 
     */
    public PingResponse createPingResponse() {
        return new PingResponse();
    }

    /**
     * Create an instance of {@link SetOptionsSetResponse }
     * 
     */
    public SetOptionsSetResponse createSetOptionsSetResponse() {
        return new SetOptionsSetResponse();
    }

    /**
     * Create an instance of {@link GetVersionResponse }
     * 
     */
    public GetVersionResponse createGetVersionResponse() {
        return new GetVersionResponse();
    }

    /**
     * Create an instance of {@link GetCompositorVersion }
     * 
     */
    public GetCompositorVersion createGetCompositorVersion() {
        return new GetCompositorVersion();
    }

    /**
     * Create an instance of {@link Authenticate }
     * 
     */
    public Authenticate createAuthenticate() {
        return new Authenticate();
    }

    /**
     * Create an instance of {@link GetVersion }
     * 
     */
    public GetVersion createGetVersion() {
        return new GetVersion();
    }

    /**
     * Create an instance of {@link GetOptionsSet }
     * 
     */
    public GetOptionsSet createGetOptionsSet() {
        return new GetOptionsSet();
    }

    /**
     * Create an instance of {@link SetOptionsSet }
     * 
     */
    public SetOptionsSet createSetOptionsSet() {
        return new SetOptionsSet();
    }

    /**
     * Create an instance of {@link Ping }
     * 
     */
    public Ping createPing() {
        return new Ping();
    }

    /**
     * Create an instance of {@link GetCompositorVersionResponse }
     * 
     */
    public GetCompositorVersionResponse createGetCompositorVersionResponse() {
        return new GetCompositorVersionResponse();
    }

    /**
     * Create an instance of {@link ChunkedExecuteParams }
     * 
     */
    public ChunkedExecuteParams createChunkedExecuteParams() {
        return new ChunkedExecuteParams();
    }

    /**
     * Create an instance of {@link DocumentInfo }
     * 
     */
    public DocumentInfo createDocumentInfo() {
        return new DocumentInfo();
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link ChunkedExecuteParams }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ChunkedExecuteParams")
    public JAXBElement<ChunkedExecuteParams> createChunkedExecuteParams(ChunkedExecuteParams value) {
        return new JAXBElement<ChunkedExecuteParams>(_ChunkedExecuteParams_QNAME, ChunkedExecuteParams.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link ExecuteParams }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ExecuteParams")
    public JAXBElement<ExecuteParams> createExecuteParams(ExecuteParams value) {
        return new JAXBElement<ExecuteParams>(_ExecuteParams_QNAME, ExecuteParams.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link ResponseOptions }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ResponseOptions")
    public JAXBElement<ResponseOptions> createResponseOptions(ResponseOptions value) {
        return new JAXBElement<ResponseOptions>(_ResponseOptions_QNAME, ResponseOptions.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link DocumentInfo }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "DocumentInfo")
    public JAXBElement<DocumentInfo> createDocumentInfo(DocumentInfo value) {
        return new JAXBElement<DocumentInfo>(_DocumentInfo_QNAME, DocumentInfo.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link CompareResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "CompareResults")
    public JAXBElement<CompareResults> createCompareResults(CompareResults value) {
        return new JAXBElement<CompareResults>(_CompareResults_QNAME, CompareResults.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link CompareResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "PingResult", scope = PingResponse.class)
    public JAXBElement<CompareResults> createPingResponsePingResult(CompareResults value) {
        return new JAXBElement<CompareResults>(_PingResponsePingResult_QNAME, CompareResults.class, PingResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "GetVersionResult", scope = GetVersionResponse.class)
    public JAXBElement<String> createGetVersionResponseGetVersionResult(String value) {
        return new JAXBElement<String>(_GetVersionResponseGetVersionResult_QNAME, String.class, GetVersionResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "GetCompositorVersionResult", scope = GetCompositorVersionResponse.class)
    public JAXBElement<String> createGetCompositorVersionResponseGetCompositorVersionResult(String value) {
        return new JAXBElement<String>(_GetCompositorVersionResponseGetCompositorVersionResult_QNAME, String.class, GetCompositorVersionResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link byte[]}{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "Redline", scope = CompareResults.class)
    public JAXBElement<byte[]> createCompareResultsRedline(byte[] value) {
        return new JAXBElement<byte[]>(_CompareResultsRedline_QNAME, byte[].class, CompareResults.class, ((byte[]) value));
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "Summary", scope = CompareResults.class)
    public JAXBElement<String> createCompareResultsSummary(String value) {
        return new JAXBElement<String>(_CompareResultsSummary_QNAME, String.class, CompareResults.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "RedlineMl", scope = CompareResults.class)
    public JAXBElement<String> createCompareResultsRedlineMl(String value) {
        return new JAXBElement<String>(_CompareResultsRedlineMl_QNAME, String.class, CompareResults.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link byte[]}{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "OriginalData", scope = Execute.class)
    public JAXBElement<byte[]> createExecuteOriginalData(byte[] value) {
        return new JAXBElement<byte[]>(_ExecuteOriginalData_QNAME, byte[].class, Execute.class, ((byte[]) value));
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "CompareOptions", scope = Execute.class)
    public JAXBElement<String> createExecuteCompareOptions(String value) {
        return new JAXBElement<String>(_ExecuteCompareOptions_QNAME, String.class, Execute.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link byte[]}{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ModifiedData", scope = Execute.class)
    public JAXBElement<byte[]> createExecuteModifiedData(byte[] value) {
        return new JAXBElement<byte[]>(_ExecuteModifiedData_QNAME, byte[].class, Execute.class, ((byte[]) value));
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "sOptionsSet", scope = SetOptionsSet.class)
    public JAXBElement<String> createSetOptionsSetSOptionsSet(String value) {
        return new JAXBElement<String>(_SetOptionsSetSOptionsSet_QNAME, String.class, SetOptionsSet.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link DocumentInfo }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ModifiedDocumentInfo", scope = ChunkedExecuteParams.class)
    public JAXBElement<DocumentInfo> createChunkedExecuteParamsModifiedDocumentInfo(DocumentInfo value) {
        return new JAXBElement<DocumentInfo>(_ChunkedExecuteParamsModifiedDocumentInfo_QNAME, DocumentInfo.class, ChunkedExecuteParams.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link DocumentInfo }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "OriginalDocumentInfo", scope = ChunkedExecuteParams.class)
    public JAXBElement<DocumentInfo> createChunkedExecuteParamsOriginalDocumentInfo(DocumentInfo value) {
        return new JAXBElement<DocumentInfo>(_ChunkedExecuteParamsOriginalDocumentInfo_QNAME, DocumentInfo.class, ChunkedExecuteParams.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "CompareOptions", scope = ChunkedExecuteParams.class)
    public JAXBElement<String> createChunkedExecuteParamsCompareOptions(String value) {
        return new JAXBElement<String>(_ExecuteCompareOptions_QNAME, String.class, ChunkedExecuteParams.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "DocumentSource", scope = DocumentInfo.class)
    public JAXBElement<String> createDocumentInfoDocumentSource(String value) {
        return new JAXBElement<String>(_DocumentInfoDocumentSource_QNAME, String.class, DocumentInfo.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "DocumentDescription", scope = DocumentInfo.class)
    public JAXBElement<String> createDocumentInfoDocumentDescription(String value) {
        return new JAXBElement<String>(_DocumentInfoDocumentDescription_QNAME, String.class, DocumentInfo.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link DocumentInfo }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ModifiedDocumentInfo", scope = ExecuteParams.class)
    public JAXBElement<DocumentInfo> createExecuteParamsModifiedDocumentInfo(DocumentInfo value) {
        return new JAXBElement<DocumentInfo>(_ChunkedExecuteParamsModifiedDocumentInfo_QNAME, DocumentInfo.class, ExecuteParams.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link DocumentInfo }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "OriginalDocumentInfo", scope = ExecuteParams.class)
    public JAXBElement<DocumentInfo> createExecuteParamsOriginalDocumentInfo(DocumentInfo value) {
        return new JAXBElement<DocumentInfo>(_ChunkedExecuteParamsOriginalDocumentInfo_QNAME, DocumentInfo.class, ExecuteParams.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "CompareOptions", scope = ExecuteParams.class)
    public JAXBElement<String> createExecuteParamsCompareOptions(String value) {
        return new JAXBElement<String>(_ExecuteCompareOptions_QNAME, String.class, ExecuteParams.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "CompareOptionInfo", scope = ExecuteParams.class)
    public JAXBElement<String> createExecuteParamsCompareOptionInfo(String value) {
        return new JAXBElement<String>(_ExecuteParamsCompareOptionInfo_QNAME, String.class, ExecuteParams.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link byte[]}{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "Original", scope = ExecuteParams.class)
    public JAXBElement<byte[]> createExecuteParamsOriginal(byte[] value) {
        return new JAXBElement<byte[]>(_ExecuteParamsOriginal_QNAME, byte[].class, ExecuteParams.class, ((byte[]) value));
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link byte[]}{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "Modified", scope = ExecuteParams.class)
    public JAXBElement<byte[]> createExecuteParamsModified(byte[] value) {
        return new JAXBElement<byte[]>(_ExecuteParamsModified_QNAME, byte[].class, ExecuteParams.class, ((byte[]) value));
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link CompareResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ExecuteResult", scope = ExecuteResponse.class)
    public JAXBElement<CompareResults> createExecuteResponseExecuteResult(CompareResults value) {
        return new JAXBElement<CompareResults>(_ExecuteResponseExecuteResult_QNAME, CompareResults.class, ExecuteResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "sPassword", scope = Authenticate.class)
    public JAXBElement<String> createAuthenticateSPassword(String value) {
        return new JAXBElement<String>(_AuthenticateSPassword_QNAME, String.class, Authenticate.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "sRealm", scope = Authenticate.class)
    public JAXBElement<String> createAuthenticateSRealm(String value) {
        return new JAXBElement<String>(_AuthenticateSRealm_QNAME, String.class, Authenticate.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "sUser", scope = Authenticate.class)
    public JAXBElement<String> createAuthenticateSUser(String value) {
        return new JAXBElement<String>(_AuthenticateSUser_QNAME, String.class, Authenticate.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "GetOptionsSetResult", scope = GetOptionsSetResponse.class)
    public JAXBElement<String> createGetOptionsSetResponseGetOptionsSetResult(String value) {
        return new JAXBElement<String>(_GetOptionsSetResponseGetOptionsSetResult_QNAME, String.class, GetOptionsSetResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link CompareResults }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "ExecuteExResult", scope = ExecuteExResponse.class)
    public JAXBElement<CompareResults> createExecuteExResponseExecuteExResult(CompareResults value) {
        return new JAXBElement<CompareResults>(_ExecuteExResponseExecuteExResult_QNAME, CompareResults.class, ExecuteExResponse.class, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link ExecuteParams }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://workshare.com/compareservices/5.0/comparewebservice/", name = "execParams", scope = ExecuteEx.class)
    public JAXBElement<ExecuteParams> createExecuteExExecParams(ExecuteParams value) {
        return new JAXBElement<ExecuteParams>(_ExecuteExExecParams_QNAME, ExecuteParams.class, ExecuteEx.class, value);
    }

}
