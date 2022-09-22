using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Data
{
  public interface ITreeNode<T>
  {
    public T Value { get; }
    public IEnumerable<ITreeNode<T>> Children { get; }
  }
}
