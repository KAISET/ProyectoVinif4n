using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlDigitalSignature;

public class XmlTagsSignature
{
    [XmlAttribute(AttributeName = "Id")]
    public String Id {get; set;} = String.Empty;

    [XmlElement(ElementName = "SignedInfo", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsSignedInfo SignedInfo {get; set;} = new();

    [XmlElement(ElementName = "SignatureValue", Namespace = XmlTagsNamespace.Ds)]
    public String SignatureValue {get; set;} = String.Empty;

    [XmlElement(ElementName = "KeyInfo", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsKeyInfo KeyInfo {get; set;} = new();
}