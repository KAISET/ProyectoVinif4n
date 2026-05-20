using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

[XmlRoot(ElementName = "Invoice")]
public class XmlTagsInvoice
{
    public XmlTagsInvoice () {}
    
    [XmlElement(ElementName = "UBLExtensions", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsUblExtensions UBLExtensions {get; set;} = new();
}