using System.Xml.Linq;
using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

[XmlRoot(ElementName = "UBLExtensions", Namespace = XmlTagsNamespace.Ext)]
public class XmlTagsUblExtension
{
    [XmlElement(ElementName = "UBLExtension", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsUblExtensions UblExtensionXMLDSIG {get; set;} = new();
}