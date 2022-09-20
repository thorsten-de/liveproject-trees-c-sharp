namespace Tree.Data
{
  /// <summary>
  /// Add some useful extensions for Nullable types
  /// </summary>
  public static class NullableExtensions
  {
    /// <summary>
    /// Transforms a property of a nullable object to its string representation,
    /// or returns a nullValue if the object is null.
    /// </summary>
    /// <param name="this">The nullable object that should be displayed</param>
    /// <param name="propertySelector">Property to get the value from when Nullable is not null</param>
    /// <param name="nullValue">String value to be used when Nullable is null. Defaults to "null"</param>
    /// <returns></returns>
    public static string PropertyOrNullString<T, R>(this T? @this, Func<T, R> propertySelector, string nullValue = "null") =>
      @this == null
        ? nullValue
        : propertySelector(@this).ToString();

    /// <summary>
    /// Transforms the ITreeNode value to its string representation, but only if 
    /// the node is not null. Returns nullValue otherwise.
    /// </summary>
    /// <param name="node">Node object that may be null</param>
    /// <param name="nullValue">String returned when node is null. Defaults to "null"</param>
    /// <returns>String representation of nodes value or nullValue</returns>
    public static string ValueString<T>(this ITreeNode<T>? node, string nullValue = "null") =>
      node.PropertyOrNullString(n => n.Value, nullValue);
  }


  public static class EnumerableExtensions
  {
    public static string JoinToString<T>(this IEnumerable<T> enumerable, string seperator = ", ") =>
      string.Join(seperator, enumerable);
  }
}
