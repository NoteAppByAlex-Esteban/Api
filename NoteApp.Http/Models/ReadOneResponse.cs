namespace NoteApp.Http.Models;


public class ReadOneResponse<T> : HttpResponse<T>
{


    /// <summary>
    /// Nueva respuesta ReadOne
    /// </summary>
    /// <param name="code">Codigo de estado</param>
    /// <param name="message">Mensaje</param>
    /// <param name="value">Valor</param>
    public ReadOneResponse(int code, string message, T value) 
    {
        base.StatusCode = code;
        base.Object = new
        {
            Message = message,
            Value = value
        };
    }


}
