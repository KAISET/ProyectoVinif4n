using FacturadorSunat.Domain.Entities.SectionAdditionalDocumentReference;
using FacturadorSunat.Domain.Entities.SectionAdditionalInformation;
using FacturadorSunat.Domain.Entities.SectionDespatchDocumentReference;
using FacturadorSunat.Domain.Entities.SectionDigitalSignatureSpecification;
using FacturadorSunat.Domain.Entities.SectionSignatureDeclaration;

namespace FacturadorSunat.Domain.Entities;

/// <summary>
/// Clase usada para llenar los datos XML para invoice
/// </summary>
public class Invoice
{
    public Invoice () {}
    /// <summary>
    /// 
    /// </summary>
    public DigitalSignatureSpecification? UblExtSignature { get; set;} = new ();
    /// <summary>
    /// 
    /// </summary>
    public Boolean IsUblExtAdditionalInformationUsed {get; set;}
    /// <summary>
    /// 
    /// </summary>
    public AdditionalInformationEntity? UblExtAdditionalInformation {get; set;} = new ();

    public String? UBLVersionId {get; set;} = String.Empty;
    public String? CustomizationId {get; set;} = String.Empty;
    public String? Id {get; set;} = String.Empty;
    public String? IssueDate {get; set;} = String.Empty;
    public String? InvoiceTypeCode {get; set;} = String.Empty;
    public String? DocumentCurrencyCode {get; set;} = String.Empty;
    public DespatchDocumentReferenceEntity? DespatchDocumentReference {get; set;} = new();
    public AdditionalDocumentReferenceEntity? AdditionalDocumentReference {get; set;} = new();
    public SignatureDeclarationEntity? SignatureDeclaration {get; set;} = new(); 
}