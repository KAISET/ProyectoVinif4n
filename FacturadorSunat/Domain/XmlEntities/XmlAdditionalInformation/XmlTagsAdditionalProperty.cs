using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;

public class XmlTagsAdditionalProperty
{
    public XmlTagsAdditionalProperty () {}

    [XmlElement(ElementName = "ID", Namespace = XmlTagsNamespace.Cbc)]
    public String ID {get; set;} = String.Empty;

    [XmlElement(ElementName = "Name", Namespace = XmlTagsNamespace.Cbc)]
    public String Name {get; set;} = String.Empty;

    [XmlElement(ElementName = "Value", Namespace = XmlTagsNamespace.Cbc)]
    public String Value {get; set;} = String.Empty;
}