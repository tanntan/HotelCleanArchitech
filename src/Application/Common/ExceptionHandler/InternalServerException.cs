namespace CleanArchitecture.Blazor.Application.Common.Exceptions;
public class InternalServerException : ServerException
{
    public InternalServerException(string message, List<string>? errors = default)
        : base(message, errors, System.Net.HttpStatusCode.InternalServerError)
    {
    }
}