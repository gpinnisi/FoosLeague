using FoosLeague.Core.Models.XResults.Fails;

namespace FoosLeague.Core.Models.XResults;

public class XResult
{
    public Fail? Fail { get; }

    public XResultState State => Fail == null ? XResultState.Success : XResultState.Fail;

    public bool IsSuccess => State == XResultState.Success;
    public bool IsFailed => State == XResultState.Fail;

    public XResult()
    {
    }

    public XResult(Fail fail)
    {
        Fail = fail ?? throw new ArgumentNullException(nameof(fail));
    }

    public bool IsFailedType<TFail>() where TFail : Fail =>
        Fail is TFail;

    public TFail? GetFail<TFail>() where TFail : Fail => (TFail?)Fail;

    public static XResult Success => new();

    public static implicit operator XResult(Fail fail) => new(fail);

    public enum XResultState
    {
        Success = 1,
        Fail = 2
    }
}

