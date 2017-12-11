using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GeneticShader
{
  [System.Serializable]
  public class GSTree
  {

    GSTreeNode root;

    bool positionTree;

    float rangeMin = 0.0f;
    float rangeMax = 0.15f;

    public GSTree(bool positionTree)
    {
      root = new GSTreeNode();
      GSTreeNode left = new GSTreeNode();
      GSTreeNode right = new GSTreeNode();
      root.value = GSDictionaries.GetRandomDoubleArgFunction();
      left.value = "(" + GetRandomFloat() + ")";
      right.value = "(" + GetRandomFloat() + ")";
      root.left = left;
      root.right = right;
      this.positionTree = positionTree;
    }

    public string WriteTree()
    {
      StringBuilder sb = new StringBuilder();
      GSTreeTraverse.TraverseTreeWrite(root, sb);
      return sb.ToString();
    }

    public void MutateRandomNode()
    {
      List<GSTreeNode> nodes = new List<GSTreeNode>();
      GSTreeTraverse.GetAllFunctionNodes(root, nodes);
      System.Random rand = new System.Random();
      GSTreeNode node = nodes[rand.Next(nodes.Count)];

      if (GSDictionaries.BasicFunctions.ContainsKey(node.value) ||
          GSDictionaries.DoubleArgFunctions.ContainsKey(node.value))
      {
        node.value = GSDictionaries.GetDifferentDoubleArgFunction(node.value);
      }
      else if (GSDictionaries.SingleArgFunctions.ContainsKey(node.value))
      {
        node.value = GSDictionaries.GetDifferentSingleArgFunction(node.value);
      }
    }

    public void PermutateRandomNode()
    {
      List<GSTreeNode> nodes = new List<GSTreeNode>();
      GSTreeTraverse.GetDoubleArgFunctionNodes(root, nodes);
      System.Random rand = new System.Random();
      GSTreeNode node = nodes[rand.Next(nodes.Count)];
      GSTreeNode temp = node.left;
      node.left = node.right;
      node.right = temp;
    }

    public void CrossoverRandom(GSTree treeB)
    {
      List<GSTreeNode> nodes = new List<GSTreeNode>();
      GSTreeTraverse.GetDoubleArgFunctionNodes(treeB.root, nodes);
      System.Random rand = new System.Random();
      GSTreeNode nodeB = nodes[rand.Next(nodes.Count)];

      nodes = new List<GSTreeNode>();
      GSTreeTraverse.GetDoubleArgFunctionNodes(root, nodes);
      rand = new System.Random();
      GSTreeNode nodeA = nodes[rand.Next(nodes.Count)];

      GSTreeNode newTree = new GSTreeNode();
      newTree.left = nodeB.left;
      newTree.right = nodeA.right;
      nodeA = (GSTreeNode)newTree.Clone();

    }

    public void InsertRandomNode()
    {
      System.Random rand = new System.Random();
      List<GSTreeNode> nodes = new List<GSTreeNode>();
      GSTreeTraverse.GetDoubleArgFunctionNodes(root, nodes);
      GSTreeNode selNode = nodes[rand.Next(nodes.Count)];
      GSTreeNode tempNode = selNode.left;

      GSTreeNode newNode = new GSTreeNode();
      selNode.left = newNode;

      if (rand.Next(0, 2) == 1)
      {
        newNode.value = GSDictionaries.GetRandomDoubleArgFunction();
        newNode.left = tempNode;
        newNode.right = new GSTreeNode();
        string randValue;
        if (rand.Next(0, 2) == 1)
        {
          randValue = "(" + GetRandomFloat() + ")";
        }
        else
        {
          if (positionTree)
          {
            randValue = "(" + GSDictionaries.GetRandomPositionVariable() + ")";
          }
          else
          {
            randValue = "(" + GSDictionaries.GetRandomColorVariable() + ")";
          }
        }
        newNode.right.value = randValue;
      }
      else
      {
        newNode.value = GSDictionaries.GetRandomSingleArgFunction();
        newNode.left = tempNode;
      }
    }

    private string GetRandomFloat()
    {
      return Random.Range(rangeMin, rangeMax).ToString().Substring(0, 5);
    }

    public static GSTree DeepClone<GSTree>(GSTree obj)
    {
      using (var ms = new MemoryStream())
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        ms.Position = 0;

        return (GSTree)formatter.Deserialize(ms);
      }
    }


  }

}