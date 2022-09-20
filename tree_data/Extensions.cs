using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Tree.Data
{
  public static class EnumerableExtensions
  {
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string seperator = ", ") =>
      string.Join(seperator, enumerable);
  }


  public static class StringbuilderExtensions
  {
    public static StringBuilder Apply(this StringBuilder @this, Func<StringBuilder, StringBuilder> f) =>
      f(@this);
  }

}
