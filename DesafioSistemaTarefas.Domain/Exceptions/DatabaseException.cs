using System.Diagnostics.CodeAnalysis;

namespace DesafioSistemaTarefas.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class DataBaseException : Exception
    {
        [ExcludeFromCodeCoverage]
        public DataBaseException(string error) : base(error)
        { }
        [ExcludeFromCodeCoverage]
        public DataBaseException(string error, Exception? innerException) : base(error, innerException)
        { }
    }
}
