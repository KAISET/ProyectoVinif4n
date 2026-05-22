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

    public void SetOperationResult (ref OperationResult<T> operationResult, Boolean isSuccess, T? data, Int32 code, String customMessage = "")
    {
        String ErrorMessage = "Error";
        String SuccessMessage = "Success";
        operationResult = new OperationResult<T>()
        {
            Code = code,
            Success = isSuccess,
            Message = customMessage == "" ? (isSuccess ? SuccessMessage : ErrorMessage) : customMessage,
            Data = data
        };    
    }
}