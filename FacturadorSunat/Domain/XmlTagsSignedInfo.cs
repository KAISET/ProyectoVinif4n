using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsSignedInfo
{
    [XmlElement(ElementName = "Reference", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsReference Reference {get; set;} = new();

    [XmlElement(ElementName = "CanonicalizationMethod", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsCanonicalizationMethod CanonicalizationMethod {get; set;} = new();

    [XmlElement(ElementName = "SignatureMethod", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsSignatureMethod SignatureMethod {get; set;} = new();
}