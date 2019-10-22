using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal interface IRecordingTree
    {
        IRecordingResult Result { get; }

        ICollection<IRecordingTree> Children { get; }
    }

    public class TreeNode<T>
    {
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            Value = value;
        }

        public TreeNode()
        {
        }

        public TreeNode<T> Parent { get; private set; }

        public T Value { get; }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public TreeNode<T> InsertChild(TreeNode<T> parent, T value)
        {
            var node = new TreeNode<T>(value)
            {
                Parent = parent
            };
            parent._children.Add(node);
            return node;
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public TreeNode<T> Find(Func<T, bool> matcher)
        {
            if (Value != null && matcher(Value))
            {
                return this;
            }

            foreach (TreeNode<T> child in _children)
            {
                TreeNode<T> result = child.Find(matcher);
                if (result != null)
                {
                    return child;
                }
            }
            return null;
        }

        public TreeNode<T> Find(T value)
        {
            if (value == null)
            {
                return null;
            }
            return Find(v => v?.Equals(value) == true);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
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
    }
}