namespace FacturadorSunat.Domain.Entities.SectionSignatureDeclaration;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class SignatoryPartyEntity
{
    /// <summary>
    /// Identificaciónde la firma dentro del documento 
    /// </summary>
    public PartyIdentificationEntity? PartyIdentification {get; set;} = new();
    /// <summary>
    /// 
    /// </summary>
    public PartyNameEntity? PartyName {get; set;} = new();
}