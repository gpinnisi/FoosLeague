namespace FoosLeague.Core.Models.XResults.Fails;

public class Fail
{
    public string Message { get; }

    public Fail(string message)
    {
        Message = message;
    }

    public Fail() : this(string.Empty)
    {
    }
}

