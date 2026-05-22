
namespace FacturadorSunat.Domain;

/// <summary>
/// Clase usada para llenar los tags de la firma XMLDSIG 
/// </summary>
public class AdditionalInformationAdditionalProperty
{
    public String? Id {get; set;}
    public String? Name {get; set;}
    public String? Value {get; set;}
}