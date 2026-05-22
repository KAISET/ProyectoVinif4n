using System.Xml.Serialization;

namespace FacturadorSunat.Domain;

public class XmlTagsPayableAmount
{
    public XmlTagsPayableAmount () {}

    [XmlAttribute(AttributeName = "currencyID")]
    public string CurrencyID { get; set; } = "PEN";
}

