using FacturadorSunat.Utility;
using FacturadorSunat.Domain.Entities;
using FacturadorSunat.Domain.Entities.SectionAdditionalInformation;
using FacturadorSunat.Domain.Entities.SectionDespatchDocumentReference;
using FacturadorSunat.Domain.Entities.SectionDigitalSignatureSpecification;
using FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;
using FacturadorSunat.Domain.XmlEntities.XmlDespatchDocumentReference;
using FacturadorSunat.Domain.XmlEntities.XmlDigitalSignatureSpecification;
using FacturadorSunat.Domain.XmlEntities.XmlInvoice;
using FacturadorSunat.Domain.XmlEntities.XmlAdditionalDocumentReference;
using FacturadorSunat.Domain.Entities.SectionAdditionalDocumentReference;
using FacturadorSunat.Domain.XmlEntities.XmlSignatureDeclaration;
using FacturadorSunat.Domain.Entities.SectionSignatureDeclaration;

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
                
                ublExtensionItemsAdditionalInformation.ExtensionContent.XmlUblAdditionalInformation = ublAdditionalInformation.Data;
                xmlTagsUblExtensionItems.Add(ublExtensionItemsAdditionalInformation);
            }

            // faltan validar que los campos esten llenos
            XmlTagsUblExtensionItems ublExtensionItemsSignature = new XmlTagsUblExtensionItems();
            OperationResult<XmlTagsDigitalSignatureSpecification> ublExtensionXMLDSIGresult = new OperationResult<XmlTagsDigitalSignatureSpecification>();
            ublExtensionXMLDSIGresult = BuildSignatureXML(invoiceData.UblExtSignature);

            if(ublExtensionXMLDSIGresult.Success == false || ublExtensionXMLDSIGresult.Data == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400, "Error al generar XMLDSIG");
                return operationResult;
            }
            
            ublExtensionItemsSignature.ExtensionContent.XmlUblSignatureSpecification = ublExtensionXMLDSIGresult.Data;
            xmlTagsUblExtensionItems.Add(ublExtensionItemsSignature);

            invoice.UBLExtensions.UBLExtensionsItems = xmlTagsUblExtensionItems;
            invoice.UBLVersionId = invoiceData.UBLVersionId;
            invoice.CustomizationId = invoiceData.CustomizationId;
            invoice.Id = invoiceData.Id;
            invoice.IssueDate = invoiceData.IssueDate;
            invoice.InvoiceTypeCode = invoiceData.InvoiceTypeCode;
            invoice.DocumentCurrencyCode = invoiceData.DocumentCurrencyCode;

            OperationResult<XmlTagsDespatchDocumentReference> despatchDocumentReferenceSection = new OperationResult<XmlTagsDespatchDocumentReference>();
            despatchDocumentReferenceSection = BuildDespatchDocumentReferenceXML(invoiceData.DespatchDocumentReference);
            invoice.DespatchDocumentReference = despatchDocumentReferenceSection.Data;

            OperationResult<XmlTagsAdditionalDocumentReference> additionalDocumentReferenceSection = new OperationResult<XmlTagsAdditionalDocumentReference>();
            additionalDocumentReferenceSection = BuildAdditionalDocumentReferenceXML(invoiceData.AdditionalDocumentReference);
            invoice.AdditionalDocumentReference = additionalDocumentReferenceSection.Data;

            OperationResult<XmlTagsSignatureDeclaration> signatureDeclarationSection = new OperationResult<XmlTagsSignatureDeclaration>();
            signatureDeclarationSection = BuildSignatureDeclarationXML(invoiceData.SignatureDeclaration);
            invoice.SignatureDeclaration = signatureDeclarationSection.Data;

            String xmlInvoice = Tools.XMLSerializer(invoice);

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
    public OperationResult<XmlTagsDigitalSignatureSpecification> BuildSignatureXML(DigitalSignatureSpecification digitalSignatureValues)
    {
        OperationResult<XmlTagsDigitalSignatureSpecification> operationResult = new OperationResult<XmlTagsDigitalSignatureSpecification>();

        try
        {
            if(digitalSignatureValues == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsDigitalSignatureSpecification xmlTagsSignature = new XmlTagsDigitalSignatureSpecification();
            xmlTagsSignature = BuildSignature(digitalSignatureValues);

            operationResult.SetOperationResult(ref operationResult, true, xmlTagsSignature, 200);
        }
        catch(Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar seccion XMLDSIG: {ex.Message}");
        }

        return operationResult;
    }

    private static XmlTagsDigitalSignatureSpecification BuildSignature(DigitalSignatureSpecification digitalSignatureValues)
    {
        XmlTagsDigitalSignatureSpecification signature = new XmlTagsDigitalSignatureSpecification();
        signature.Id = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.Id ?? "");
        signature.SignedInfo = BuildSignedInfo(digitalSignatureValues);
        signature.SignedInfo.Reference = BuildReference(digitalSignatureValues);
        signature.SignatureValue = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.SignatureValue ?? "");
        signature.KeyInfo.X509Data.X509Certificate = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.X509Certificate ?? "");

        return signature;
    }

    private static XmlTagsSignedInfo BuildSignedInfo(DigitalSignatureSpecification digitalSignatureValues)
    {
        XmlTagsSignedInfo signedInfo = new XmlTagsSignedInfo();
        signedInfo.CanonicalizationMethod.Algorithm = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.CanonicalizationMethodAlgorithm ?? "");
        signedInfo.SignatureMethod.Algorithm = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.SignatureMethodAlgorithm ?? "");
        
        return signedInfo;
    }

    private static XmlTagsReference BuildReference(DigitalSignatureSpecification digitalSignatureValues)
    {
        XmlTagsReference reference = new XmlTagsReference();
        reference.Uri = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.ReferenceUri ?? "");
        reference.Transforms.Transform.Algorithm = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.TransformAlgorithm ?? "");
        reference.DigestMethod.Algorithm = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.DigestMethodAlgorithm ?? "");
        reference.DigestValue = Tools.ValidateStringIsNullOrEmpty(digitalSignatureValues.SignedInfo.DigestValue ?? "");

        return reference;
    }

    /// <summary>
    /// Construye la seccion XML para la Additional Information
    /// </summary>
    /// <param name="digitalSignatureValues">Clase que contiene los valores para Additional Information</param>
    /// <returns>A <see cref="OperationResult{XmlTagsUBLExtensionAdditionalInformation}"/></returns>
    public OperationResult<XmlTagsAdditionalInformation> BuildAdditionalInformationXML(AdditionalInformationEntity additionalInformationValues)
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

        private static XmlTagsAdditionalInformation BuildAdditionalInformation(AdditionalInformationEntity additionalInformationValues)
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

    private static XmlTagsAdditionalProperty BuildAdditonalProperty(AdditionalInformationEntity additionalInformationValues)
    {
        XmlTagsAdditionalProperty additionalProperty = new XmlTagsAdditionalProperty();
        additionalProperty.ID = additionalInformationValues.AdditionalProperty.Id;
        additionalProperty.Name = additionalInformationValues.AdditionalProperty.Name;
        additionalProperty.Value = additionalInformationValues.AdditionalProperty.Value;

        return additionalProperty;
    }

    public OperationResult<XmlTagsDespatchDocumentReference> BuildDespatchDocumentReferenceXML(DespatchDocumentReferenceEntity despatchDocumentReferenceValues)
    {
        OperationResult<XmlTagsDespatchDocumentReference> operationResult = new OperationResult<XmlTagsDespatchDocumentReference>();

        try
        {
            if (despatchDocumentReferenceValues == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsDespatchDocumentReference xmlTagsDespatchDocumentReference = new XmlTagsDespatchDocumentReference();
            xmlTagsDespatchDocumentReference = BuildDespatchDocumentReference(despatchDocumentReferenceValues);

            operationResult.SetOperationResult(ref operationResult, true, xmlTagsDespatchDocumentReference, 200);
        }
        catch (Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar seccion Guias de Remision: {ex.Message}");
        }

        return operationResult;
    }

    private static XmlTagsDespatchDocumentReference BuildDespatchDocumentReference(DespatchDocumentReferenceEntity despatchDocumentReferenceValues)
    {
        XmlTagsDespatchDocumentReference despatchDocumentReference = new XmlTagsDespatchDocumentReference();
        despatchDocumentReference.DocumentTypeCode = despatchDocumentReferenceValues.DocumentTypeCode;
        despatchDocumentReference.Id = despatchDocumentReferenceValues.DocumentTypeCode;

        return despatchDocumentReference;
    }

    public OperationResult<XmlTagsAdditionalDocumentReference> BuildAdditionalDocumentReferenceXML(AdditionalDocumentReferenceEntity additionalDocumentReferenceValues)
    {
        OperationResult<XmlTagsAdditionalDocumentReference> operationResult = new OperationResult<XmlTagsAdditionalDocumentReference>();

        try
        {
            if (additionalDocumentReferenceValues == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsAdditionalDocumentReference xmlTagsAdditionalDocumentReference = new XmlTagsAdditionalDocumentReference();
            xmlTagsAdditionalDocumentReference = BuildAdditionalDocumentReference(additionalDocumentReferenceValues);

            operationResult.SetOperationResult(ref operationResult, true, xmlTagsAdditionalDocumentReference, 200);
        }
        catch (Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar seccion Guias de Remision: {ex.Message}");
        }

        return operationResult;
    }

    private static XmlTagsAdditionalDocumentReference BuildAdditionalDocumentReference(AdditionalDocumentReferenceEntity additionalDocumentReferenceValues)
    {
        XmlTagsAdditionalDocumentReference additionalDocumentReference = new XmlTagsAdditionalDocumentReference();
        additionalDocumentReference.DocumentTypeCode = additionalDocumentReference.DocumentTypeCode;
        additionalDocumentReference.Id = additionalDocumentReference.Id;

        return additionalDocumentReference;
    }

    public OperationResult<XmlTagsSignatureDeclaration> BuildSignatureDeclarationXML(SignatureDeclarationEntity signatureDeclarationValues)
    {
        OperationResult<XmlTagsSignatureDeclaration> operationResult = new OperationResult<XmlTagsSignatureDeclaration>();

        try
        {
            if (signatureDeclarationValues == null)
            {
                operationResult.SetOperationResult(ref operationResult, false, null, 400);
                return operationResult;
            }

            XmlTagsSignatureDeclaration xmlTagsSignatureDeclaration = new XmlTagsSignatureDeclaration();
            xmlTagsSignatureDeclaration = BuildSignatureDeclaration(signatureDeclarationValues);

            operationResult.SetOperationResult(ref operationResult, true, xmlTagsSignatureDeclaration, 200);
        }
        catch (Exception ex)
        {
            operationResult.SetOperationResult(ref operationResult, false, null, 400, $"Error al generar seccion Guias de Remision: {ex.Message}");
        }

        return operationResult;
    }

    private static XmlTagsSignatureDeclaration BuildSignatureDeclaration(SignatureDeclarationEntity signatureDeclarationValues)
    {
        XmlTagsSignatureDeclaration additionalDocumentReference = new XmlTagsSignatureDeclaration();
        additionalDocumentReference.ID = signatureDeclarationValues.Id;
        additionalDocumentReference.SignatoryParty.PartyIdentification.Id = signatureDeclarationValues.SignatoryParty.PartyIdentification.Id;
        additionalDocumentReference.SignatoryParty.PartyName.Name = signatureDeclarationValues.SignatoryParty.PartyName.Name;
        additionalDocumentReference.DigitalSignatureAttachment.ExternalReference.Uri = signatureDeclarationValues.DigitalSignatureAttachment.ExternalReference.URI;

        return additionalDocumentReference;
    }
}