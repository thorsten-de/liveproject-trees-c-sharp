using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Data
{
  public interface ITreeNode<T>
  {
    public T Value { get; }
    public ITreeNode<T> FindNode(T value);
  }
}
