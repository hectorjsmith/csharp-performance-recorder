using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    public interface ITreeNode<TValue, TRec> where TRec : ITreeNode<TValue, TRec>
    {
        int ChildCount { get; }

        TRec Parent { get; set; }

        TValue Value { get; }

        TRec AddChild(TValue value);

        TRec AddChild(TRec tree);

        IReadOnlyCollection<TRec> Children();

        TRec Find(Func<TValue, bool> matcher);

        TRec Find(TValue value);

        /// <summary>
        /// Flatten all children of this tree into a flat enumerable collection.
        /// </summary>
        IEnumerable<TValue> Flatten();
    }
}