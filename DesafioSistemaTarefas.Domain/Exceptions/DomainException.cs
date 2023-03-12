namespace DesafioSistemaTarefas.Domain.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string error) : base(error)
        { }

        public DomainException(string error, Exception? innerException)
        { }
        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainException(error);
        }
    }
}
