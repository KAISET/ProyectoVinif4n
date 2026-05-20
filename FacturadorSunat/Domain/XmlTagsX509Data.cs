using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsX509Data
{
    [XmlElement(ElementName ="X509Certificate", Namespace = "XmlTagsNamespace.Ds")]
    public String X509Certificate   {get; set;} = String.Empty;
}