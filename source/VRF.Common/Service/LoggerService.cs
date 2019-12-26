using NLog;
using System;
using System.Text;

namespace VRFEngine.Common.Service
{
    /// <summary>
    /// Logger Service Constants.
    /// </summary>
    public abstract class LoggerConstants
    {
        public const string DEBUG = "DEBUG";
        public const string INFO = "INFO";
        public const string WARN = "WARN";
        public const string ERROR = "ERROR";
    }

    /// <summary>
    /// Provides a wrapper implentation for the Logger framework.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        protected Logger Logger;

        #region Constants

        private const string SEPARATOR = ";";

        #endregion

        #region Fields

        private string _methodName;
        private string _className;
        private string _user;
        private bool _isInit;

        #endregion

        public LoggerService()
        {
        }

        #region Public Methods

        /// <summary>
        /// Initialize the service for the current action.
        /// </summary>
        /// <param name="className">Name of the current class.</param>
        /// <param name="methodName">Name of the current method.</param>
        /// <param name="user">Name of the user calling the method.</param>
        public void Init(string className, string methodName, string user)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentException("ClassName must not be null or empty.", nameof(className));
            }

            _className = className;

            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("MethodName must not be null or empty", nameof(methodName));
            }

            _methodName = methodName;


            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("User must not be null or empty", nameof(user));
            }

            _user = user;

            Logger = LogManager.GetLogger(className);
            _isInit = true;
        }

        /// <summary>
        /// Logs a Debug message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        public void Debug(string message, DateTime? clientDateTime = null)
        {
            CheckIsInit();
            clientDateTime = CheckClientDateTime(clientDateTime);
            string clientDateTimeString = ToStringForLog(clientDateTime.Value);
            string serverDateTimeString = ToStringForLog(DateTime.Now);

            Logger.Debug(GetLog(serverDateTimeString, clientDateTimeString, LoggerConstants.DEBUG, message, null));
        }

        /// <summary>
        /// Logs an Info message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        public void Info(string message, DateTime? clientDateTime = null)
        {
            CheckIsInit();
            clientDateTime = CheckClientDateTime(clientDateTime);
            string clientDateTimeString = ToStringForLog(clientDateTime.Value);
            string serverDateTimeString = ToStringForLog(DateTime.Now);

            Logger.Info(GetLog(serverDateTimeString, clientDateTimeString, LoggerConstants.INFO, message, null));
        }

        /// <summary>
        /// Logs a Warning message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        public void Warn(string message, DateTime? clientDateTime = null)
        {
            CheckIsInit();
            clientDateTime = CheckClientDateTime(clientDateTime);
            string clientDateTimeString = ToStringForLog(clientDateTime.Value);
            string serverDateTimeString = ToStringForLog(DateTime.Now);

            Logger.Warn(GetLog(serverDateTimeString, clientDateTimeString, LoggerConstants.WARN, message, null));
        }

        /// <summary>
        /// Logs an Error message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception - Optional</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        public void Error(string message, Exception exception = null, DateTime? clientDateTime = null)
        {
            CheckIsInit();
            clientDateTime = CheckClientDateTime(clientDateTime);
            string clientDateTimeString = ToStringForLog(clientDateTime.Value);
            string serverDateTimeString = ToStringForLog(DateTime.Now);
            string exceptionString = exception != null ? exception.ToString() : "";

            Logger.Error(GetLog(serverDateTimeString, clientDateTimeString, LoggerConstants.ERROR, message, exceptionString));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates a string for the log file.
        /// </summary>
        /// <param name="serverDateTime">Date and Time of the server.</param>
        /// <param name="clientDateTime">Date and Time of the client.</param>
        /// <param name="level">Log level.</param>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception string</param>
        /// <returns></returns>
        public string GetLog(string serverDateTime, string clientDateTime, string level, string message, string exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(serverDateTime);
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(clientDateTime);
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(level);
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(_user);
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(_className);
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(_methodName);
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(message);
            stringBuilder.Append(SEPARATOR);
            stringBuilder.Append(exception);
            stringBuilder.Append(SEPARATOR);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Checks if the client DateTime is not null.
        /// If it's null return the current DateTime.
        /// </summary>
        /// <param name="clientDateTime">Client Date and Time.</param>
        /// <returns>Current or Client Date and Time</returns>
        public DateTime? CheckClientDateTime(DateTime? clientDateTime)
        {
            if (clientDateTime == null)
            {
                clientDateTime = DateTime.Now;
            }

            return clientDateTime;
        }

        /// <summary>
        /// Checks if the current Service is initialized.
        /// </summary>
        public bool CheckIsInit()
        {
            if (!_isInit)
            {
                throw new InvalidOperationException("Call Init method before logging.");
            }

            return true;
        }

        /// <summary>
        /// Transform a DateTime in a string for logs.
        /// Format: "yyyy-MM-dd HH:mm:ss.ffff"
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns>Formatted string</returns>
        public string ToStringForLog(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff");
        }

        #endregion
    }
}
