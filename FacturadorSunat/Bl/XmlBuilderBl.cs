using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FacturadorSunat.Domain;

namespace FacturadorSunat.Bl;

public class XmlBuilderBl
{
   private static XmlBuilderBl instance = null;
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
                return operationResult.SetOperationResult(false, null, 400);
            }

            XmlTagsUblExtensions ublExtensions = new XmlTagsUblExtensions();

            ublExtensions.UblExtensionXMLDSIG.Signature.Id = digitalSignatureValues.Id;
            //SignedInfo
            ublExtensions.UblExtensionXMLDSIG.Signature.SignedInfo.CanonicalizationMethod.Algorithm = digitalSignatureValues.CanonicalizationMethodAlgorithm;
            ublExtensions.UblExtensionXMLDSIG.Signature.SignedInfo.SignatureMethod.Algorithm = digitalSignatureValues.SignatureMethodAlgorithm;
            // Reference
            ublExtensions.UblExtensionXMLDSIG.Signature.SignedInfo.Reference.Uri = digitalSignatureValues.ReferenceUri;
            ublExtensions.UblExtensionXMLDSIG.Signature.SignedInfo.Reference.Transforms.Transform.Algorithm = digitalSignatureValues.TransformAlgorithm;
            ublExtensions.UblExtensionXMLDSIG.Signature.SignedInfo.Reference.DigestMethod.Algorithm = digitalSignatureValues.DigestMethodAlgorithm;
            ublExtensions.UblExtensionXMLDSIG.Signature.SignedInfo.Reference.DigestValue = digitalSignatureValues.DigestValue;

            ublExtensions.UblExtensionXMLDSIG.Signature.SignatureValue = digitalSignatureValues.SignatureValue;
            ublExtensions.UblExtensionXMLDSIG.Signature.KeyInfo.X509Data.X509Certificate = digitalSignatureValues.X509Certificate;

            String XMLDSIG = XMLSerializer(ublExtensions);
            operationResult.SetOperationResult(true, XMLDSIG, 200);
        }
        catch(Exception ex)
        {
            operationResult.SetOperationResult(false, null, 400, $"Error al generar XMLDSIG: {ex.Message}");
        }

        return operationResult;
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