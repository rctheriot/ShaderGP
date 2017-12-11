using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace GeneticShader
{

  [System.Serializable]
  public class GSTreeNode : ICloneable
  {

    public string value;
    public GSTreeNode left;
    public GSTreeNode right;

    public object Clone()
    {
        return this.MemberwiseClone();
    }

  }

}