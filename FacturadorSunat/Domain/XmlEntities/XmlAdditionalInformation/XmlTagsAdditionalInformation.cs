using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;

public class XmlTagsAdditionalInformation
{
    [XmlElement(ElementName = "AdditionalMonetaryTotal", Namespace = XmlTagsNamespace.Sac)]
    public XmlTagsAdditionalMonetaryTotal? AdditionalMonetaryTotal {get; set;} = new();

    [XmlElement(ElementName = "AdditionalProperty", Namespace = XmlTagsNamespace.Sac)]
    public XmlTagsAdditionalProperty? AdditionalProperty {get; set;} = new();
}