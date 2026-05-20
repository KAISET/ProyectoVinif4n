using System.Xml.Linq;
using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

[XmlRoot(ElementName = "UBLExtensions", Namespace = "XmlTagsNamespace.Ext")]
public class XmlTagsUblExtensions
{
    [XmlElement(ElementName = "UBLExtension", Namespace = "XmlTagsNamespace.Ext")]
    public XmlTagsUblExtensionContentXMLDSIG UblExtensionXMLDSIG {get; set;} = new();
}