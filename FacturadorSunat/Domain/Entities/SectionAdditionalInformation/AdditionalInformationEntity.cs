namespace FacturadorSunat.Domain.Entities.SectionAdditionalInformation;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class AdditionalInformationEntity
{
    public AdditionalInformationAdditionalMonetaryTotalEntity? AdditionalMonetaryTotal {get; set;}
    public AdditionalInformationAdditionalPropertyEntity? AdditionalProperty {get; set;}
}