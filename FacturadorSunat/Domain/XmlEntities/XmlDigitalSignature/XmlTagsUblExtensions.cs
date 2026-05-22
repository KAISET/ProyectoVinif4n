using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensions
{
    public XmlTagsUblExtensions () {}

    [XmlElement(ElementName = "UBLExtension", Namespace = XmlTagsNamespace.Ext)]
    public List<XmlTagsUblExtensionItems> UBLExtensionsItems {get; set;} = new();
    
}