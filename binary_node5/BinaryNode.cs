using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq.Expressions;
using System.IO;
using System.Xaml.Schema;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace binary_node5
{
  internal class BinaryNode<T>
  {
    internal T Value { get; set; }
    internal BinaryNode<T> LeftChild, RightChild;

    // New constants and properties go here...

    internal BinaryNode(T value)
    {
      Value = value;
      LeftChild = null;
      RightChild = null;
    }

    internal void AddLeft(BinaryNode<T> child)
    {
      LeftChild = child;
    }

    internal void AddRight(BinaryNode<T> child)
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

    // Recursively search this node's subtree looking for the target value.
    // Return the node that contains the value or null.
    internal BinaryNode<T> FindNode(T target)
    {
      // See if this node contains the value.
      if (Value.Equals(target)) return this;

      // Search the left child subtree.
      BinaryNode<T> result = null;
      if (LeftChild != null)
        result = LeftChild.FindNode(target);
      if (result != null) return result;

      // Search the right child subtree.
      if (RightChild != null)
        result = RightChild.FindNode(target);
      if (result != null) return result;

      // We did not find the value. Return null.
      return null;
    }

    internal List<BinaryNode<T>> TraversePreorder()
    {
      List<BinaryNode<T>> result = new List<BinaryNode<T>>();

      // Add this node to the traversal.
      result.Add(this);

      // Add the child subtrees.
      if (LeftChild != null) result.AddRange(LeftChild.TraversePreorder());
      if (RightChild != null) result.AddRange(RightChild.TraversePreorder());
      return result;
    }

    internal List<BinaryNode<T>> TraverseInorder()
    {
      List<BinaryNode<T>> result = new List<BinaryNode<T>>();

      // Add the left subtree.
      if (LeftChild != null) result.AddRange(LeftChild.TraverseInorder());

      // Add this node.
      result.Add(this);

      // Add the right subtree.
      if (RightChild != null) result.AddRange(RightChild.TraverseInorder());
      return result;
    }

    internal List<BinaryNode<T>> TraversePostorder()
    {
      List<BinaryNode<T>> result = new List<BinaryNode<T>>();

      // Add the child subtrees.
      if (LeftChild != null) result.AddRange(LeftChild.TraversePostorder());
      if (RightChild != null) result.AddRange(RightChild.TraversePostorder());

      // Add this node.
      result.Add(this);
      return result;
    }

    internal List<BinaryNode<T>> TraverseBreadthFirst()
    {
      List<BinaryNode<T>> result = new List<BinaryNode<T>>();
      Queue<BinaryNode<T>> queue = new Queue<BinaryNode<T>>();

      // Start with the top node in the queue.
      queue.Enqueue(this);
      while (queue.Count > 0)
      {
        // Remove the top node from the queue and
        // add it to the result list.
        BinaryNode<T> node = queue.Dequeue();
        result.Add(node);

        // Add the node's children to the queue.
        if (node.LeftChild != null) queue.Enqueue(node.LeftChild);
        if (node.RightChild != null) queue.Enqueue(node.RightChild);
      }

      return result;
    }

    public const int NODE_RADIUS = 10;
    public const int X_SPACING = 20;
    public const int Y_SPACING = 20;
    public readonly Brush LINK_BRUSH = Brushes.Black;
    public readonly Brush NODE_STROKE = Brushes.Green;
    public readonly Brush NODE_BG = Brushes.White;
    public readonly Brush NODE_FG = Brushes.Red;
    public readonly Brush SUBTREE_BOUNDS_STROKE = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
    public const double LINK_THICKNESS = 2.0;
    public const double NODE_THICKNESS = 2.0;

    public Point Center { get; private set; }
    public Rect SubtreeBounds { get; private set; }

    private IEnumerable<BinaryNode<T>> Children
    {
      get
      {
        if (LeftChild != null)
          yield return LeftChild;
        if (RightChild != null)
          yield return RightChild;
      }
    }

    private Rect NodeBounds(double xmin, double ymin) => new Rect(xmin, ymin, 2 * NODE_RADIUS, 2 * NODE_RADIUS);

    private void ArrangeSubtree(double xmin, double ymin)
    {
      double childXmin = xmin;
      double childYmin = ymin + 2 * NODE_RADIUS + Y_SPACING;

      SubtreeBounds =
        Children
        .Aggregate(NodeBounds(xmin, ymin), (bounds, node) =>
        {
          node.ArrangeSubtree(childXmin, childYmin);
          childXmin += node.SubtreeBounds.Width + X_SPACING;

          return Rect.Union(bounds, node.SubtreeBounds);
        });

      double leftNodeX = Children.FirstOrDefault()?.Center.X ?? xmin;
      double rightNodeX = Children.LastOrDefault()?.Center.X ?? xmin + 2 * NODE_RADIUS;
      Center = new Point((leftNodeX + rightNodeX) / 2, ymin + NODE_RADIUS);
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
      foreach (var node in Children)
      {
        canvas.DrawLine(Center, node.Center, LINK_BRUSH, LINK_THICKNESS);
        node.DrawSubtreeLinks(canvas);
      }
    }

    private void DrawSubtreeNodes(Canvas canvas)
    {
      // canvas.DrawRectangle(SubtreeBounds, Brushes.Transparent, SUBTREE_BOUNDS_STROKE, 1); // Show calculated bounds

      var nodeBounds = NodeBounds(Center.X, Center.Y);
      nodeBounds.Offset(-NODE_RADIUS, -NODE_RADIUS);
      canvas.DrawEllipse(nodeBounds, NODE_BG, NODE_STROKE, NODE_THICKNESS);
      canvas.DrawLabel(nodeBounds, Value, Brushes.Transparent, NODE_FG, HorizontalAlignment.Center, VerticalAlignment.Center, NODE_RADIUS, 0);

      foreach (var node in Children)
      {
        node.DrawSubtreeNodes(canvas);
      }
    }

    public void ArrangeAndDrawSubtree(Canvas canvas, double xmin, double ymin)
    {
      ArrangeSubtree(xmin, ymin);
      DrawSubtreeLinks(canvas);
      DrawSubtreeNodes(canvas);
    }
  }
}
