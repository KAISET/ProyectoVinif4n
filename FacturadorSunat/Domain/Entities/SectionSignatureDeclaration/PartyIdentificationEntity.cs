namespace FacturadorSunat.Domain.Entities.SectionSignatureDeclaration;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class PartyIdentificationEntity
{
    /// <summary>
    /// Identificaciónde la firma dentro del documento 
    /// </summary>
    public String? Id {get; set;} = String.Empty;
}