using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlDigitalSignatureSpecification;

/// <summary>
/// 
/// </summary>
public class XmlTagsDigitalSignatureSpecification
{
    [XmlAttribute(AttributeName = "Id")]
    public String? Id {get; set;} = String.Empty;

    [XmlElement(ElementName = "SignedInfo", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsSignedInfo? SignedInfo {get; set;} = new();

    [XmlElement(ElementName = "SignatureValue", Namespace = XmlTagsNamespace.Ds)]
    public String? SignatureValue {get; set;} = String.Empty;

    [XmlElement(ElementName = "KeyInfo", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsKeyInfo? KeyInfo {get; set;} = new();
}