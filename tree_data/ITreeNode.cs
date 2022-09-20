namespace Tree.Data
{
  /// <summary>
  /// A very basic interface for all TreeNode implementations,
  /// simplyfies formatting and printing (see NullableExtensions)
  /// </summary>
  /// <typeparam name="T">Type for node value data</typeparam>
  public interface ITreeNode<T>
  {
    public T Value { get; }
    public IEnumerable<ITreeNode<T>> GetChildren();
  }
}
