using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal abstract class TreeNodeImpl<TValue, TRec> : ITreeNode<TValue, TRec> where TRec : ITreeNode<TValue, TRec>
    {
        private readonly List<TRec> _children = new List<TRec>();

        public TreeNodeImpl(TValue value)
        {
            Value = value;
        }

        public TreeNodeImpl()
        {
        }

        public int ChildCount => _children.Count;

        public TRec Parent { get; set; }

        public TValue Value { get; }

        public TRec AddChild(TValue value)
        {
            TRec node = GetNew(value, GetMe());
            _children.Add(node);
            return node;
        }

        public TRec AddChild(TRec tree)
        {
            tree.Parent = GetMe();
            _children.Add(tree);
            return tree;
        }

        public IReadOnlyCollection<TRec> Children()
        {
            return _children.AsReadOnly();
        }

        public TRec Find(Func<TValue, bool> matcher)
        {
            if (Value != null && matcher(Value))
            {
                return GetMe();
            }

            foreach (TRec child in _children)
            {
                TRec result = child.Find(matcher);
                if (result != null)
                {
                    return result;
                }
            }
            return GetDefault();
        }

        public TRec Find(TValue value)
        {
            if (value == null)
            {
                return GetDefault();
            }
            return Find(v => v?.Equals(value) == true);
        }

        public IEnumerable<TValue> Flatten()
        {
            if (Value == null)
            {
                return _children.SelectMany(x => x.Flatten());
            }
            else
            {
                return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
            }
        }

        protected abstract TRec GetNew(TValue value, TRec parent);

        protected abstract TRec GetMe();

        protected abstract TRec GetDefault();
    }
}