namespace Tree.Data
{
  public static class EnumerableExtensions
  {
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string seperator = ", ") =>
      string.Join(seperator, enumerable);
  }
}
