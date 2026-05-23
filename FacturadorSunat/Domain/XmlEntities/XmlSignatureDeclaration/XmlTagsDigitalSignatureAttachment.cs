using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;
public class XmlTagsDigitalSignatureAttachment
{
    [XmlElement(ElementName = "ExternalReference", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsExternalReference? ExternalReference {get; set;} = new();
}