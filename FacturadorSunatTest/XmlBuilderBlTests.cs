using Xunit;
using System.Net;
using FacturadorSunat.Bl;
using FacturadorSunat.Domain.Entities.SectionAdditionalInformation;
using FacturadorSunat.Domain.XmlEntities.XmlAdditionalInformation;
using FacturadorSunat.Domain.Entities;
using FacturadorSunat.Domain.XmlEntities.XmlDigitalSignatureSpecification;
using FacturadorSunat.Domain.Entities.SectionDigitalSignatureSpecification;
using FacturadorSunat.Domain.Entities.SectionDespatchDocumentReference;
using FacturadorSunat.Domain.Entities.SectionAdditionalDocumentReference;
using FacturadorSunat.Domain.Entities.SectionSignatureDeclaration;

namespace FacturadorSunatTest;

public class XmlBuilderBlTests
{
    [Fact]
    public void BuildInvoiceXML_Ok()
    {   
        Invoice datosPruebaFirma = new Invoice()
        {
            UBLVersionId = "2.0", // Versión del UBL exigida para la estructura base en Perú
            CustomizationId = "2.0", // Versión de la personalización de SUNAT
            Id = "F001-00001234", // Serie y Correlativo de la Factura Electrónica
            IssueDate = "2026-05-23", // Fecha de emisión en formato YYYY-MM-DD
            InvoiceTypeCode = "01", // Código SUNAT "01" significa FACTURA
            DocumentCurrencyCode = "PEN", // Moneda: PEN para Soles ("USD" para dólares)

            // Guía de Remisión relacionada (Si aplica, por ejemplo código "09" para Guía de Remisión Remitente)
            DespatchDocumentReference = new DespatchDocumentReferenceEntity
            {
                Id = "T001-00000555", // Número de la guía relacionada
                DocumentTypeCode = "09" // Código de tipo de documento SUNAT para Guías
            },

            // Documento adicional relacionado (Ej: Nota de pedido, orden de compra, ticket de balanza)
            AdditionalDocumentReference = new AdditionalDocumentReferenceEntity
            {
                Id = "OC-998877", // Número de orden de compra
                DocumentTypeCode = "99" // "99" se usa de forma general para "Otros documentos"
            },

            // Declaración de la Firma dentro del cuerpo de la Factura (<cac:Signature>)
            SignatureDeclaration = new SignatureDeclarationEntity
            {
                Id = "SIGNATURE_KAISET", // Debe coincidir exactamente con el ID de tu UblExtSignature
                SignatoryParty = new SignatoryPartyEntity
                {
                    PartyIdentification = new PartyIdentificationEntity
                    {
                        Id = "20100147514" // El RUC de la empresa emisora que firma digitalmente
                    },
                    PartyName = new PartyNameEntity
                    {
                        Name = "TU EMPRESA S.A.C." // Razón social del emisor
                    }
                },
                DigitalSignatureAttachment = new DigitalSignatureAttachmentEntity
                {
                    ExternalReference = new ExternalReferenceEntity
                    {
                        URI = "#SIGNATURE_KAISET" // Apunta al ID de la firma digital
                    }
                }
            },
            UblExtSignature = new DigitalSignatureSpecification()
            {
                Id = "SIGNATURE_KAISET",
                SignatureValue = "Abc123ValueBase64String...",
                X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
                SignedInfo = new DigitalSignatureSpecificationSignedInfo()
                {
                    CanonicalizationMethodAlgorithm = "http://www.w3.org/2001/10/xml-exc-c14n#",
                    SignatureMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
                    ReferenceUri = "#DOCUMENTO-ID",
                    TransformAlgorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature",
                    DigestMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#sha1",
                    DigestValue = "vafds89723hfsf="
                }
            },
            IsUblExtAdditionalInformationUsed = true,
            UblExtAdditionalInformation = new AdditionalInformationEntity()
            {
                AdditionalMonetaryTotal = new AdditionalInformationAdditionalMonetaryTotalEntity() 
                {
                    Id = "2001", 
                    Name = "Percepción",
                    ReferenceAmount = "1000.00",
                    PayableAmount = "20.00",
                    Percent = "2.00",
                    TotalAmount = "1020.00"
                },
                AdditionalProperty = new AdditionalInformationAdditionalPropertyEntity()
                {
                    Id = "01",
                    Name = "Leyenda de Percepción",
                    Value = "Operación sujeta al Sistema de Pago de Obligaciones Tributarias"
                }
            }
        };

        OperationResult<String> resultado = XmlBuilderBl.Instance.BuildXML(datosPruebaFirma);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data);
    }

    [Fact]
    public void BuildInvoiceXML_Ok_Desde_CASO_A()
    {   
        Invoice datosPruebaFirma = new Invoice ()
        {
            UblExtSignature = new DigitalSignatureSpecification()
            {
                Id = "SIGNATURE_KAISET",
                SignatureValue = "Abc123ValueBase64String...",
                X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
                SignedInfo = new DigitalSignatureSpecificationSignedInfo()
                {
                    CanonicalizationMethodAlgorithm = "http://www.w3.org/2001/10/xml-exc-c14n#",
                    SignatureMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
                    ReferenceUri = "#DOCUMENTO-ID",
                    TransformAlgorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature",
                    DigestMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#sha1",
                    DigestValue = "vafds89723hfsf="
                }
            },
            IsUblExtAdditionalInformationUsed = true,
            UblExtAdditionalInformation = new AdditionalInformationEntity()
            {
                AdditionalMonetaryTotal = new AdditionalInformationAdditionalMonetaryTotalEntity() {
                    Id = "2001", 
                    Name = "Percepción",
                    ReferenceAmount = "1000.00",
                    PayableAmount = "20.00",
                    Percent = "2.00",
                    TotalAmount = "1020.00"
                },
                AdditionalProperty = new AdditionalInformationAdditionalPropertyEntity()
                {
                    Id = "01",
                    Name = "Leyenda de Percepción",
                    Value = "Operación sujeta al Sistema de Pago de Obligaciones Tributarias"
                }
            }
        };

        OperationResult<String> resultado = XmlBuilderBl.Instance.BuildXML(datosPruebaFirma);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data);
    }

    [Fact]
    public void BuildAdditionalInformationXML_Ok()
    {   
        AdditionalInformationEntity additionalInformation = new AdditionalInformationEntity
        {
            AdditionalMonetaryTotal = new AdditionalInformationAdditionalMonetaryTotalEntity() {
                Id = "2001", 
                Name = "Percepción",
                ReferenceAmount = "1000.00",
                PayableAmount = "20.00",
                Percent = "2.00",
                TotalAmount = "1020.00"
            },
            AdditionalProperty = new AdditionalInformationAdditionalPropertyEntity()
            {
                Id = "01",
                Name = "Leyenda de Percepción",
                Value = "Operación sujeta al Sistema de Pago de Obligaciones Tributarias"
            }
        };

        OperationResult<XmlTagsAdditionalInformation> resultado = XmlBuilderBl.Instance.BuildAdditionalInformationXML(additionalInformation);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data.ToString());
    }

    

    [Fact]
    public void BuildSignatureXML_Ok()
    {   
        var datosPruebaFirma = new DigitalSignatureSpecification
        {
            Id = "SIGNATURE_KAISET",
            SignatureValue = "Abc123ValueBase64String...",
            X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
            SignedInfo = new DigitalSignatureSpecificationSignedInfo()
            {
                CanonicalizationMethodAlgorithm = "http://www.w3.org/2001/10/xml-exc-c14n#",
                SignatureMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
                ReferenceUri = "#DOCUMENTO-ID",
                TransformAlgorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature",
                DigestMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#sha1",
                DigestValue = "vafds89723hfsf="
            }
        };

        OperationResult<XmlTagsDigitalSignatureSpecification> resultado = XmlBuilderBl.Instance.BuildSignatureXML(datosPruebaFirma);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data);
    }
}