using PerformanceRecorder.Log;
using PerformanceRecorder.Result;
using System;

namespace PerformanceRecorder.API
{
    /// <summary>
    /// Main interface used to interact with the API.
    /// </summary>
    public interface IPerformanceRecorderApi
    {
        /// <summary>
        /// Flag to enable or disable performance recording.
        /// </summary>
        bool IsRecordingEnabled { get; set; }

        /// <summary>
        /// Set a new <see cref="ILogger"/> instance to be used by the library. Once set all logged messages will be forwarded to this instance.
        /// </summary>
        void SetLogger(ILogger logger);

        /// <summary>
        /// Helper method to enable the recorder. Sets <see cref="IsRecordingEnabled"/>.
        /// </summary>
        void EnablePerformanceRecording();

        /// <summary>
        /// Helper method to disable the recorder. Sets <see cref="IsRecordingEnabled"/>.
        /// </summary>
        void DisablePerformanceRecording();

        /// <summary>
        /// Reset the recorder by removing any results currently in memory.
        /// </summary>
        void ResetRecorder();

        /// <summary>
        /// Get the recording results from the current recording session.
        /// </summary>
        IRecordingSessionResult GetResults();

        /// <summary>
        /// Record a specific action with the provided name.
        /// The recorder generally works against annotated methods, this function extends the recording capabilities to <see cref="System.Action"/> instances.
        /// </summary>
        void RecordAction(string actionName, Action actionToRecord);

        /// <summary>
        /// Return a new <see cref="IFormatterFactoryApi"/> instance.
        /// </summary>
        IFormatterFactoryApi NewFormatterFactoryApi();
    }
}