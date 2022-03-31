using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    /// <summary>
    /// Represents a node in a tree structure of a generic type.
    /// </summary>
    public interface ITreeNode<TValue, TRec> where TRec : ITreeNode<TValue, TRec>
    {
        /// <summary>
        /// Return the number of children under this node.
        /// </summary>
        int ChildCount { get; }

        /// <summary>
        /// Get the parent node of this node.
        /// </summary>
        TRec Parent { get; set; }

        /// <summary>
        /// Get the value stored inside this node.
        /// </summary>
        TValue Value { get; }

        /// <summary>
        /// Add a new child to this node.
        /// </summary>
        TRec AddChild(TValue value);

        /// <summary>
        /// Add a new child to this node.
        /// </summary>
        TRec AddChild(TRec tree);

        /// <summary>
        /// Return a collection of all child nodes under this node.
        /// </summary>
        IReadOnlyCollection<TRec> Children();

        /// <summary>
        /// Recursively find the first child node that matches the given matcher function.
        /// </summary>
        TRec Find(Func<TValue, bool> matcher);

        /// <summary>
        /// Recursively find the first child node where the value matches the provided value.
        /// </summary>
        TRec Find(TValue value);

        /// <summary>
        /// Flatten all children of this tree into a flat enumerable collection.
        /// This function will recurse down the entire tree, not just one level.
        /// </summary>
        IEnumerable<TValue> Flatten();
    }
}