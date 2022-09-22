using System.Text;
using System.Xml.Serialization;

namespace Tree.Data
{
  public class BinaryNode<T> : ITreeNode<T>
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

    public IEnumerable<BinaryNode<T>> TraverseInorder()
    {
      var result = new List<BinaryNode<T>>();
      if (LeftNode != null) result.AddRange(LeftNode.TraverseInorder());
      result.Add(this);
      if (RightNode != null) result.AddRange(RightNode.TraverseInorder());
      return result;
    }


    // When all not-null branches are needed, behave like a n-ary tree with n=2
    public IEnumerable<ITreeNode<T>> Children
    {
      get
      {
        if (LeftNode != null) yield return LeftNode;
        if (RightNode != null) yield return RightNode;
      }
    }

    public override string ToString() =>
      ToString("");

    /// <summary>
    /// Stringifies this node, actually the whole subtree, recoursively
    /// </summary>
    /// <returns>string outline of this subtree</returns>
    private string ToString(string spaces)
    {
      string nextLevelSpacing = spaces + "  ";

      // build a function that appends lines to a string builder
      // that is based on the presence of Left/Right child nodes
      Func<StringBuilder, StringBuilder> outlineChildren;
      switch ((LeftNode, RightNode))
      {
        case (BinaryNode<T> left, BinaryNode<T> right):
          outlineChildren = (StringBuilder sb) =>
            sb.Append(left.ToString(nextLevelSpacing))
              .Append(right.ToString(nextLevelSpacing));
          break;


        case (BinaryNode<T> left, null):
          outlineChildren = (StringBuilder sb) =>
            sb.Append(left.ToString(nextLevelSpacing))
              .Append(nextLevelSpacing).AppendLine("None");
          break;

        case (null, BinaryNode<T> right):
          outlineChildren = (StringBuilder sb) =>
            sb.Append(nextLevelSpacing).AppendLine("None")
              .Append(right.ToString(nextLevelSpacing));
          break;


        case (null, null):
          outlineChildren = (StringBuilder sb) => sb;
          break;
      }

      return new StringBuilder(spaces)
        .Append(Value)
        .Append(":\n")
        .Apply(outlineChildren)
        .ToString();
    }


  }
}