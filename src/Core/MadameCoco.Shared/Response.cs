using System.Text.Json.Serialization;

namespace MadameCoco.Shared;

/// <summary>
/// A generic wrapper for API responses to enforce standard structure.
/// </summary>
/// <typeparam name="T">The type of the data payload.</typeparam>
public class Response<T>
{
    /// <summary>
    /// Gets the data payload.
    /// </summary>
    public T? Data { get; private set; }
    
    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    [JsonIgnore]
    public int StatusCode { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Gets the error or status message.
    /// </summary>
    public string? Message { get; private set; }

    /// <summary>
    /// Creates a successful response.
    /// </summary>
    /// <param name="data">The data to return.</param>
    /// <param name="statusCode">The HTTP status code (default: 200).</param>
    /// <returns>A successful response wrapper.</returns>
    public static Response<T> Success(T data, int statusCode)
    {
        return new Response<T>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a failed response.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <returns>A failed response wrapper.</returns>
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
