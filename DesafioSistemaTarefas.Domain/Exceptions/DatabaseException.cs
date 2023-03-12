namespace DesafioSistemaTarefas.Domain.Exceptions
{
    [Serializable]
    public class DataBaseException : Exception
    {
        public DataBaseException(string error) : base(error)
        { }
        public DataBaseException(string error, Exception? innerException)
        { }
    }
}
