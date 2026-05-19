using System.Xml.Linq;
using FacturadorSunat.Utility;

namespace FacturadorSunat.Domain;

public class XmlTagsUblExtensionsExtensionContentSignedInfo
{
    /// <summary>
    /// Equivale a ./ds:SignedInfo
    /// </summary>
    public readonly XNamespace SignedInfo = XmlTagsPrefix.DigitalSignature + ":SignedInfo";
    
    /// <summary>
    /// Equivale a ./ds:CanonicalizationMethod@Algorithm 
    /// </summary>
    public readonly XNamespace CanonicalizationMethodAlgorithm = XmlTagsPrefix.DigitalSignature + "CanonicalizationMethod@Algorithm";
    
    /// <summary>
    /// Equivale a ./ds:SignatureMethod@Algorithm 
    /// </summary>
    public readonly XNamespace SignatureMethodAlgorithm = XmlTagsPrefix.DigitalSignature + "SignatureMethod@Algorithm";

    /// <summary>
    /// Equivale al prefijo ./ds:Reference
    /// </summary>
    private static readonly String Reference = XmlTagsPrefix.DigitalSignature + ":Reference";

    /// <summary>
    /// Equivale a: ./ds:Reference@URI 
    /// </summary>
    public readonly XNamespace ReferenceUri = Reference + "@URI";

    /// <summary>
    /// Equivale al prefijo ds:Transforms
    /// </summary>
    private static readonly String Transforms = XmlTagsPrefix.DigitalSignature + ":Transforms";

    /// <summary>
    /// Equivale al prefijo ds:Transform
    /// </summary>
    private static readonly String Transform = XmlTagsPrefix.DigitalSignature + ":Transform";

    /// <summary>
    /// Equivale a los prefijos ./ds:Reference/ds:Transforms/ds:Transform
    /// </summary>
    private static readonly List<String> ReferenceTransformItems = new List<String> ()
    {
        Reference,
        Transforms,
        Transform
    };

    /// <summary>
    /// Equivale a: ./ds:Reference/ds:Transforms/ds:Transform@Algorithm
    /// </summary>
    public readonly XNamespace TransformAlgorithm = Tools.BuildComplexTagPrefix(ReferenceTransformItems) + "@Algorithm";

    /// <summary>
    /// Equivale al prefijo ds:DigestMethod
    /// </summary>
    private static readonly String DigestMethod = XmlTagsPrefix.DigitalSignature + ":DigestMethod";

    /// <summary>
    /// Equivale a los prefijos ./ds:Reference/ds:DigestMethod
    /// </summary>
    private static readonly List<String> ReferenceDigestMethodItems = new List<String> ()
    {
        Reference,
        DigestMethod
    };

    /// <summary>
    /// Equivale a: ./ds:Reference/ds:DigestMethod@Algorithm 
    /// </summary>
    public readonly XNamespace DigestMethodAlgorithm = Tools.BuildComplexTagPrefix(ReferenceDigestMethodItems) + "@Algorithm";

    /// <summary>
    /// Equivale al prefijo ds:DigestValue
    /// </summary>
    private static readonly String DigestValueWithPrefix = XmlTagsPrefix.DigitalSignature + ":DigestValue";

    /// <summary>
    /// Equivale a los prefijos ./ds:Reference/ds:DigestMethod
    /// </summary>
    private static readonly List<String> DigestValueItems = new List<String> ()
    {
        Reference,
        DigestValueWithPrefix
    };

    /// <summary>
    /// Equivale a: ./ds:Reference/ds:DigestValue
    /// </summary>
    public readonly XNamespace DigestValue = Tools.BuildComplexTagPrefix(DigestValueItems);
}