using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace nary_node5
{
  internal class NaryNode<T>
  {
    internal T Value { get; set; }
    internal List<NaryNode<T>> Children;

    // New constants and properties go here...

    internal NaryNode(T value)
    {
      Value = value;
      Children = new List<NaryNode<T>>();
    }

    internal void AddChild(NaryNode<T> child)
    {
      Children.Add(child);
    }

    // Return an indented string representation of the node and its children.
    public override string ToString()
    {
      return ToString("");
    }

    // Recursively create a string representation of this node's subtree.
    // Display this value indented, followed its children indented one more level.
    // End in a newline.
    public string ToString(string spaces)
    {
      // Create a string named result that initially holds the
      // current node's value followed by a new line.
      string result = string.Format("{0}{1}:\n", spaces, Value);

      // Add the children representations
      // indented by one more level.
      foreach (NaryNode<T> child in Children)
        result += child.ToString(spaces + "  ");
      return result;
    }

    // Recursively search this node's subtree looking for the target value.
    // Return the node that contains the value or null.
    internal NaryNode<T> FindNode(T target)
    {
      // See if this node contains the value.
      if (Value.Equals(target)) return this;

      // Search the child subtrees.
      foreach (NaryNode<T> child in Children)
      {
        NaryNode<T> result = child.FindNode(target);
        if (result != null) return result;
      }

      // We did not find the value. Return null.
      return null;
    }

    internal List<NaryNode<T>> TraversePreorder()
    {
      List<NaryNode<T>> result = new List<NaryNode<T>>();

      // Add this node to the traversal.
      result.Add(this);

      // Add the children.
      foreach (NaryNode<T> child in Children)
      {
        result.AddRange(child.TraversePreorder());
      }

      return result;
    }

    internal List<NaryNode<T>> TraversePostorder()
    {
      List<NaryNode<T>> result = new List<NaryNode<T>>();

      // Add the children.
      foreach (NaryNode<T> child in Children)
      {
        result.AddRange(child.TraversePreorder());
      }

      // Add this node to the traversal.
      result.Add(this);
      return result;
    }

    internal List<NaryNode<T>> TraverseBreadthFirst()
    {
      List<NaryNode<T>> result = new List<NaryNode<T>>();
      Queue<NaryNode<T>> queue = new Queue<NaryNode<T>>();

      // Start with the top node in the queue.
      queue.Enqueue(this);
      while (queue.Count > 0)
      {
        // Remove the top node from the queue and
        // add it to the result list.
        NaryNode<T> node = queue.Dequeue();
        result.Add(node);

        // Add the node's children to the queue.
        foreach (NaryNode<T> child in node.Children)
          queue.Enqueue(child);
      }

      return result;
    }

    public const double BOX_HALF_WIDTH = 80 / 2;
    public const double BOX_HALF_HEIGHT = 40 / 2;
    public const double X_SPACING = 20;
    public const double Y_SPACING = 20;
    public readonly Brush LINK_BRUSH = Brushes.Green;
    public readonly Brush NODE_STROKE = Brushes.Black;
    public readonly Brush NODE_BG = Brushes.White;
    public readonly Brush NODE_FG = Brushes.Red;
    public readonly Brush SUBTREE_BOUNDS_STROKE = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
    public const double LINK_THICKNESS = 2.0;
    public const double NODE_THICKNESS = 2.0;

    public Point Center { get; private set; }
    public Rect SubtreeBounds { get; private set; }

    private Rect NodeBounds(double xmin, double ymin) => new Rect(xmin, ymin, 2 * BOX_HALF_WIDTH, 2 * BOX_HALF_HEIGHT);


    public bool IsLeaf
    {
      get => !Children.Any();
    }

    public bool IsTwig
    {
      get => Children.All(c => c.IsLeaf);
    }

    private void ArrangeSubtree(double xmin, double ymin)
    {
      double childXmin = xmin;
      double childYmin = ymin + 2 * BOX_HALF_HEIGHT + Y_SPACING;

      SubtreeBounds =
        Children
        .Aggregate(NodeBounds(xmin, ymin), (bounds, node) =>
        {
          node.ArrangeSubtree(childXmin, childYmin);
          childXmin += node.SubtreeBounds.Width + X_SPACING;

          return Rect.Union(bounds, node.SubtreeBounds);
        });

      Center = new Point(xmin + SubtreeBounds.Width /2, ymin + BOX_HALF_HEIGHT);
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
      double centerY = Center.Y + BOX_HALF_HEIGHT + Y_SPACING / 2;
      var corner1 = new Point(Center.X, centerY);

      foreach (var node in Children)
      {
        var corner2 = new Point(node.Center.X, centerY);
        canvas.DrawLine(Center, corner1, LINK_BRUSH, LINK_THICKNESS);
        canvas.DrawLine(corner1, corner2, LINK_BRUSH, LINK_THICKNESS);
        canvas.DrawLine(corner2, node.Center, LINK_BRUSH, LINK_THICKNESS);
        node.DrawSubtreeLinks(canvas);
      }
    }

    private void DrawSubtreeNodes(Canvas canvas)
    {
      //canvas.DrawRectangle(SubtreeBounds, Brushes.Transparent, SUBTREE_BOUNDS_STROKE, 1); // Show calculated bounds

      var nodeBG = IsLeaf ? NODE_BG : Brushes.LightPink;

      var nodeBounds = NodeBounds(Center.X, Center.Y);
      nodeBounds.Offset(-BOX_HALF_WIDTH, -BOX_HALF_HEIGHT);
      canvas.DrawRectangle(nodeBounds, nodeBG, NODE_STROKE, NODE_THICKNESS);
      canvas.DrawLabel(nodeBounds, Value, Brushes.Transparent, NODE_FG, HorizontalAlignment.Center, VerticalAlignment.Center, 11, 4);

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
