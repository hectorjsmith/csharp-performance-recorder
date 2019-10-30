﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal abstract class TreeNodeImpl<TValue, TRec> : ITreeNode<TValue, TRec> where TRec : ITreeNode<TValue, TRec>
    {
        private readonly List<TRec> _children = new List<TRec>();

        public int ChildCount => _children.Count;

        public TreeNodeImpl(TValue value)
        {
            Value = value;
        }

        public TreeNodeImpl()
        {
        }

        public TRec Parent { get; protected set; }

        public TValue Value { get; }

        public TRec AddChild(TValue value)
        {
            TRec node = GetNew(value, GetMe());
            _children.Add(node);
            return node;
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

        public TRec Find(Func<TRec, bool> matcher)
        {
            if (matcher(GetMe()))
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
            return Find((TRec node) => node.Value?.Equals(value) == true);
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