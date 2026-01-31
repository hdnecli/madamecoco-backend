using System.Text.Json.Serialization;

namespace MadameCoco.Shared;

public class Response<T>
{
    public T Data { get; private set; }
    
    [JsonIgnore]
    public int StatusCode { get; private set; }

    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public static Response<T> Success(T data, int statusCode)
    {
        return new Response<T>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static Response<T> Fail(string message, int statusCode)
    {
        return new Response<T>
        {
            Message = message,
            StatusCode = statusCode,
            IsSuccess = false
        };
    }
}
