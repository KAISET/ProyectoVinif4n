using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlDigitalSignature;

public class XmlTagsX509Data
{
    [XmlElement(ElementName ="X509Certificate", Namespace = XmlTagsNamespace.Ds)]
    public String? X509Certificate   {get; set;} = String.Empty;
    // Aca teoricamente tendria que haber un campo ds:X509SubjectName que creo que es opcional
}