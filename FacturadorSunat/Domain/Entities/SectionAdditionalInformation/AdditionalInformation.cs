namespace FacturadorSunat.Domain.Entities.SectionAdditionalInformation;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class AdditionalInformation
{
    public AdditionalInformationAdditionalMonetaryTotal? AdditionalMonetaryTotal {get; set;}
    public AdditionalInformationAdditionalProperty? AdditionalProperty {get; set;}
}