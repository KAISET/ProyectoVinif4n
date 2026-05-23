namespace FacturadorSunat.Domain.Entities.SectionDigitalSignatureSpecification;

/// <summary>
/// Clase usada para llenar las etiquetas de SignedInfo de la firma XMLDSIG 
/// </summary>
public class DigitalSignatureSpecificationSignedInfo
{
    // /// <summary>
    // /// Información sobre el valor de la firma e información sobre los datos a firmar 
    // /// </summary>
    // public String? SignedInfo {get; set;}
    
    /// <summary>
    /// Indica cómo se debe transformar a forma canónica el elemento "Signinfo" antes de realizar la firma
    /// </summary>
    public String? CanonicalizationMethodAlgorithm {get; set;}
    /// <summary>
    /// Especifica qué tipo de algoritmo de firma que se utilizará para obtener la firma 
    /// </summary>
    public String? SignatureMethodAlgorithm {get; set;}
    /// <summary>
    /// Identifica al objeto de datos que se va a firmar, 
    /// si su valor es cadena vacía identifica al documento completo que contiene la firma
    /// </summary>
    public String? ReferenceUri {get; set;}
    /// <summary>
    /// Indica un paso realizado en el procesamiento de cálculo del hash
    /// </summary>
    public String? TransformAlgorithm {get; set;}
    /// <summary>
    /// Define la función hash utilizada 
    /// </summary>
    public String? DigestMethodAlgorithm {get; set;}
    /// <summary>
    /// Es el valor hash codificado en Base64 
    /// </summary>
    public String? DigestValue {get; set;}
}