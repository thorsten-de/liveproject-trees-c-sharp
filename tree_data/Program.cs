
// See https://aka.ms/new-console-template for more information
using BNode = Tree.Data.BinaryNode<string>;

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

var nodes = new[] { root, a, b, c, d, e, f };
foreach (var node in nodes)
{
  Console.WriteLine(node);
}
