using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal interface ITreeNode<TValue, TRec> where TRec : ITreeNode<TValue, TRec>
    {
        int ChildCount { get; }

        TRec Parent { get; }

        TValue Value { get; }

        TRec AddChild(TValue value);

        IReadOnlyCollection<TRec> Children();

        TRec Find(Func<TValue, bool> matcher);

        TRec Find(Func<TRec, bool> matcher);

        TRec Find(TValue value);

        IEnumerable<TValue> Flatten();
    }
}