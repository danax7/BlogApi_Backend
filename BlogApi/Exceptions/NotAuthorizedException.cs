namespace BlogApi.Exception;

public class NotAuthorizedException : System.Exception
{
    public NotAuthorizedException(string message) : base(message)
    {
    }
}