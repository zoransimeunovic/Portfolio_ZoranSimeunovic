namespace MsGraphClient;

public record GraphResult(bool Success, string? Error = null)
{
    public static GraphResult Ok() => new(true);
    public static GraphResult Fail(string error) => new(false, error);
}

public record GraphResult<T>(bool Success, T? Data, string? Error = null)
{
    public static GraphResult<T> Ok(T data) => new(true, data);
    public static GraphResult<T> Fail(string error) => new(false, default, error);
}
