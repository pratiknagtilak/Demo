namespace Demo.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message)
            : base(message, 400) { }
    }
}
