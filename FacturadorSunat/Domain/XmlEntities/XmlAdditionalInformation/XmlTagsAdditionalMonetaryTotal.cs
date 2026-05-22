using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;

public class XmlTagsAdditionalMonetaryTotal
{
    public XmlTagsAdditionalMonetaryTotal () {}

    [XmlElement(ElementName = "ID", Namespace = XmlTagsNamespace.Cbc)]
    public String? ID {get; set;} = String.Empty;

    [XmlElement(ElementName = "Name", Namespace = XmlTagsNamespace.Cbc)]
    public String? Name {get; set;} = String.Empty;

    [XmlElement(ElementName = "ReferenceAmount", Namespace = XmlTagsNamespace.Sac)]
    public String? ReferenceAmount {get; set;} = String.Empty;

    [XmlElement(ElementName = "PayableAmount", Namespace = XmlTagsNamespace.Cbc)]
    public XmlTagsPayableAmount? PayableAmount {get; set;}

    [XmlElement(ElementName = "Percent", Namespace = XmlTagsNamespace.Cbc)]
    public String? Percent {get; set;} = String.Empty;

    [XmlElement(ElementName = "TotalAmount", Namespace = XmlTagsNamespace.Sac)]
    public String? TotalAmount {get; set;} = String.Empty;
}