using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;
public class XmlTagsExternalReference
{
    [XmlElement(ElementName = "URI", Namespace = XmlTagsNamespace.Cbc)]
    public String? Uri {get; set;} = String.Empty;
}