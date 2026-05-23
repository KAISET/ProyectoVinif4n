using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;
public class XmlTagsSignatureDeclaration
{
    [XmlElement(ElementName = "ID", Namespace = XmlTagsNamespace.Cbc)]
    public String? ID {get; set;} = String.Empty;

    [XmlElement(ElementName = "SignatoryParty ", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsSignatoryParty? SignatoryParty {get; set;} = new();

    [XmlElement(ElementName = "DigitalSignatureAttachment ", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsDigitalSignatureAttachment? DigitalSignatureAttachment {get; set;} = new();

}