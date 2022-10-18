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

using System.Diagnostics;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace sorted_binary_node1
{
  using Node = SortedBinaryNode<int>;

  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {

    private Node Tree { get; set; }


    public Window1()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      RunTests();
    }

    private void addButton_Click(object sender, RoutedEventArgs e)
    {
      Tree.AddNode(int.Parse(ValueTextBox.Text));
      DrawTree();
    }

    private void findButton_Click(object sender, RoutedEventArgs e)
    {
      var node = Tree.FindNode(int.Parse(ValueTextBox.Text));
      MessageBox.Show(node?.Value.ToString() ?? "Value not found.");
    }

    private void removeButton_Click(object sender, RoutedEventArgs e)
    {
      Tree.RemoveNode(int.Parse(ValueTextBox.Text));
      DrawTree();

    }

    private void resetButton_Click(object sender, RoutedEventArgs e)
    {
      ResetTree();
    }

    private void RunTests()
    {
      ResetTree();
      var values = new[] { 60, 35, 76, 21, 42, 71, 89, 17, 24, 74, 11, 23, 72, 75 };
      Array.ForEach(values, Tree.AddNode);
      DrawTree();

      foreach (var value in values)
      {
        Debug.Assert(value == Tree.FindNode(value)?.Value);
      }
      Debug.Assert(Tree.FindNode(12) == null);


    }

    private void ResetTree()
    {
      Tree = new Node(-1);
      DrawTree();
    }

    private void DrawTree()
    {
      mainCanvas.Children.Clear();
      Tree.ArrangeAndDrawSubtree(mainCanvas, 10, 10);
    }
  }
}
