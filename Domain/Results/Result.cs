using System.Diagnostics.CodeAnalysis;

namespace PressedCoins.Domain.Result;

public class Result<TResult> 
{
    // Success constructor
    private Result(TResult value)
    {
        IsSuccess = true;
        ResultObject = value;
        Error = null;
    }

    // Failure constructor
    private Result(Error error)
    {
        IsSuccess = false;
        ResultObject = default;
        Error = error;
    }
    
    [MemberNotNullWhen(true, nameof(ResultObject))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get;}
    public TResult? ResultObject { get; } 
    public Error? Error { get; } 
    
    // Helper methods for constructing the `Result<T>`
    public static Result<TResult> Success(TResult value) => new(value);
    public static Result<TResult> Fail(Error error) => new(error);
    
    // Allow converting a T directly into Result<T>
    public static implicit operator Result<TResult>(TResult value) => Success(value);
    public static implicit operator Result<TResult>(Error error) => Fail(error);
}

public abstract record Error; 
