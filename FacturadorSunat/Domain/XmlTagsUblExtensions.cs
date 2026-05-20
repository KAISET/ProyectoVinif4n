using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

[XmlRoot(ElementName = "Invoice")]
public class XmlTagsUblExtensions
{
    public XmlTagsUblExtensions () {}
    
    [XmlElement(ElementName = "UBLExtension", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsUBLExtensionXMLDSIG UBLExtensionXMLDSIG {get; set;} = new();
}