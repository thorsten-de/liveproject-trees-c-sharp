namespace Tree.Data
{
  public class BinaryNode<T>
  {
    /// <summary>
    /// Left subtree
    /// </summary>
    public BinaryNode<T>? LeftNode { get; private set; } = null;

    /// <summary>
    /// Right subtree
    /// </summary>
    public BinaryNode<T>? RightNode { get; private set; } = null;


    /// <summary>
    /// Data contained in this node
    /// </summary>
    public T Value { get; private set; }

    /// <summary>
    /// Creates a binary tree node with a given value. Because LeftNode 
    /// and RightNode are initialized to null (see property definitions), 
    /// it is implicitly a leaf until subtrees are added.
    /// </summary>
    /// <param name="value">data of this node</param>
    public BinaryNode(T value)
    {
      Value = value;
    }

    /// <summary>
    /// Adds a subtree to this node on the left side.
    /// </summary>
    /// <param name="leftNode">Subtree for the left node</param>
    /// <returns>Itself to be used in a fluent api style</returns>
    public BinaryNode<T> AddLeft(BinaryNode<T> leftNode)
    {
      LeftNode = leftNode;
      return this;
    }

    /// <summary>
    /// Adds a subtree to this node on the right side.
    /// </summary>
    /// <param name="rightNode">Subtree for the right node</param>
    /// <returns>Itself (Fluent Api)</returns>
    public BinaryNode<T> AddRight(BinaryNode<T> rightNode)
    {
      RightNode = rightNode;
      return this;
    }

    /// <summary>
    /// Stringifies this node, actually the whole subtree, recoursively
    /// </summary>
    /// <returns>string reprensentation of this subtree</returns>
    public override string ToString() =>
        $"{Value}: {LeftNode?.Value.ToString() ?? "null"} {RightNode?.Value.ToString() ?? "null"}";

  }
}