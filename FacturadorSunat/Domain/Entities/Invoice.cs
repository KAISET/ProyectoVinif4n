namespace FacturadorSunat.Domain;

/// <summary>
/// Clase usada para llenar los datos XML para invoice
/// </summary>
public class Invoice
{
    public Invoice () {}
    /// <summary>
    /// 
    /// </summary>
    public Signature? UblExtSignature { get; set;}
    /// <summary>
    /// 
    /// </summary>
    public Boolean IsUblExtAdditionalInformationUsed {get; set;}
    /// <summary>
    /// 
    /// </summary>
    public AdditionalInformation? UblExtAdditionalInformation {get; set;}
}