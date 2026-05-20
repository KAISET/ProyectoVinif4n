using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsAdditionalMonetaryTotal
{
    public XmlTagsAdditionalMonetaryTotal () {}

    [XmlElement(ElementName = "ID", Namespace = XmlTagsNamespace.Cbc)]
    public String ID {get; set;} = String.Empty;

    [XmlElement(ElementName = "Name", Namespace = XmlTagsNamespace.Cbc)]
    public String Name {get; set;} = String.Empty;

    [XmlElement(ElementName = "ReferenceAmount", Namespace = XmlTagsNamespace.Sac)]
    public String ReferenceAmount {get; set;} = String.Empty;

    [XmlElement(ElementName = "PayableAmount", Namespace = XmlTagsNamespace.Cbc)]
    public String PayableAmount {get; set;} = String.Empty;

    [XmlElement(ElementName = "Percent", Namespace = XmlTagsNamespace.Cbc)]
    public String Percent {get; set;} = String.Empty;

    [XmlElement(ElementName = "TotalAmount", Namespace = XmlTagsNamespace.Sac)]
    public String TotalAmount {get; set;} = String.Empty;
}