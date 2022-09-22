using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Tree.Data
{
  public static class EnumerableExtensions
  {
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string Separator = ", ") =>
      string.Join(Separator, enumerable);

    public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, Action<T> f)
    {
      foreach (var item in enumerable)
      {
        f(item);
      }
      return enumerable;
    }
  }


  public static class StringbuilderExtensions
  {
    public static StringBuilder Apply(this StringBuilder @this, Func<StringBuilder, StringBuilder> f) =>
      f(@this);
  }

}
