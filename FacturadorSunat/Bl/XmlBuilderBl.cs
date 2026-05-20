using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FacturadorSunat.Domain;

namespace FacturadorSunat.Bl;

public class XmlBuilderBl
{
   private static XmlBuilderBl? instance = null;
   public static XmlBuilderBl Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new XmlBuilderBl();
            }
            return instance;
        }
    }

    /// <summary>
    /// Construlle la seccion XML para la firma XMLDSIG 
    /// </summary>
    /// <param name="digitalSignatureValues">Clase que contiene los valores para XMLDSIG</param>
    /// <returns>A <see cref="OperationResult{String}"/></returns>
    public OperationResult<String> BuildSignatureXML(DigitalSignature digitalSignatureValues)
    {
        OperationResult<String> operationResult = new OperationResult<String>();

        try
        {
            if(digitalSignatureValues == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsUblExtensions ublExtensionsContent = new XmlTagsUblExtensions(){};
            ublExtensionsContent.UBLExtensionXMLDSIG.XMLDSIGExtentionContent.Signature = BuildSignature(digitalSignatureValues);

            String XMLDSIG = XMLSerializer(ublExtensionsContent);
            operationResult.SetOperationResult(ref operationResult, true, XMLDSIG, 200);
        }
        catch(Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar XMLDSIG: {ex.Message}");
        }

        return operationResult;
    }

    private static XmlTagsSignature BuildSignature(DigitalSignature digitalSignatureValues)
    {
        XmlTagsSignature signature = new XmlTagsSignature();
        signature.Id = ValidateStringIsNullOrEmpty(digitalSignatureValues.Id ?? "");
        signature.SignedInfo = BuildSignedInfo(digitalSignatureValues);
        signature.SignedInfo.Reference = BuildReference(digitalSignatureValues);
        signature.SignatureValue = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignatureValue ?? "");
        signature.KeyInfo.X509Data.X509Certificate = ValidateStringIsNullOrEmpty(digitalSignatureValues.X509Certificate ?? "");

        return signature;
    }

    private static XmlTagsSignedInfo BuildSignedInfo(DigitalSignature digitalSignatureValues)
    {
        XmlTagsSignedInfo signedInfo = new XmlTagsSignedInfo();
        signedInfo.CanonicalizationMethod.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.CanonicalizationMethodAlgorithm ?? "");
        signedInfo.SignatureMethod.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignatureMethodAlgorithm ?? "");
        
        return signedInfo;
    }

    private static XmlTagsReference BuildReference(DigitalSignature digitalSignatureValues)
    {
        XmlTagsReference reference = new XmlTagsReference();
        reference.Uri = ValidateStringIsNullOrEmpty(digitalSignatureValues.ReferenceUri ?? "");
        reference.Transforms.Transform.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.TransformAlgorithm ?? "");
        reference.DigestMethod.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.DigestMethodAlgorithm ?? "");
        reference.DigestValue = ValidateStringIsNullOrEmpty(digitalSignatureValues.DigestValue ?? "");

        return reference;
    }

    private static String ValidateStringIsNullOrEmpty(String stringValue)
    {
        return stringValue = String.IsNullOrEmpty(stringValue) ? "NoData" : stringValue;
    }

    private static String XMLSerializer(XmlTagsUblExtensions xmlData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlTagsUblExtensions));
        
        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add("ext", XmlTagsNamespace.Ext);
        namespaces.Add("ds", XmlTagsNamespace.Ds);

        XmlWriterSettings writerSettings = new XmlWriterSettings()
        {
            Encoding = Encoding.UTF8,
            Indent = true,
            OmitXmlDeclaration = true
        };

        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings);

        serializer.Serialize(xmlWriter, xmlData, namespaces);
        return stringWriter.ToString();
    }
}