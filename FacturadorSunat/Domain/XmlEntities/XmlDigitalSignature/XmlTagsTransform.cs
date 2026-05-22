using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsTransform
{
    [XmlAttribute(AttributeName = "Algorithm")]
    public String Algorithm {get; set;} = String.Empty;
}