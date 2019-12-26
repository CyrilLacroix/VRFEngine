using System;

namespace VRFEngine.Repository.Interface
{
    /// <summary>
    /// Provides a wrapper implentation for the Logger framework.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Initialize the service for the current action.
        /// </summary>
        /// <param name="className">Name of the current class.</param>
        /// <param name="methodName">Name of the current method.</param>
        /// <param name="user">Name of the user calling the method.</param>
        void Init(string className, string methodName, string user);

        /// <summary>
        /// Logs a Debug message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        void Debug(string message, DateTime? clientDateTime = null);

        /// <summary>
        /// Logs an Info message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        void Info(string message, DateTime? clientDateTime = null);

        /// <summary>
        /// Logs a Warning message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        void Warn(string message, DateTime? clientDateTime = null);

        /// <summary>
        /// Logs an Error message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception - Optional</param>
        /// <param name="clientDateTime">DateTime of the client (Frontend, Mobile app...) - Optional</param>
        void Error(string message, Exception exception = null, DateTime? clientDateTime = null);
    }
}