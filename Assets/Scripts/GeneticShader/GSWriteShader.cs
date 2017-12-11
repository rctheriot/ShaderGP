using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GeneticShader
{

  public static class GSWriteShader
  {
    public static void WriteToLine(string newText, string fileName, int line_to_edit)
    {
      string[] arrLine = File.ReadAllLines(fileName);
      arrLine[line_to_edit - 1] = newText;
      File.WriteAllLines(fileName, arrLine);
    }

    public static string WriteShader(GSIndividual ind)
    {
      string templatePath = "Assets/Resources/GSTemplate.shader";
      string copyPath = "Assets/Resources/GeneratedShaders/" + ind.timeStamp + "/" + ind.generation + "/" + ind.shaderName + ".shader";
      Directory.CreateDirectory("Assets/Resources/GeneratedShaders/" + ind.timeStamp + "/" + ind.generation + "/");
      File.Copy(templatePath, copyPath, true);

      WriteToLine("Shader \"GeneticShader/" + ind.shaderName + "\"", copyPath, 1);

      WriteToLine("        v.vertex.x += " + ind.xpos.WriteTree() + ";", copyPath, 37);
      WriteToLine("        v.vertex.y += " + ind.ypos.WriteTree() + ";", copyPath, 38);
      WriteToLine("        v.vertex.z += " + ind.zpos.WriteTree() + ";", copyPath, 39);

      WriteToLine("        col.r = " + ind.red.WriteTree() + ";", copyPath, 49);
      WriteToLine("        col.g = " + ind.green.WriteTree() + ";", copyPath, 50);
      WriteToLine("        col.b = " + ind.blue.WriteTree() + ";", copyPath, 51);

      GSWriteShader.WriteToLine("        col.a = " + ind.alpha.WriteTree() + ";", copyPath, 52);
      AssetDatabase.Refresh();
      return "GeneticShader/" + ind.shaderName;
    }
  }

}
