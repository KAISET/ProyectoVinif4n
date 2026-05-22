using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FacturadorSunat.Domain;
using FacturadorSunat.Domain.Entities;
using FacturadorSunat.Domain.Entities.SectionAdditionalInformation;
using FacturadorSunat.Domain.Entities.SectionDsig;
using FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;
using FacturadorSunat.Domain.XmlEntities.XmlDigitalSignature;
using FacturadorSunat.Domain.XmlEntities.XmlInvoice;

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
    /// Construye el archivo XML - BVE para SUNAT
    /// </summary>
    /// <param name="digitalSignatureValues">Clase que contiene los valores esperados por el XML</param>
    /// <returns>A <see cref="OperationResult{String}"/></returns>
    public OperationResult<String> BuildXML(Invoice invoiceData)
    {
        OperationResult<String> operationResult = new OperationResult<String>();

        try
        {
            if(invoiceData == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsInvoice invoice = new XmlTagsInvoice();
            List<XmlTagsUblExtensionItems> xmlTagsUblExtensionItems = new List<XmlTagsUblExtensionItems>();

            if(invoiceData.UblExtSignature == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400, "Signature requerido");
                return operationResult;
            }

            if (invoiceData.IsUblExtAdditionalInformationUsed)
            {
                OperationResult<XmlTagsAdditionalInformation> ublAdditionalInformation = new OperationResult<XmlTagsAdditionalInformation>();
                XmlTagsUblExtensionItems ublExtensionItemsAdditionalInformation = new XmlTagsUblExtensionItems();
                ublAdditionalInformation = BuildAdditionalInformationXML(invoiceData.UblExtAdditionalInformation);
                if(ublAdditionalInformation.Success == false || ublAdditionalInformation.Data == null)
                {
                    operationResult.SetOperationResult(ref operationResult, false, null, 400, "Error al generar informacion adicional");
                    return operationResult;
                }
                
                ublExtensionItemsAdditionalInformation.ExtensionContent.XmlUblAdditionalInformation = ublAdditionalInformation.Data;
                xmlTagsUblExtensionItems.Add(ublExtensionItemsAdditionalInformation);
            }

            XmlTagsUblExtensionItems ublExtensionItemsSignature = new XmlTagsUblExtensionItems();
            // faltan validar que los campos esten llenos

            OperationResult<XmlTagsSignature> ublExtensionXMLDSIGresult = new OperationResult<XmlTagsSignature>();
            ublExtensionXMLDSIGresult = BuildSignatureXML(invoiceData.UblExtSignature);

            if(ublExtensionXMLDSIGresult.Success == false || ublExtensionXMLDSIGresult.Data == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400, "Error al generar XMLDSIG");
                return operationResult;
            }
            
            ublExtensionItemsSignature.ExtensionContent.XmlUblSignature = ublExtensionXMLDSIGresult.Data;
            xmlTagsUblExtensionItems.Add(ublExtensionItemsSignature);

            invoice.UBLExtensions.UBLExtensionsItems = xmlTagsUblExtensionItems;
            String xmlInvoice = XMLSerializer(invoice);

            operationResult.SetOperationResult(ref operationResult, true, xmlInvoice, 200);
        }
        catch(Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar Invoice XML: {ex.Message}");
        }

        return operationResult;
    }

    /// <summary>
    /// Construye la seccion XML para la firma XMLDSIG 
    /// </summary>
    /// <param name="digitalSignatureValues">Clase que contiene los valores para XMLDSIG</param>
    /// <returns>A <see cref="OperationResult{XmlTagsUBLExtensionXMLDSIG}"/></returns>
    public OperationResult<XmlTagsSignature> BuildSignatureXML(Signature digitalSignatureValues)
    {
        OperationResult<XmlTagsSignature> operationResult = new OperationResult<XmlTagsSignature>();

        try
        {
            if(digitalSignatureValues == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsSignature xmlTagsSignature = new XmlTagsSignature();
            xmlTagsSignature = BuildSignature(digitalSignatureValues);

            operationResult.SetOperationResult(ref operationResult, true, xmlTagsSignature, 200);
        }
        catch(Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar seccion XMLDSIG: {ex.Message}");
        }

        return operationResult;
    }

    /// <summary>
    /// Construye la seccion XML para la Additional Information
    /// </summary>
    /// <param name="digitalSignatureValues">Clase que contiene los valores para Additional Information</param>
    /// <returns>A <see cref="OperationResult{XmlTagsUBLExtensionAdditionalInformation}"/></returns>
    public OperationResult<XmlTagsAdditionalInformation> BuildAdditionalInformationXML(AdditionalInformation additionalInformationValues)
    {
        OperationResult<XmlTagsAdditionalInformation> operationResult = new OperationResult<XmlTagsAdditionalInformation>();

        try
        {
            if(additionalInformationValues == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsAdditionalInformation xmlTagsAdditionalInformation = new XmlTagsAdditionalInformation();
            xmlTagsAdditionalInformation = BuildAdditionalInformation(additionalInformationValues);

            operationResult.SetOperationResult(ref operationResult, true, xmlTagsAdditionalInformation, 200);
        }
        catch(Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar seccion Informacion Adicional: {ex.Message}");
        }

        return operationResult;
    }

    private static XmlTagsAdditionalInformation BuildAdditionalInformation(AdditionalInformation additionalInformationValues)
    {
        XmlTagsAdditionalInformation additionalInformation = new XmlTagsAdditionalInformation();
        additionalInformation.AdditionalMonetaryTotal.ID = additionalInformationValues.AdditionalMonetaryTotal.Id;
        additionalInformation.AdditionalMonetaryTotal.Name = additionalInformationValues.AdditionalMonetaryTotal.Name;
        additionalInformation.AdditionalMonetaryTotal.ReferenceAmount = additionalInformationValues.AdditionalMonetaryTotal.ReferenceAmount;
        additionalInformation.AdditionalMonetaryTotal.PayableAmount = new XmlTagsPayableAmount()
        {
            Value = additionalInformationValues.AdditionalMonetaryTotal.PayableAmount
        };
        additionalInformation.AdditionalMonetaryTotal.Percent = additionalInformationValues.AdditionalMonetaryTotal.Percent;
        additionalInformation.AdditionalMonetaryTotal.TotalAmount = additionalInformationValues.AdditionalMonetaryTotal.TotalAmount;
        additionalInformation.AdditionalProperty = BuildAdditonalProperty(additionalInformationValues);

        return additionalInformation;
    }

    private static XmlTagsAdditionalProperty BuildAdditonalProperty(AdditionalInformation additionalInformationValues)
    {
        XmlTagsAdditionalProperty additionalProperty = new XmlTagsAdditionalProperty();
        additionalProperty.ID = additionalInformationValues.AdditionalProperty.Id;
        additionalProperty.Name = additionalInformationValues.AdditionalProperty.Name;
        additionalProperty.Value = additionalInformationValues.AdditionalProperty.Value;

        return additionalProperty;
    }

    private static XmlTagsSignature BuildSignature(Signature digitalSignatureValues)
    {
        XmlTagsSignature signature = new XmlTagsSignature();
        signature.Id = ValidateStringIsNullOrEmpty(digitalSignatureValues.Id ?? "");
        signature.SignedInfo = BuildSignedInfo(digitalSignatureValues);
        signature.SignedInfo.Reference = BuildReference(digitalSignatureValues);
        signature.SignatureValue = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignatureValue ?? "");
        signature.KeyInfo.X509Data.X509Certificate = ValidateStringIsNullOrEmpty(digitalSignatureValues.X509Certificate ?? "");

        return signature;
    }

    private static XmlTagsSignedInfo BuildSignedInfo(Signature digitalSignatureValues)
    {
        XmlTagsSignedInfo signedInfo = new XmlTagsSignedInfo();
        signedInfo.CanonicalizationMethod.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.CanonicalizationMethodAlgorithm ?? "");
        signedInfo.SignatureMethod.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.SignatureMethodAlgorithm ?? "");
        
        return signedInfo;
    }

    private static XmlTagsReference BuildReference(Signature digitalSignatureValues)
    {
        XmlTagsReference reference = new XmlTagsReference();
        reference.Uri = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.ReferenceUri ?? "");
        reference.Transforms.Transform.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.TransformAlgorithm ?? "");
        reference.DigestMethod.Algorithm = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.DigestMethodAlgorithm ?? "");
        reference.DigestValue = ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.DigestValue ?? "");

        return reference;
    }

    private static String ValidateStringIsNullOrEmpty(String stringValue)
    {
        return stringValue = String.IsNullOrEmpty(stringValue) ? "NoData" : stringValue;
    }

    private static String XMLSerializer(XmlTagsInvoice xmlData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlTagsInvoice));
        
        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add("ext", XmlTagsNamespace.Ext);
        namespaces.Add("ds", XmlTagsNamespace.Ds);
        namespaces.Add("sac", XmlTagsNamespace.Sac);
        namespaces.Add("cbc", XmlTagsNamespace.Cbc);

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