using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsSignatureMethod
{
    [XmlAttribute(AttributeName = "Algorithm")]
    public String Algorithm {get; set;} = String.Empty;
}