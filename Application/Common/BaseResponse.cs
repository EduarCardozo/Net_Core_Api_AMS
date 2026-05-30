namespace Net_Core_Api.Application.Common;

public record BaseResponse<T>(bool Success, T? Data, string? Error = null)
{
    public static BaseResponse<T> Ok(T data) => new(true, data);
    public static BaseResponse<T> Fail(string error) => new(false, default, error);
}
