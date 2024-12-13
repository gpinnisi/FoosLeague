using FoosLeague.Core.Models.XResults.Fails;

namespace FoosLeague.Core.Models.XResults;

public class XResult<TPayload> : XResult
{
    private readonly TPayload _payload;
    public TPayload Payload
    {
        get
        {
            if (State == XResultState.Success) { return _payload; }
            throw new InvalidOperationException($"You can't access payload if the state is {State}.");
        }
    }

    public XResult(TPayload payload) : base()
    {
        _payload = payload;
    }

    public XResult(Fail fail) : base(fail)
    {
        _payload = default!;
    }

    public static XResult<TPayload> Create(TPayload payload) => new(payload);

    public static implicit operator XResult<TPayload>(TPayload payload) => new(payload);
    public static implicit operator XResult<TPayload>(Fail fail) => new(fail);
}

