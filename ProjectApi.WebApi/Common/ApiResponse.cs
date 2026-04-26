namespace ProjectApi.WebApi.Common;

public class ApiResponse
{
    public int Code { get; init; }
    public int Count { get; init; }
    public string Message { get; init; } = "Successfully";
    public string MessageAlt { get; init; } = "สำเร็จ";
    public object? Results { get; init; }

    public static ApiResponse Success(object? data = null)
    {
        var count = data switch
        {
            System.Collections.ICollection col => col.Count,
            null => 0,
            _ => 1
        };
        return new() { Code = 200, Count = count, Results = data };
    }

    public static ApiResponse Fail(int code, string message, string messageAlt = "ผิดพลาด")
        => new() { Code = code, Count = 0, Message = message, MessageAlt = messageAlt };
}
