namespace FacturadorSunat.Domain.Entities.SectionSignatureDeclaration;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class DigitalSignatureAttachmentEntity
{
    /// <summary>
    /// Identificaciónde la firma dentro del documento 
    /// </summary>
    public ExternalReferenceEntity? ExternalReference {get; set;} = new();
}