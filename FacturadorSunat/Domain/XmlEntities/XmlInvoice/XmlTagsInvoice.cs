using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlInvoice;

[XmlRoot(ElementName = "Invoice", Namespace = XmlTagsNamespace.RootInvoice)]
public class XmlTagsInvoice
{
    public XmlTagsInvoice () {}
    
    [XmlElement(ElementName = "UBLExtensions", Namespace = XmlTagsNamespace.Ext)]
    public XmlTagsUblExtensions UBLExtensions {get; set;} = new();
}