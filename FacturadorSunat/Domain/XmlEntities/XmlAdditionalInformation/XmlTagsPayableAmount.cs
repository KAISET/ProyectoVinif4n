using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;

public class XmlTagsPayableAmount
{
    public XmlTagsPayableAmount () {}

    [XmlAttribute(AttributeName = "currencyID")]
    public string CurrencyID { get; set; } = "PEN";

    [XmlText]
    public string? Value { get; set; }
}

