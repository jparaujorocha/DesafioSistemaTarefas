using DesafioSistemaTarefas.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace DesafioSistemaTarefas.Shared.Extensions
{
    public static class LoggerExtension
    {
        public static void LogDomainExceptionError(ILogger logger, string local, string tarefa, DomainException exception, string message)
        {
            logger.LogError(exception, "Erro: DomainException on "  + local + " to " + tarefa + ": " + message);
        }
        public static void LogApplicationExceptionError(ILogger logger, string local, string tarefa, ApplicationException exception, string message)
        {
            logger.LogError(exception, "Erro: ApplicationException on " + local + " to " + tarefa + ": " + message);
        }
        public static void LogDatabaseExceptionError(ILogger logger, string local, string tarefa, DataBaseException exception, string message)
        {
            logger.LogError(exception, "Erro: DataBaseException on " + local + " to " + tarefa + ": " + message);
        }

        public static void LogExceptionError(ILogger logger, string local, string tarefa, Exception exception, string message)
        {
            logger.LogError(exception, "Erro: Exception on " + local + " to " + tarefa + ": " + message);
        }
    }

}
