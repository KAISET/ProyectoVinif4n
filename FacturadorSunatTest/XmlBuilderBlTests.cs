using Xunit;
using System.Net;
using FacturadorSunat.Bl;
using FacturadorSunat.Domain;
using FacturadorSunat.Utility;

namespace FacturadorSunatTest;

public class XmlBuilderBlTests
{
    [Fact]
    public void BuildInvoiceXML_Ok()
    {   
        Invoice datosPruebaFirma = new Invoice ()
        {
            UblExtSignature = new Signature()
            {
                Id = "SIGNATURE_KAISET",
                SignatureValue = "Abc123ValueBase64String...",
                X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
                SignedInfo = new SignatureSignedInfo()
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
            UblExtAdditionalInformation = new AdditionalInformation()
            {
                AdditionalMonetaryTotal = new AdditionalInformationAdditionalMonetaryTotal() {
                    Id = "2001", 
                    Name = "Percepción",
                    ReferenceAmount = "1000.00",
                    PayableAmount = "20.00",
                    Percent = "2.00",
                    TotalAmount = "1020.00"
                },
                AdditionalProperty = new AdditionalInformationAdditionalProperty()
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
            UblExtSignature = new Signature()
            {
                Id = "SIGNATURE_KAISET",
                SignatureValue = "Abc123ValueBase64String...",
                X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
                SignedInfo = new SignatureSignedInfo()
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
            UblExtAdditionalInformation = new AdditionalInformation()
            {
                AdditionalMonetaryTotal = new AdditionalInformationAdditionalMonetaryTotal() {
                    Id = "1001",
                    PayableAmount = "",
                    Percent = "2.00",
                    TotalAmount = "1020.00"
                },
                AdditionalProperty = new AdditionalInformationAdditionalProperty()
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
        AdditionalInformation additionalInformation = new AdditionalInformation
        {
            AdditionalMonetaryTotal = new AdditionalInformationAdditionalMonetaryTotal() {
                Id = "2001", 
                Name = "Percepción",
                ReferenceAmount = "1000.00",
                PayableAmount = "20.00",
                Percent = "2.00",
                TotalAmount = "1020.00"
            },
            AdditionalProperty = new AdditionalInformationAdditionalProperty()
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
        var datosPruebaFirma = new Signature
        {
            Id = "SIGNATURE_KAISET",
            SignatureValue = "Abc123ValueBase64String...",
            X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
            SignedInfo = new SignatureSignedInfo()
            {
                CanonicalizationMethodAlgorithm = "http://www.w3.org/2001/10/xml-exc-c14n#",
                SignatureMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
                ReferenceUri = "#DOCUMENTO-ID",
                TransformAlgorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature",
                DigestMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#sha1",
                DigestValue = "vafds89723hfsf="
            }
        };

        OperationResult<XmlTagsSignature> resultado = XmlBuilderBl.Instance.BuildSignatureXML(datosPruebaFirma);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data);
    }
}