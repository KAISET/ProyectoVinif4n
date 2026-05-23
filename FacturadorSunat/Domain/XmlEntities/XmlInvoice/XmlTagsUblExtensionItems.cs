using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlInvoice;

public class XmlTagsUblExtensionItems
{
    public XmlTagsUblExtensionItems () {}
    
    [XmlElement(ElementName = "ExtensionContent", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsUblExtensionContent? ExtensionContent { get; set; } = new();
}