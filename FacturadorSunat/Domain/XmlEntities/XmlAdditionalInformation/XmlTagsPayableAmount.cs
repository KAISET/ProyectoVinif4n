using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;

public class XmlTagsPayableAmount
{
    public XmlTagsPayableAmount () {}

    [XmlAttribute(AttributeName = "currencyID")]
    public String CurrencyID { get; set; } = "PEN";

    [XmlText]
    public String? Value { get; set; }
}

