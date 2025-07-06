public class ApiResponse<T>
{
    public HttpResponseMessage Response { get; set; }

    public T Data { get; set; }
    public MessageResponseDto Error { get; set; } = new MessageResponseDto
    {
        Message = "An error occurred while processing your request."
    };
}
public class ApiResponse : ApiResponse<dynamic>
{
}