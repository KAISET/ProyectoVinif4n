using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsKeyInfo
{
    [XmlElement(ElementName ="X509Data", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsX509Data X509Data {get; set;} = new();
}