namespace FacturadorSunat.Domain.Entities.SectionSignatureDeclaration;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class PartyNameEntity
{
    /// <summary>
    /// Identificaciónde la firma dentro del documento 
    /// </summary>
    public String? Name {get; set;} = String.Empty;
}