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
        var datosPruebaFirma = new DigitalSignature
        {
            Id = "SIGNATURE_KAISET",
            SignatureValue = "Abc123ValueBase64String...",
            X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
            CanonicalizationMethodAlgorithm = "http://www.w3.org/2001/10/xml-exc-c14n#",
            SignatureMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
            ReferenceUri = "#DOCUMENTO-ID",
            TransformAlgorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature",
            DigestMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#sha1",
            DigestValue = "vafds89723hfsf="
        };

        OperationResult<String> resultado = XmlBuilderBl.Instance.BuildXML(datosPruebaFirma);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data);
    }

    [Fact]
    public void BuildSignatureXML_Ok()
    {   
        var datosPruebaFirma = new DigitalSignature
        {
            Id = "SIGNATURE_KAISET",
            SignatureValue = "Abc123ValueBase64String...",
            X509Certificate = "MIIFpDCCA4SgAwIBAgIQ...",
            CanonicalizationMethodAlgorithm = "http://www.w3.org/2001/10/xml-exc-c14n#",
            SignatureMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
            ReferenceUri = "#DOCUMENTO-ID",
            TransformAlgorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature",
            DigestMethodAlgorithm = "http://www.w3.org/2000/09/xmldsig#sha1",
            DigestValue = "vafds89723hfsf="
        };

        OperationResult<XmlTagsUBLExtensionXMLDSIG> resultado = XmlBuilderBl.Instance.BuildSignatureXML(datosPruebaFirma);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data);
    }
}