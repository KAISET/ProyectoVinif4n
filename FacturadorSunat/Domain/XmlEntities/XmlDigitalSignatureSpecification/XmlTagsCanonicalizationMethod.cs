using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlDigitalSignatureSpecification;

public class XmlTagsCanonicalizationMethod
{
    [XmlAttribute(AttributeName = "Algorithm")]
    public String? Algorithm {get; set;} = String.Empty;
}