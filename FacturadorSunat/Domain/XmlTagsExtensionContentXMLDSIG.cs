using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensionContentXMLDSIG
{
    [XmlElement(ElementName = "ExtensionContent", Namespace = "XmlTagsNamespace.Ext")]
    public XmlTagsSignature Signature {get; set;} = new();
}