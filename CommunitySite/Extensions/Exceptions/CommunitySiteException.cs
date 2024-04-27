namespace CommunitySite.Extensions.Exceptions
{
    public class CommunitySiteException : Exception
    {
        public CommunitySiteException() { }

        public CommunitySiteException(string message) : base(message) { }

        public CommunitySiteException(string message, Exception exception) : base(message, exception) { }
    }
}
