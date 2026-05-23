using System.Xml.Serialization;

namespace FacturadorSunat.Domain.XmlEntities.XmlDigitalSignatureSpecification;
public class XmlTagsTransforms
{
    [XmlElement(ElementName = "Transform", Namespace = XmlTagsNamespace.Ds)]
    public XmlTagsTransform? Transform {get; set;} = new ();
}