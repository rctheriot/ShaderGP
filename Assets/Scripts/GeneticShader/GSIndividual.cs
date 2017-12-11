using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GeneticShader
{

  [System.Serializable]
  public class GSIndividual
  {

    public string timeStamp;
    public int generation;
    public int indNum;

    public string shaderName;

    public GSTree red;
    public GSTree green;
    public GSTree blue;
    public GSTree alpha;

    public GSTree xpos;
    public GSTree ypos;
    public GSTree zpos;


    public GSIndividual(string timeStamp, int generation, int indNum)
    {
      this.timeStamp = timeStamp;
      this.generation = generation;
      this.indNum = indNum;
      shaderName = timeStamp + "_" + generation + "_" + indNum;

      red = new GSTree(false);
      green = new GSTree(false);
      blue = new GSTree(false);
      alpha = new GSTree(false);

      xpos = new GSTree(true);
      ypos = new GSTree(true);
      zpos = new GSTree(true);

    }

    public GSIndividual InsertRandomNode(int iNum)
    {
      GSIndividual newInd = new GSIndividual(timeStamp, generation, iNum);
      newInd.red = GSTree.DeepClone(red);
      newInd.red.InsertRandomNode();
      newInd.green = GSTree.DeepClone(green);
      newInd.green.InsertRandomNode();
      newInd.blue = GSTree.DeepClone(blue);
      newInd.blue.InsertRandomNode();
      newInd.alpha = GSTree.DeepClone(alpha);
      newInd.alpha.InsertRandomNode();

      newInd.xpos = GSTree.DeepClone(xpos);
      newInd.xpos.InsertRandomNode();
      newInd.ypos = GSTree.DeepClone(ypos);
      newInd.ypos.InsertRandomNode();
      newInd.zpos = GSTree.DeepClone(zpos);
      newInd.zpos.InsertRandomNode();

      return newInd;
    }

    public GSIndividual Mutate(int iNum)
    {
      GSIndividual newInd = new GSIndividual(timeStamp, generation, iNum);
      newInd.red = GSTree.DeepClone(red);
      newInd.red.MutateRandomNode();
      newInd.green = GSTree.DeepClone(green);
      newInd.green.MutateRandomNode();
      newInd.blue = GSTree.DeepClone(blue);
      newInd.blue.MutateRandomNode();
      newInd.alpha = GSTree.DeepClone(alpha);
      newInd.alpha.MutateRandomNode();

      newInd.xpos = GSTree.DeepClone(xpos);
      newInd.xpos.MutateRandomNode();
      newInd.ypos = GSTree.DeepClone(ypos);
      newInd.ypos.MutateRandomNode();
      newInd.zpos = GSTree.DeepClone(zpos);
      newInd.zpos.MutateRandomNode();

      return newInd;
    }

    public GSIndividual Permutate(int iNum)
    {
      GSIndividual newInd = new GSIndividual(timeStamp, generation, iNum);
      newInd.red = GSTree.DeepClone(red);
      newInd.red.PermutateRandomNode();
      newInd.green = GSTree.DeepClone(green);
      newInd.green.PermutateRandomNode();
      newInd.blue = GSTree.DeepClone(blue);
      newInd.blue.PermutateRandomNode();
      newInd.alpha = GSTree.DeepClone(alpha);
      newInd.alpha.PermutateRandomNode();

      newInd.xpos = GSTree.DeepClone(xpos);
      newInd.xpos.PermutateRandomNode();
      newInd.ypos = GSTree.DeepClone(ypos);
      newInd.ypos.PermutateRandomNode();
      newInd.zpos = GSTree.DeepClone(zpos);
      newInd.zpos.PermutateRandomNode();

      return newInd;
    }

    public GSIndividual Crossover(GSIndividual ind, int iNum)
    {
      GSIndividual newInd = new GSIndividual(timeStamp, generation, iNum);
      newInd.red = GSTree.DeepClone(red);
      newInd.red.CrossoverRandom(ind.red);
      newInd.green = GSTree.DeepClone(green);
      newInd.green.CrossoverRandom(ind.green);
      newInd.blue = GSTree.DeepClone(blue);
      newInd.blue.CrossoverRandom(ind.blue);
      newInd.alpha = GSTree.DeepClone(alpha);
      newInd.alpha.CrossoverRandom(ind.alpha);

      newInd.xpos = GSTree.DeepClone(xpos);
      newInd.xpos.CrossoverRandom(ind.xpos);
      newInd.ypos = GSTree.DeepClone(ypos);
      newInd.ypos.CrossoverRandom(ind.ypos);
      newInd.zpos = GSTree.DeepClone(zpos);
      newInd.zpos.CrossoverRandom(ind.zpos);

      return newInd;
    }

    public void UpdateGeneration(int generation)
    {
      this.generation = generation;
      shaderName = timeStamp + "_" + generation + "_" + indNum;
    }

  }



}