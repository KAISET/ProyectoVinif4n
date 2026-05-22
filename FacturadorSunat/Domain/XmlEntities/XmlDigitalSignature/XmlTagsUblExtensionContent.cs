using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensionContent
{
    public XmlTagsUblExtensionContent () {}
    
    [XmlElement(ElementName = "AdditionalInformation", Namespace = XmlTagsNamespace.Sac)]
    public XmlTagsAdditionalInformation? XmlUblAdditionalInformation { get; set; }

    [XmlElement(ElementName = "Signature", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsSignature? XmlUblSignature { get; set; }
}