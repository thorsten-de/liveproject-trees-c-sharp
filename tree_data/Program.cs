
// See https://aka.ms/new-console-template for more information
using BNode = Tree.Data.BinaryNode<string>;
using NNode = Tree.Data.NaryNode<string>;


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
  Console.WriteLine(a);
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

  var nodes = new[] { root, a, b, c, d, e, f, g, h, i };
  foreach (var node in nodes)
  {
    Console.WriteLine(node);
  }
};

// Run tests
test_binary_nodes();
test_nary_nodes();
