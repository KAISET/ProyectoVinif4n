using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsExtentionContentXMLDSIG
{
    [XmlElement(ElementName = "Signature", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsSignature Signature {get; set;} = new();
}