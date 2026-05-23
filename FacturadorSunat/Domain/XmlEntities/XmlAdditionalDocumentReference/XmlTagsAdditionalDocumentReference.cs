using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlAdditionalDocumentReference;

public class XmlTagsAdditionalDocumentReference 
{
    public XmlTagsAdditionalDocumentReference () {}
    
    [XmlElement(ElementName = "ID", Namespace = XmlTagsNamespace.Cbc)]
    public String? Id {get; set;} = String.Empty;

    [XmlElement(ElementName = "DocumentTypeCode", Namespace = XmlTagsNamespace.Cbc)]
    public String? DocumentTypeCode {get; set;} = String.Empty;
}