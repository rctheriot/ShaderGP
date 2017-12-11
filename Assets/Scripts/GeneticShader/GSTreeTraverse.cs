using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GeneticShader
{

  public static class GSTreeTraverse
  {

    public static StringBuilder TraverseTreeWrite(GSTreeNode node, StringBuilder value)
    {
      if (node == null)
      {
        return value;
      }
      else if (GSDictionaries.BasicFunctions.ContainsKey(node.value))
      {
        value.Append("(");
        TraverseTreeWrite(node.left, value);
        value.Append(GSDictionaries.BasicFunctions[node.value]);
        TraverseTreeWrite(node.right, value);
        value.Append(")");
      }
      else if (GSDictionaries.DoubleArgFunctions.ContainsKey(node.value))
      {
        value.Append(GSDictionaries.DoubleArgFunctions[node.value]);
        value.Append("(");
        TraverseTreeWrite(node.left, value);
        value.Append(",");
        TraverseTreeWrite(node.right, value);
        value.Append(")");
      }
      else if (GSDictionaries.SingleArgFunctions.ContainsKey(node.value))
      {
        value.Append(GSDictionaries.SingleArgFunctions[node.value]);
        value.Append("(");
        TraverseTreeWrite(node.left, value);
        value.Append(")");
      }
      else
      {
        TraverseTreeWrite(node.left, value);
        value.Append(node.value);
        TraverseTreeWrite(node.right, value);
      }
      return value;
    }

    public static List<GSTreeNode> GetAllFunctionNodes(GSTreeNode node, List<GSTreeNode> nodes)
    {
      if (node == null)
      {
        return nodes;
      }
      else
      {
        if (GSDictionaries.BasicFunctions.ContainsKey(node.value) ||
            GSDictionaries.DoubleArgFunctions.ContainsKey(node.value) ||
            GSDictionaries.SingleArgFunctions.ContainsKey(node.value))
        {
          nodes.Add(node);
        }
        GetAllFunctionNodes(node.left, nodes);
        GetAllFunctionNodes(node.right, nodes);
        return nodes;
      }
    }

    public static List<GSTreeNode> GetSingleArgFunctionNodes(GSTreeNode node, List<GSTreeNode> nodes)
    {
      if (node == null)
      {
        return nodes;
      }
      else
      {
        if (GSDictionaries.SingleArgFunctions.ContainsKey(node.value))
        {
          nodes.Add(node);
        }
        GetSingleArgFunctionNodes(node.left, nodes);
        GetSingleArgFunctionNodes(node.right, nodes);
        return nodes;
      }
    }

    public static List<GSTreeNode> GetDoubleArgFunctionNodes(GSTreeNode node, List<GSTreeNode> nodes)
    {
      if (node == null)
      {
        return nodes;
      }
      else
      {
        if (GSDictionaries.BasicFunctions.ContainsKey(node.value) ||
            GSDictionaries.DoubleArgFunctions.ContainsKey(node.value))
        {
          nodes.Add(node);
        }
        GetDoubleArgFunctionNodes(node.left, nodes);
        GetDoubleArgFunctionNodes(node.right, nodes);
        return nodes;
      }
    }
    
    public static List<GSTreeNode> GetLeafNodes(GSTreeNode node, List<GSTreeNode> nodes)
    {
      if (node == null)
      {
        return nodes;
      }
      else
      {
        if (!GSDictionaries.BasicFunctions.ContainsKey(node.value) &&
            !GSDictionaries.DoubleArgFunctions.ContainsKey(node.value) &&
            !GSDictionaries.SingleArgFunctions.ContainsKey(node.value))
        {
          nodes.Add(node);
        }
        GetAllFunctionNodes(node.left, nodes);
        GetAllFunctionNodes(node.right, nodes);
        return nodes;
      }
    }

  }
}