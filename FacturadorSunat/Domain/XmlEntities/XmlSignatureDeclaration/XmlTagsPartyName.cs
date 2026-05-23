using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;
public class XmlTagsPartyName
{
    [XmlElement(ElementName = "Name", Namespace = XmlTagsNamespace.Cbc)]
    public String? Name {get; set;} = String.Empty;
}