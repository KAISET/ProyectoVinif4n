using System.Xml.Linq;
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

            XmlTagsUblExtensionsExtensionContentSignature xmlSignature = new XmlTagsUblExtensionsExtensionContentSignature();
            XElement xmlSignatureContent = new XElement(xmlSignature.SignatureId, digitalSignatureValues.Id,
            new XElement(xmlSignature.SignatureValue, digitalSignatureValues.SignatureValue),
            new XElement(xmlSignature.X509Certificate, digitalSignatureValues.X509Certificate));

            // algunos de estos son requeridos, si estan vacios debe ser return vacio y con error
            XmlTagsUblExtensionsExtensionContentSignedInfo xmlSignedInfo = new XmlTagsUblExtensionsExtensionContentSignedInfo();
            XElement xmlSignedInfoContent = new XElement(xmlSignedInfo.SignedInfo,
            new XElement(xmlSignedInfo.CanonicalizationMethodAlgorithm, digitalSignatureValues.CanonicalizationMethodAlgorithm)
            , new XElement(xmlSignedInfo.SignatureMethodAlgorithm, digitalSignatureValues.SignatureMethodAlgorithm)
            , new XElement(xmlSignedInfo.ReferenceUri, digitalSignatureValues.ReferenceUri)
            , new XElement(xmlSignedInfo.TransformAlgorithm, digitalSignatureValues.TransformAlgorithm)
            , new XElement(xmlSignedInfo.DigestMethodAlgorithm, digitalSignatureValues.DigestMethodAlgorithm)
            , new XElement(xmlSignedInfo.DigestValue, digitalSignatureValues.DigestValue));

            XmlTagsUblExtensionsExtensionContentXMLDSIG xmlDsigBody = new XmlTagsUblExtensionsExtensionContentXMLDSIG();
            XElement extensionContent = new XElement(xmlDsigBody.ExtensionContent!.UblExtensionsExtensionContent
            , xmlSignatureContent
            , xmlSignedInfoContent);

            String XMLDSIG = extensionContent.ToString();
            operationResult.SetOperationResult(true, XMLDSIG, 200);
        }
        catch(Exception ex)
        {
            operationResult.SetOperationResult(false, null, 400, $"Error al generar XMLDSIG: {ex.Message}");
        }

        return operationResult;
    }
}