using FluentResults;

namespace MTGCardApi.Errors;

public abstract class BaseError : Error
{
    protected BaseError(ErrorType errorType, string message) : base(message)
    {
        ErrorType = errorType;
    }
    public ErrorType ErrorType { get; }
}

public enum ErrorType
{
    DoesNotExist = 1,
    Mismatch = 2,
    AlreadyExists = 3,
    NotAllowed = 4,
    ExternalService = 5
}