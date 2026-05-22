using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensionContent
{
    public XmlTagsUblExtensionContent () {}
    
    [XmlElement(ElementName = "Signature", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsSignature? XmlUblSignature { get; set; }

    [XmlElement(ElementName = "AdditionalInformation", Namespace = XmlTagsNamespace.Sac)]
    public XmlTagsAdditionalInformation? XmlUblAdditionalInformation { get; set; }
    
}