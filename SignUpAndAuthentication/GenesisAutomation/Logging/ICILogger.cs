using System;
namespace GenesisAutomation.Logging
{
    public interface ICILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(string message, Exception ex);
    }
}
