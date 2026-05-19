using System.Xml.Linq;
using FacturadorSunat.Utility;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensionsExtensionContentSignature
{
    /// <summary>
    /// Equivale a ./ds:Signature @Id
    /// </summary>
    public readonly XName SignatureId = XmlTagsPrefix.DigitalSignature + ":Signature @Id";

    public readonly XmlTagsUblExtensionsExtensionContentSignedInfo? SignedInfo;
    
    /// <summary>
    /// Equivale a: ./ds:SignatureValue
    /// </summary>
    public readonly XName SignatureValue = XmlTagsPrefix.DigitalSignature + ":SignatureValue";
    
    private static readonly String KeyInfo = XmlTagsPrefix.DigitalSignature + ":KeyInfo";
    private static readonly String X509Data = XmlTagsPrefix.DigitalSignature + ":X509Data";
    private static readonly String X509CertificateNode = XmlTagsPrefix.DigitalSignature + ":X509Certificate";
    private static readonly List<String> CertificateItems = new List<String>()
    {
        KeyInfo,
        X509Data,
        X509CertificateNode
    };

    /// <summary>
    /// Equivale a: ./ds:KeyInfo/ds:X509Data/ds:X509Certificate
    /// </summary>
    public readonly XName X509Certificate = Tools.BuildComplexTagPrefix(CertificateItems);
}