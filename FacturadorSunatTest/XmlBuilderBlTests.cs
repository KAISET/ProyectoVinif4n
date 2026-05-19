using Xunit;
using System.Net;
using FacturadorSunat.Bl;
using FacturadorSunat.Domain;
using FacturadorSunat.Utility;

namespace FacturadorSunatTest;

public class XmlBuilderBlTests
{
    [Fact]
    public void BuildSignatureXML_ConDatosValidos_DebeRetornarXmlExitoso()
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

        // 2. ACT (Ejecutar el método que estamos evaluando)
        OperationResult<string> resultado = XmlBuilderBl.Instance.BuildSignatureXML(datosPruebaFirma);

        // 3. ASSERT (Verificar que los resultados cumplan las condiciones estrictas)
        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Data);
    }
}