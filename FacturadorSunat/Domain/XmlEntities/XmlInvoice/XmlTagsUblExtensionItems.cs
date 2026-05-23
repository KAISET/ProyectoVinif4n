using System.Xml.Serialization;
using FacturadorSunat.Domain.XmlEntities.XmlAdditionalDocumentReference;
using FacturadorSunat.Domain.XmlEntities.XmlDespatchDocumentReference;
using FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;

namespace FacturadorSunat.Domain.XmlEntities.XmlInvoice;

public class XmlTagsUblExtensionItems
{
    public XmlTagsUblExtensionItems () {}
    
    [XmlElement(ElementName = "ExtensionContent", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsUblExtensionContent? ExtensionContent { get; set; } = new();

    [XmlElement(ElementName = "UBLVersionId", Namespace = XmlTagsNamespace.Cbc)]
    public String? UBLVersionId {get; set;} = String.Empty;

    [XmlElement(ElementName = "CustomizationId", Namespace = XmlTagsNamespace.Cbc)]
    public String? CustomizationId {get; set;} = String.Empty;

    [XmlElement(ElementName = "Id", Namespace = XmlTagsNamespace.Cbc)]
    public String? Id {get; set;} = String.Empty;

    [XmlElement(ElementName = "IssueDate", Namespace = XmlTagsNamespace.Cbc)]
    public String? IssueDate {get; set;} = String.Empty;

    [XmlElement(ElementName = "InvoiceTypeCode", Namespace = XmlTagsNamespace.Cbc)]
    public String? InvoiceTypeCode {get; set;} = String.Empty;

    [XmlElement(ElementName = "DocumentaryCode", Namespace = XmlTagsNamespace.Cbc)]
    public String? DocumentaryCode {get; set;} = String.Empty;

    [XmlElement(ElementName = "DespatchDocumentReference", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsDespatchDocumentReference? DespatchDocumentReference {get; set;} = new();

    [XmlElement(ElementName = "AdditionalDocumentReference", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsAdditionalDocumentReference? AdditionalDocumentReference {get; set;} = new();

    [XmlElement(ElementName = "Signature", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsSignatureDeclaration? SignatureDeclaration {get; set;} = new();
    
}