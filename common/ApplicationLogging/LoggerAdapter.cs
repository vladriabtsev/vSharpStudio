using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApplicationLogging
{
    public interface ILoggerAdapter
    {
        void LogTrace(Exception? exception, string? message, params object?[] args);
        void LogDebug(Exception? exception, string? message, params object?[] args);
        void LogInformation(Exception? exception, string? message, params object?[] args);
        void LogWarning(Exception? exception, string? message, params object?[] args);
        void LogError(Exception? exception, string? message, params object?[] args);
        void LogCritical(Exception? exception, string? message, params object?[] args);

        void LogTrace(string? message, params object?[] args);
        void LogDebug(string? message, params object?[] args);
        void LogInformation(string? message, params object?[] args);
        void LogWarning(string? message, params object?[] args);
        void LogError(string? message, params object?[] args);
        void LogCritical(string? message, params object?[] args);

        void LogTrace(string message);
        void LogTrace<T0>(string message, T0 arg0);
        void LogTrace<T0, T1>(string message, T0 arg0, T1 arg1);
        void LogTrace<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
        void LogDebug(string message);
        void LogDebug<T0>(string message, T0 arg0);
        void LogDebug<T0, T1>(string message, T0 arg0, T1 arg1);
        void LogDebug<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
        void LogInformation(string message);
        void LogInformation<T0>(string message, T0 arg0);
        void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1);
        void LogInformation<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
        void LogWarning(string message);
        void LogWarning<T0>(string message, T0 arg0);
        void LogWarning<T0, T1>(string message, T0 arg0, T1 arg1);
        void LogWarning<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
        void LogError(string message);
        void LogError<T0>(string message, T0 arg0);
        void LogError<T0, T1>(string message, T0 arg0, T1 arg1);
        void LogError<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
        void LogCritical(string message);
        void LogCritical<T0>(string message, T0 arg0);
        void LogCritical<T0, T1>(string message, T0 arg0, T1 arg1);
        void LogCritical<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
    }
    public class LoggerAdapter : ILoggerAdapter
    {
        private readonly ILogger _logger;
        public LoggerAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public void LogTrace(Exception? exception, string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Trace))
                _logger.LogTrace(exception, message, args);
        }
        public void LogDebug(Exception? exception, string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug(exception, message, args);
        }
        public void LogInformation(Exception? exception, string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(exception, message, args);
        }
        public void LogWarning(Exception? exception, string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(exception, message, args);
        }
        public void LogError(Exception? exception, string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(exception, message, args);
        }
        public void LogCritical(Exception? exception, string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Critical))
                _logger.LogCritical(exception, message, args);
        }

        public void LogTrace(string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Trace))
                _logger.LogTrace(message, args);
        }
        public void LogDebug(string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug(message, args);
        }
        public void LogInformation(string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, args);
        }
        public void LogWarning(string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, args);
        }
        public void LogError(string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, args);
        }
        public void LogCritical(string? message, params object?[] args)
        {
            if (_logger.IsEnabled(LogLevel.Critical))
                _logger.LogCritical(message, args);
        }

        public void LogTrace(string message)
        {
            if (_logger.IsEnabled(LogLevel.Trace))
                _logger.LogTrace(message);
        }
        public void LogTrace<T0>(string message, T0 arg0)
        {
            if (_logger.IsEnabled(LogLevel.Trace))
                _logger.LogTrace(message, arg0);
        }
        public void LogTrace<T0, T1>(string message, T0 arg0, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Trace))
                _logger.LogTrace(message, arg0, arg1);
        }
        public void LogTrace<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Trace))
                _logger.LogTrace(message, arg0, arg1, arg2);
        }
        public void LogDebug(string message)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug(message);
        }
        public void LogDebug<T0>(string message, T0 arg0)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug(message, arg0);
        }
        public void LogDebug<T0, T1>(string message, T0 arg0, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug(message, arg0, arg1);
        }
        public void LogDebug<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug(message, arg0, arg1, arg2);
        }
        public void LogInformation(string message)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message);
        }
        public void LogInformation<T0>(string message, T0 arg0)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, arg0);
        }
        public void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, arg0, arg1);
        }
        public void LogInformation<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(message, arg0, arg1, arg2);
        }
        public void LogWarning(string message)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message);
        }
        public void LogWarning<T0>(string message, T0 arg0)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, arg0);
        }
        public void LogWarning<T0, T1>(string message, T0 arg0, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, arg0, arg1);
        }
        public void LogWarning<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(message, arg0, arg1, arg2);
        }
        public void LogError(string message)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message);
        }
        public void LogError<T0>(string message, T0 arg0)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, arg0);
        }
        public void LogError<T0, T1>(string message, T0 arg0, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, arg0, arg1);
        }
        public void LogError<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(message, arg0, arg1, arg2);
        }
        public void LogCritical(string message)
        {
            if (_logger.IsEnabled(LogLevel.Critical))
                _logger.LogCritical(message);
        }
        public void LogCritical<T0>(string message, T0 arg0)
        {
            if (_logger.IsEnabled(LogLevel.Critical))
                _logger.LogCritical(message, arg0);
        }
        public void LogCritical<T0, T1>(string message, T0 arg0, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Critical))
                _logger.LogCritical(message, arg0, arg1);
        }
        public void LogCritical<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Critical))
                _logger.LogCritical(message, arg0, arg1, arg2);
        }
    }
}
