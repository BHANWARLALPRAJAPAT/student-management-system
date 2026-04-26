namespace StudentManagementSystem.Application.Common.Responses;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Result { get; set; }

    public bool Success => StatusCode >= 200 && StatusCode < 300;

    public static ApiResponse<T> SuccessResponse(T result, string message = "Success", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            StatusCode = statusCode,
            Message = message,
            Result = result
        };
    }

    public static ApiResponse<T> FailResponse(string message, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            StatusCode = statusCode,
            Message = message,
            Result = default
        };
    }
}