
// See https://aka.ms/new-console-template for more information
using Tree.Data;
using BNode = Tree.Data.BinaryNode<string>;
using NNode = Tree.Data.NaryNode<string>;

var FindValue = (ITreeNode<string> tree, string value) =>
{
  var result = tree.FindNode(value);
  if (result != null)
  {
    Console.WriteLine($"Found {result.Value}");
  }
  else
  {
    Console.WriteLine($"Value {value} not found");
  }
};

// Test binary nodes as Action to scope var names
var test_binary_nodes = () =>
{
  var root = new BNode("Root");
  var a = new BNode("A");
  var b = new BNode("B");
  var c = new BNode("C");
  var d = new BNode("D");
  var e = new BNode("E");
  var f = new BNode("F");

  root
    .AddLeft(
      a
      .AddLeft(c)
      .AddRight(d))
    .AddRight(
      b.AddRight(
        e.AddLeft(f))
    );

  Console.WriteLine(root);

  FindValue(root, "Root");
  FindValue(root, "E");
  FindValue(root, "F");
  FindValue(root, "Q");
  
  FindValue(b, "F");
};

// Test N-ary nodes, also wrapped in an Action to scope var names
var test_nary_nodes = () =>
{
  var root = new NNode("Root");
  var a = new NNode("A");
  var b = new NNode("B");
  var c = new NNode("C");
  var d = new NNode("D");
  var e = new NNode("E");
  var f = new NNode("F");
  var g = new NNode("G");
  var h = new NNode("H");
  var i = new NNode("I");

  root
    .AddChild(
      a
      .AddChild(
        d.AddChild(g)
      )
      .AddChild(e)
    )
    .AddChild(b)
    .AddChild(
      c
      .AddChild(
        f
        .AddChild(h)
        .AddChild(i)
      )
    );
  
  Console.WriteLine(root);
  FindValue(root, "Root");
  FindValue(root, "E");
  FindValue(root, "F");
  FindValue(root, "Q");

  FindValue(c, "F");
};

// Run tests
test_binary_nodes(); 
test_nary_nodes();
