using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUBLExtensionXMLDSIG
{
    [XmlElement(ElementName = "ExtensionContent", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsExtentionContentXMLDSIG XMLDSIGExtentionContent {get; set;} = new ();
}