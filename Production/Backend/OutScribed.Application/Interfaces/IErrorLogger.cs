namespace OutScribed.Application.Interfaces
{
    public interface IErrorLogger
    {

        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(Exception exception);

        void LogError(string exception);

    }
}
