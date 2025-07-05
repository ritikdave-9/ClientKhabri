public class ApiResponse<T>
{
    public HttpResponseMessage Response { get; set; }

    public T Data { get; set; }
    public ErrorResponseDto Error { get; set; } = new ErrorResponseDto
    {
        Message = "An error occurred while processing your request."
    };
}
public class ApiResponse : ApiResponse<dynamic>
{
}