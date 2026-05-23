namespace FacturadorSunat.Domain.Entities.SectionDigitalSignatureSpecification;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class DigitalSignatureSpecification
{
    /// <summary>
    /// Identificaciónde la firma dentro del documento 
    /// </summary>
    public String? Id {get; set;}
    /// <summary>
    /// Contiene los campos para las etiquetas signed info
    /// </summary>
    public DigitalSignatureSpecificationSignedInfo? SignedInfo {get; set;}
    /// <summary>
    /// Contiene la firma codificada en Base64
    /// </summary>
    public String? SignatureValue {get; set;}
    /// <summary>
    /// Es una estructura que contiene información del certificado firmante 
    /// </summary>
    public String? X509Certificate {get; set;}
}