using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Data
{
  public static class TraversalExtensions
  {
    public static IEnumerable<ITreeNode<T>> TraverseBreathFirst<T>(this ITreeNode<T> @this)
    {
      var result = new List<ITreeNode<T>>();
      var queue = new Queue<ITreeNode<T>>();

      queue.Enqueue(@this);

      while (queue.Any())
      {
        var node = queue.Dequeue();
        result.Add(node);
        node.Children.Each(queue.Enqueue);
      }

      return result;
    }

    public static IEnumerable<ITreeNode<T>> TraversePreorder<T>(this ITreeNode<T> @this)
    {
      var result = new List<ITreeNode<T>>();
      result.Add(@this);
      @this.Children
        .Select(c => c.TraversePreorder())
        .Each(result.AddRange);

      return result;
    }

    public static IEnumerable<ITreeNode<T>> TraversePostorder<T>(this ITreeNode<T> @this)
    {
      var result = new List<ITreeNode<T>>();
      @this.Children
        .Select(c => c.TraversePostorder())
        .Each(result.AddRange);
      result.Add(@this);

      return result;
    }
  }
}
