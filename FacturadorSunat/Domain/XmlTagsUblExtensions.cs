using System.Xml.Linq;
using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensions
{
    [XmlElement(ElementName = "ExtensionContent", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsUblExtensionContentXMLDSIG UblExtensionXMLDSIG {get; set;} = new();
}