using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsExtentionContentAdditionalInformation
{
    public XmlTagsExtentionContentAdditionalInformation () {}

    [XmlElement(ElementName = "AdditionalInformation", Namespace = XmlTagsNamespace.Sac)]
    public XmlTagsAdditionalInformation XmlTagsAdditionalInformation {get; set;} = new ();
}