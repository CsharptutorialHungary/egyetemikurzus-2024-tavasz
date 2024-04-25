namespace TruthOrDare.Domain.Exceptions;

// TODO: Replace Exception based flow control with Result pattern

/// <summary>
/// Exception that is safe to show to the user. It is used to show a message to the user when something goes wrong.
/// </summary>
public class SafeException : Exception
{
    public SafeException() : base("Something went wrong.") { }
    public SafeException(string message) : base(message) { }
}
