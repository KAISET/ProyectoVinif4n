using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;
public class XmlTagsSignatoryParty
{
    [XmlElement(ElementName = "PartyIdentification", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsPartyIdentification? PartyIdentification {get; set;} = new();

    [XmlElement(ElementName = "PartyName", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsPartyName? PartyName {get; set;} = new();

    [XmlElement(ElementName = "DigitalSignatureAttachment ", Namespace = XmlTagsNamespace.Cac)]
    public XmlTagsDigitalSignatureAttachment? ID {get; set;} = new();
}