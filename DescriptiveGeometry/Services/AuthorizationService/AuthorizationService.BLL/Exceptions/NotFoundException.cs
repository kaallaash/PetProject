namespace AuthorizationService.BLL.Exceptions;

public class NotFoundException : Exception
{
    public int StatusCode { get; } = 404;

    public NotFoundException(): base(){}
    public NotFoundException(string message) : base(message) {}
    public NotFoundException(string message, Exception exception) : base(message, exception) { }
}
