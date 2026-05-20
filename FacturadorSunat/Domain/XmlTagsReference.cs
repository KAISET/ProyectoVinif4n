using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsReference
{
    [XmlAttribute(AttributeName = "URI")]
    public String Uri {get; set;} = String.Empty;

    [XmlElement(ElementName = "Transforms", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsTransforms Transforms {get; set;} = new();

    [XmlElement(ElementName = "DigestMethod", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsDigestMethod DigestMethod {get; set;} = new();

    [XmlElement(ElementName = "DigestValue", Namespace = XmlTagsNamespace.Ds)]
    public String DigestValue {get; set;} = String.Empty;
}