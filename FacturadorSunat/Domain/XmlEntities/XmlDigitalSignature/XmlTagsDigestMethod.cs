using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsDigestMethod
{
    [XmlAttribute(AttributeName = "Algorithm")]
    public String Algorithm {get; set;} = String.Empty;
}