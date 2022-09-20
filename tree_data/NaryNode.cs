﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Data
{
  /// <summary>
  /// N-ary tree node class
  /// </summary>
  /// <typeparam name="T">type of value data</typeparam>
  public class NaryNode<T> : ITreeNode<T>
  {
    /// <summary>
    /// Value of this node
    /// </summary>
    public T Value { get; private set; }

    /// <summary>
    /// Children (Subtrees) of this node
    /// </summary>
    public List<NaryNode<T>> Children { get; private set; }

    public NaryNode(T value)
    {
      Value = value;
      Children = new List<NaryNode<T>>();
    }

    /// <summary>
    /// Adds a new child to this node
    /// </summary>
    /// <param name="child">Subtree to be added as a child</param>
    /// <returns>Itself, for fluent API</returns>
    public NaryNode<T> AddChild(NaryNode<T> child)
    {
      Children.Add(child);
      return this;
    }

    public override string ToString() =>
      $"{Value}: {Children.Select(n => n.Value).JoinToString(" ")}";

    public IEnumerable<ITreeNode<T>> GetChildren()
    {
      return Children;
    }
  }
}