using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;
public class XmlTagsPartyIdentification
{
    [XmlElement(ElementName = "ID", Namespace = XmlTagsNamespace.Cbc)]
    public String? Id {get; set;} = String.Empty;
}