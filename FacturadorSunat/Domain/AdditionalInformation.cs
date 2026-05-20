
namespace FacturadorSunat.Domain;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class AdditionalInformation
{
    public String? Id {get; set;}
    public String? Name {get; set;}
    public String? ReferenceAmount {get; set;}
    public String? PayableAmount {get; set;}
    public String? Percent {get; set;}
    public String? TotalAmount {get; set;}
    public String? AdditionalPropertyId {get; set;}
    public String? AdditionalPropertyName {get; set;}
    public String? AdditionalPropertyValue {get; set;}
}