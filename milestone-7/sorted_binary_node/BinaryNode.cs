﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Net.Security;

namespace sorted_binary_node1
{
  internal class SortedBinaryNode<T> where T : IComparable<T>
  {
    internal T Value { get; set; }
    internal SortedBinaryNode<T> LeftChild, RightChild;

    // Size and position values.
    private const double NODE_RADIUS = 10;  // Radius of a node’s circle
    private const double X_SPACING = 20;    // Horizontal distance between neighboring subtrees
    private const double Y_SPACING = 20;    // Horizontal distance between parent and child subtree
    internal Point Center { get; private set; }
    internal Rect SubtreeBounds { get; private set; }

    internal SortedBinaryNode(T value)
    {
      Value = value;
      LeftChild = null;
      RightChild = null;
    }

    internal void AddLeft(SortedBinaryNode<T> child)
    {
      LeftChild = child;
    }

    internal void AddRight(SortedBinaryNode<T> child)
    {
      RightChild = child;
    }

    // Return an indented string representation of the node and its children.
    public override string ToString()
    {
      return ToString("");
    }

    // Recursively create a string representation of this node's subtree.
    // Display this value indented, followed by the left and right
    // values indented one more level.
    // End in a newline.
    public string ToString(string spaces)
    {
      // Create a string named result that initially holds the
      // current node's value followed by a new line.
      string result = string.Format("{0}{1}:\n", spaces, Value);

      // See if the node has any children.
      if ((LeftChild != null) || (RightChild != null))
      {
        // Add null or the child's value.
        if (LeftChild == null)
          result += string.Format("{0}{1}null\n", spaces, "  ");
        else
          result += LeftChild.ToString(spaces + "  ");

        // Add null or the child's value.
        if (RightChild == null)
          result += string.Format("{0}{1}null\n", spaces, "  ");
        else
          result += RightChild.ToString(spaces + "  ");
      }
      return result;
    }

    internal List<SortedBinaryNode<T>> TraversePreorder()
    {
      List<SortedBinaryNode<T>> result = new List<SortedBinaryNode<T>>();

      // Add this node to the traversal.
      result.Add(this);

      // Add the child subtrees.
      if (LeftChild != null) result.AddRange(LeftChild.TraversePreorder());
      if (RightChild != null) result.AddRange(RightChild.TraversePreorder());
      return result;
    }

    internal List<SortedBinaryNode<T>> TraverseInorder()
    {
      List<SortedBinaryNode<T>> result = new List<SortedBinaryNode<T>>();

      // Add the left subtree.
      if (LeftChild != null) result.AddRange(LeftChild.TraverseInorder());

      // Add this node.
      result.Add(this);

      // Add the right subtree.
      if (RightChild != null) result.AddRange(RightChild.TraverseInorder());
      return result;
    }

    internal List<SortedBinaryNode<T>> TraversePostorder()
    {
      List<SortedBinaryNode<T>> result = new List<SortedBinaryNode<T>>();

      // Add the child subtrees.
      if (LeftChild != null) result.AddRange(LeftChild.TraversePostorder());
      if (RightChild != null) result.AddRange(RightChild.TraversePostorder());

      // Add this node.
      result.Add(this);
      return result;
    }

    internal List<SortedBinaryNode<T>> TraverseBreadthFirst()
    {
      List<SortedBinaryNode<T>> result = new List<SortedBinaryNode<T>>();
      Queue<SortedBinaryNode<T>> queue = new Queue<SortedBinaryNode<T>>();

      // Start with the top node in the queue.
      queue.Enqueue(this);
      while (queue.Count > 0)
      {
        // Remove the top node from the queue and
        // add it to the result list.
        SortedBinaryNode<T> node = queue.Dequeue();
        result.Add(node);

        // Add the node's children to the queue.
        if (node.LeftChild != null) queue.Enqueue(node.LeftChild);
        if (node.RightChild != null) queue.Enqueue(node.RightChild);
      }

      return result;
    }

    // Position the node's subtree.
    private void ArrangeSubtree(double xmin, double ymin)
    {
      // Calculate cy, the Y coordinate for this node.
      // This doesn't depend on the children.
      double cy = ymin + NODE_RADIUS;

      // If the node has no children, just place it here and return.
      if ((LeftChild == null) && (RightChild == null))
      {
        double cx = xmin + NODE_RADIUS;
        Center = new Point(cx, cy);
        SubtreeBounds = new Rect(xmin, ymin, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
        return;
      }

      // Set child_xmin and child_ymin to the
      // start position for child subtrees.
      double childXmin = xmin;
      double childYmin = ymin + 2 * NODE_RADIUS + Y_SPACING;

      // Position the child subtrees.
      if (LeftChild != null)
      {
        // Arrange the left child subtree and update
        // child_xmin to allow room for its subtree.
        LeftChild.ArrangeSubtree(childXmin, childYmin);
        childXmin = LeftChild.SubtreeBounds.Right;

        // If we also have a right child,
        // add space between their subtrees.
        if (RightChild != null) childXmin += X_SPACING;
      }

      if (RightChild != null)
      {
        // Arrange the right child subtree.
        RightChild.ArrangeSubtree(childXmin, childYmin);
      }

      // Arrange this node depending on the number of children.
      if ((LeftChild != null) && (RightChild != null))
      {
        // Two children. Center this node over the child nodes.
        // Use the subtree bounds to set our subtree bounds.
        double cx = (LeftChild.Center.X + RightChild.Center.X) / 2;
        Center = new Point(cx, cy);
        double xmax = RightChild.SubtreeBounds.Right;
        double ymax = Math.Max(LeftChild.SubtreeBounds.Bottom, RightChild.SubtreeBounds.Bottom);
        SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
      }
      else if (LeftChild != null)
      {
        // We have only a left child.
        double cx = LeftChild.Center.X;
        Center = new Point(cx, cy);
        double xmax = LeftChild.SubtreeBounds.Right;
        double ymax = LeftChild.SubtreeBounds.Bottom;
        SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
      }
      else
      {
        // We have only a right child.
        double cx = RightChild.Center.X;
        Center = new Point(cx, cy);
        double xmax = RightChild.SubtreeBounds.Right;
        double ymax = RightChild.SubtreeBounds.Bottom;
        SubtreeBounds = new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
      }
    }

    // Draw the subtree's links.
    private void DrawSubtreeLinks(Canvas canvas)
    {
      // Draw the subtree's links.
      if (LeftChild != null)
      {
        LeftChild.DrawSubtreeLinks(canvas);
        canvas.DrawLine(Center, LeftChild.Center, Brushes.Black, 1);
      }

      if (RightChild != null)
      {
        RightChild.DrawSubtreeLinks(canvas);
        canvas.DrawLine(Center, RightChild.Center, Brushes.Black, 1);
      }

      // Outline the subtree for debugging.
      //canvas.DrawRectangle(SubtreeBounds, null, Brushes.Red, 1);
    }

    // Draw the subtree's nodes.
    private void DrawSubtreeNodes(Canvas canvas)
    {
      // Draw the node.
      double x0 = Center.X - NODE_RADIUS;
      double y0 = Center.Y - NODE_RADIUS;
      Rect rect = new Rect(
          Center.X - NODE_RADIUS,
          Center.Y - NODE_RADIUS,
          2 * NODE_RADIUS,
          2 * NODE_RADIUS);
      canvas.DrawEllipse(rect, Brushes.White, Brushes.Green, 1);

      Label label = canvas.DrawLabel(
          rect, Value, null, Brushes.Red,
          HorizontalAlignment.Center,
          VerticalAlignment.Center,
          12, 0);

      // Draw the descendants' nodes.
      if (LeftChild != null) LeftChild.DrawSubtreeNodes(canvas);
      if (RightChild != null) RightChild.DrawSubtreeNodes(canvas);
    }

    // Position and draw the subtree.
    public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
    {
      // Position the tree.
      ArrangeSubtree(xmin, ymin);

      // Draw the links.
      DrawSubtreeLinks(canvas);

      // Draw the nodes.
      DrawSubtreeNodes(canvas);
    }

    public void AddNode(T value)
    {
      switch (value.CompareTo(this.Value))
      {
        case 0:
          throw new ArgumentException("Value is already there.");
        case -1:
          if (LeftChild == null)
          {
            LeftChild = new SortedBinaryNode<T>(value);
          }
          else
          {
            LeftChild.AddNode(value);
          }
          break;

        case 1:
          if (RightChild == null)
          {
            RightChild = new SortedBinaryNode<T>(value);
          }
          else
          {
            RightChild.AddNode(value);
          }
          break;
      }
    }

    public SortedBinaryNode<T> FindNode(T value)
    {
      switch (value.CompareTo(this.Value))
      {
        case 0:
          return this;
        case -1:
          return LeftChild?.FindNode(value);
        case 1:
          return RightChild?.FindNode(value);
      }

      return null;
    }

    public void RemoveNode(T value)
    {
      var node = this;
      SortedBinaryNode<T> parent = null;
      bool found = false;
      do
      {
        switch (value.CompareTo(node.Value))
        {
          case 0:
            found = true; break;
          case -1:
            parent = node;
            node = node.LeftChild;
            break;
          case 1:
            parent = node;
            node = node.RightChild;
            break;
        }

      } while (!found && node != null);
      if (node == null) throw new ArgumentException("Value not in tree.");

      var x = node;

      if (node.LeftChild == null) {
        x = x.RightChild;
      } else if (node.RightChild == null) {
        x = x.LeftChild;
      }
      else if (node.LeftChild.RightChild == null)
      {
        x = x.LeftChild;
        x.RightChild = node.RightChild;
      }
      else
      {
        var c = x.LeftChild;
        while (c.RightChild.RightChild != null)
        {
          c = c.RightChild;
        }
        x = c.RightChild;
        c.RightChild = x.LeftChild;
        x.RightChild = node.RightChild;
        x.LeftChild = node.LeftChild;
      }

      if (node.Value.CompareTo(parent.Value) < 0) {
        parent.LeftChild = x;
      } else
      {
        parent.RightChild = x;
      }

    }
  }
}
