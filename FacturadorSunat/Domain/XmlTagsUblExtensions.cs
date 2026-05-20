using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensions
{
    public XmlTagsUblExtensions () {}
    
    [XmlElement(ElementName = "UBLExtension", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsUBLExtensionXMLDSIG UBLExtensionXMLDSIG {get; set;} = new();

    [XmlElement(ElementName = "UBLExtension", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsExtentionContentAdditionalInformation UBLExtensionAdditionalInformation {get; set;} = new();
}