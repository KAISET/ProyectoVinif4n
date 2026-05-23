namespace FacturadorSunat.Domain.Entities.SectionAdditionalInformation;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class AdditionalInformationAdditionalMonetaryTotalEntity
{
    public String? Id {get; set;}
    public String? Name {get; set;}
    public String? ReferenceAmount {get; set;}
    public String? PayableAmount {get; set;}
    public String? Percent {get; set;}
    public String? TotalAmount {get; set;}
}