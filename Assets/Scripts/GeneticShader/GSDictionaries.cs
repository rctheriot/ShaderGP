using System.Collections.Generic;
using System.Linq;

namespace GeneticShader
{

  public static class GSDictionaries
  {
    public static Dictionary<string, string> BasicFunctions = new Dictionary<string, string>()
    {
      {"mul", "*"},
      {"sub", "-"},
      {"div", "/"},
      {"add", "+"}
    };

    public static Dictionary<string, string> DoubleArgFunctions = new Dictionary<string, string>()
    {
      {"min", "min"},
      {"max", "max"},
      {"dot", "dot"},
      {"distance", "distance"},
      {"fmod", "fmod"},
      {"ldexp", "ldexp"},
      {"pow", "pow"},
    };

    public static Dictionary<string, string> SingleArgFunctions = new Dictionary<string, string>()
    {
      {"sin", "sin"},
      {"cos", "cos"},
      {"tan", "tan"},
      {"atan", "atan"},
      {"asin", "asin"},
      {"acos", "acos"},
      {"ceil", "ceil"},
      {"floor", "floor"},
      {"exp", "exp"},
      {"exp2", "exp2"},
      {"frac", "frac"},
      {"abs", "abs"},
      {"degrees", "degrees"},
      {"log", "log"},
      {"log10", "log10"},
      {"log2", "log2"},
      {"length", "length"},
      {"rsqrt", "rsqrt"},
    };

    public static Dictionary<string, string> ColorVariableDictionary = new Dictionary<string, string>()
    {
      // {"Time", "_Time.x"},
      // {"SinTime", "_SinTime.x"},
      // {"CosTime", "_CosTime.x"},
      {"Time2", "_Time.y"},
      {"SinTime2", "_SinTime.y"},
      {"CosTime2", "_CosTime.y"},
      {"Time3", "_Time.z"},
      {"SinTime3", "_SinTime.z"},
      {"CosTime3", "_CosTime.z"},
      {"UVX", "i.uv.x"},
      {"UVY", "i.uv.y"}, 
      {"UVXX", "i.uv.x * i.uv.x"},
      {"UVYY", "i.uv.y * i.uv.y"},
      {"UVXY", "i.uv.x * i.uv.y"},
    };

    public static Dictionary<string, string> PositionVariableDictionary = new Dictionary<string, string>()
    {
      {"Time", "_Time.x"},
      {"SinTime", "_SinTime.x"},
      {"CosTime", "_CosTime.x"},
      {"Time2", "_Time.y"},
      {"SinTime2", "_SinTime.y"},
      {"CosTime2", "_CosTime.y"},
      {"Time3", "_Time.z"},
      {"SinTime3", "_SinTime.z"},
      {"CosTime3", "_CosTime.z"},
      {"VX", "v.vertex.x"},
      {"VY", "v.vertex.y"},
      {"VZ", "v.vertex.z"},
      {"VXVX", "v.vertex.x * v.vertex.x"},
      {"VXVY", "v.vertex.x * v.vertex.y"},
      {"VXVZ", "v.vertex.x * v.vertex.z"},
      {"VYVY", "v.vertex.y * v.vertex.y"},
      {"VYVZ", "v.vertex.y * v.vertex.z"},
      {"VZVZ", "v.vertex.z * v.vertex.z"},
      {"VXVYVZ", "v.vertex.x * v.vertex.y * v.vertex.z"},
    };


    public static string GetRandomSingleArgFunction()
    {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(SingleArgFunctions.Keys);
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetDifferentSingleArgFunction(string current)
    {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(SingleArgFunctions.Keys);
      keyList.Remove(current);
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetRandomDoubleArgFunction()
    {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(BasicFunctions.Keys);
      keyList.AddRange(new List<string>(DoubleArgFunctions.Keys));
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetDifferentDoubleArgFunction(string current)
    {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(BasicFunctions.Keys);
      keyList.AddRange(new List<string>(DoubleArgFunctions.Keys));
      keyList.Remove(current);
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetRandomFunction()
    {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(BasicFunctions.Keys);
      keyList.AddRange(new List<string>(DoubleArgFunctions.Keys));
      keyList.AddRange(new List<string>(SingleArgFunctions.Keys));
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetDifferentRandomFunction(string current)
    {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(BasicFunctions.Keys);
      keyList.AddRange(new List<string>(DoubleArgFunctions.Keys));
      keyList.AddRange(new List<string>(SingleArgFunctions.Keys));
      keyList.Remove(current);
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetRandomColorVariable() {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(ColorVariableDictionary.Values);
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetDifferentRandomColorVariable(string current) {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(ColorVariableDictionary.Values);
      keyList.Remove(current);
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetRandomPositionVariable() {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(PositionVariableDictionary.Values);
      return keyList[rand.Next(keyList.Count)];
    }

    public static string GetDifferentRandomPositionVariable(string current) {
      System.Random rand = new System.Random();
      List<string> keyList = new List<string>(PositionVariableDictionary.Values);
      keyList.Remove(current);
      return keyList[rand.Next(keyList.Count)];
    }

  }

}