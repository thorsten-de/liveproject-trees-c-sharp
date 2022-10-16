using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nary_node5
{
  using Node = NaryNode<string>;

  static class EnumerableExtensions
  {
    /// <summary>
    /// For easier mapping of actions over an array, like List<T>.ForEach
    /// </summary>
    public static void ForEach<T>(this T[] @this, Action<T> action) =>
      Array.ForEach(@this, action);
  }
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      // Build a test tree.
      // A
      //         |
      //     +---+---+
      // B   C   D
      //     |       |
      //    +      +-+-+
      //       I J K
      NaryNode<string> node_a = new NaryNode<string>("GeneriGloop");
      NaryNode<string> node_b = new NaryNode<string>("R & D");
      new[] {
        new Node("Applied"),
        new Node("Basic"),
        new Node("Advanced"),
        new Node("Sci Fi"),
      }.ForEach(node_b.AddChild);

      NaryNode<string> node_c = new NaryNode<string>("Sales");
      new[]
      {
        new Node("Inside Sales"),
        new Node("Outside Sales"),
        new Node("B2B"),
        new Node("Consumer"),
        new Node("Account Management")
      }.ForEach(node_c.AddChild);

      NaryNode<string> node_d = new NaryNode<string>("Professional Services");

      NaryNode<string> node_i = new NaryNode<string>("HR");
      new[] {
        new Node("Training"),
        new Node("Hiring"),
        new Node("Equity"),
        new Node("Discipline"),
      }.ForEach(node_i.AddChild);
      NaryNode<string> node_j = new NaryNode<string>("Accounting");
      new[] {
        new Node("Payroll"),
        new Node("Billing"),
        new Node("Reporting"),
        new Node("Opacity"),
      }.ForEach(node_j.AddChild);
      NaryNode<string> node_k = new NaryNode<string>("Legal");
      new[] {
        new Node("Compliance"),
        new Node("Progress Prevention"),
        new Node("Bial Services"),
      }.ForEach(node_k.AddChild);

      node_a.AddChild(node_b);
      node_a.AddChild(node_c);
      node_a.AddChild(node_d);
      node_d.AddChild(node_i);
      node_d.AddChild(node_j);
      node_d.AddChild(node_k);

      // Draw the tree.
      node_a.ArrangeAndDrawSubtree(mainCanvas, 10, 10);
    }
  }
}
