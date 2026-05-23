using System.Xml.Serialization;
using FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;
using FacturadorSunat.Domain.XmlEntities.XmlDigitalSignatureSpecification;

namespace FacturadorSunat.Domain.XmlEntities.XmlInvoice;

public class XmlTagsUblExtensionContent
{
    public XmlTagsUblExtensionContent () {}
    
    [XmlElement(ElementName = "AdditionalInformation", Namespace = XmlTagsNamespace.Sac)]
    public XmlTagsAdditionalInformation? XmlUblAdditionalInformation { get; set; }

    [XmlElement(ElementName = "Signature", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsDigitalSignatureSpecification? XmlUblSignatureSpecification { get; set; }
}