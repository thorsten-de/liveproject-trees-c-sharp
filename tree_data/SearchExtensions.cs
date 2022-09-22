using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Data
{
  public static class SearchExtensions
  {
    public static ITreeNode<T> FindNode<T>(this ITreeNode<T> @this, T value)
    {
      if (value.Equals(@this.Value))
      {
        return @this;
      }

      foreach (var node in @this.Children)
      {
        var result = node.FindNode(value);
        if (result != null)
        {
          return result;
        }
      }

      return null;
    }
  }
}
