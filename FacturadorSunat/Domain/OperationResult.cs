using System.Net;
using System.Xml.Linq;

namespace FacturadorSunat.Domain;

public class OperationResult<T>
{
    public OperationResult() {}
    public Int32 Code {get; set;}
    public Boolean Success {get; set;}
    public String? Message {get; set;}
    public T? Data {get; set;}

    public OperationResult<T> SetOperationResult (Boolean isSuccess, T? data, Int32 code, String customMessage = "")
    {
        String ErrorMessage = "Error";
        String SuccessMessage = "Success";
        return new OperationResult<T>()
        {
            Code = code,
            Success = isSuccess,
            Message = customMessage == "" ? (isSuccess ? SuccessMessage : ErrorMessage) : customMessage,
            Data = data
        };    
    }

    // private String GetSuccessCodeMessage(HttpStatusCode httpStatusCode)
    // {
    //     return httpStatusCode switch
    //     {
    //         HttpStatusCode.OK                  => "Operación realizada con éxito.",
    //         HttpStatusCode.Created             => "Archivo o recurso creado correctamente.",
    //         _                                  => $"Operación procesada con código: {httpStatusCode}"
    //     };
    // }

    // private String GetSuccessCodeError(HttpStatusCode httpStatusCode)
    // {
    //     return httpStatusCode switch
    //     {
    //         HttpStatusCode.BadRequest          => "Solicitud incorrecta. Faltan datos obligatorios o el formato es inválido.",
    //         HttpStatusCode.ServiceUnavailable  => "Servicio temporalmente no disponible. Los servidores de SUNAT están fuera de línea.",
    //         HttpStatusCode.GatewayTimeout      => "Tiempo de espera agotado. No se recibió respuesta del servidor de SUNAT.",
    //         HttpStatusCode.UnprocessableEntity => "Error de estructura. El XML no cumple con el esquema UBL requerido.",
    //         HttpStatusCode.InternalServerError => "Error interno en el servidor al procesar el documento.",
    //         _                                  => $"Operación procesada con código: {httpStatusCode}"
    //     };
    // }

    // private String GetSuccessCodeWarning(HttpStatusCode httpStatusCode)
    // {
    //     return httpStatusCode switch
    //     {
    //         HttpStatusCode.Unauthorized        => "No autorizado. Credenciales SOL de SUNAT inválidas o expiradas.",
    //         HttpStatusCode.NotFound            => "El documento o recurso solicitado no fue encontrado.",
    //         _                                  => $"Operación procesada con código: {httpStatusCode}"
    //     };
    // }
}