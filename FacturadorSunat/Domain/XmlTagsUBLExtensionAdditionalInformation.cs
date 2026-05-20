using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUBLExtensionAdditionalInformation
{
    public XmlTagsUBLExtensionAdditionalInformation() {}

    [XmlElement(ElementName = "ExtensionContent", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsExtentionContentAdditionalInformation AdditionalInformationExtentionContent {get; set;} = new ();
}