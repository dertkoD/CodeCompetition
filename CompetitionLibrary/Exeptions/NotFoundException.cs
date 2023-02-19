namespace CompetitionLibrary.Exeptions
{
    public class NotFoundException : Exception
    {
        public string? MyMessage;

        public NotFoundException()
        {
        }

        public NotFoundException(int objectId)
        {
        }
        public NotFoundException(string? message) : base(message)
        {
            MyMessage = message;
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public NotFoundException(string? message, string objectName) : this(message)
        {
        }
    }
}
