using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class DomainException : Exception
    {
        [ExcludeFromCodeCoverage]
        public DomainException(string error) : base(error)
        { }

        [ExcludeFromCodeCoverage]
        public DomainException(string error, Exception? innerException) : base(error, innerException)
        {
        }
        [ExcludeFromCodeCoverage]
        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainException(error);
        }
    }
}
