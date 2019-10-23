using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal interface ITreeNode<TValue, TRec> where TRec : ITreeNode<TValue, TRec>
    {
        TRec Parent { get; }

        TValue Value { get; }

        TRec AddChild(TValue value);

        TRec Find(Func<TValue, bool> matcher);

        TRec Find(TValue value);

        IEnumerable<TValue> Flatten();
    }
}