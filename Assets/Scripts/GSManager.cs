using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using GeneticShader;
using System.Runtime.Serialization.Formatters.Binary;

public class GSManager : MonoBehaviour
{

  [System.Serializable]
  public class Individual
  {
    public GSIndividual GSInd;

    public Individual(string timeStamp, int generation, int indNum)
    {
      UpdateGS(new GSIndividual(timeStamp, generation, indNum), true);
    }

    public void UpdateGS(GSIndividual newInd, bool writeShader)
    {
      GSInd = newInd;
      if (writeShader) { GSWriteShader.WriteShader(GSInd); }
    }

    public void UpdateGeneration(int generation)
    {
      GSInd.UpdateGeneration(generation);
    }
  }

  private int generation = 0;
  public Text generationText;
  private string time;
  public Text populationName;

  private Individual[] individualList;
  public GameObject[] individualGameObjects;
  public Button[] indButtons;
  public Individual[] selectedInds = new Individual[3];
  private int currSelInd;

  public Button rewind;
  public Button forward;
  public Button setGeneration;
  public Button evolveBtn;

  public Text selGenerationText;
  private int selGeneration;

  private List<Individual[]> generationList = new List<Individual[]>();

  private int meshSelection = 0;

  public GameObject cameras;

  // Use this for initialization
  void Start()
  {
    time = ConvertToUnixTimestamp(DateTime.UtcNow).ToString();
    populationName.text = "Population Name: " + time;
    generationText.text = "Generation: " + generation;
    selGeneration = generation;
    selGenerationText.text = selGeneration.ToString();

    individualList = new Individual[9];
    for (int i = 0; i < individualList.Length; i++)
    {
      individualList[i] = new Individual(time, generation, i);
    }
    CopyGeneration();
    UpdateGameObjects();
    for (int i = 0; i < selectedInds.Length; i++)
    {
      selectedInds[i] = individualList[i];
      indButtons[i].image.color = Color.green;
    }

  }

  void Update()
  {
    if (selGeneration == generation) { forward.interactable = false; }
    else { forward.interactable = true; }

    if (Input.GetMouseButton(0))
    {
      float rotX = Input.GetAxis("Mouse X") * 10 * Mathf.Deg2Rad;
      float rotY = Input.GetAxis("Mouse Y") * 10 * Mathf.Deg2Rad;
      for (int i = 0; i < individualGameObjects.Length; i++)
      {
        individualGameObjects[i].transform.RotateAround(Vector3.up, -rotX);
        individualGameObjects[i].transform.RotateAround(Vector3.right, rotY);
      }

    }

    float d = Input.GetAxis("Mouse ScrollWheel");
    if (d > 0f)
    {
      cameras.transform.position = new Vector3(cameras.transform.position.x, cameras.transform.position.y, cameras.transform.position.z + 0.1f);
    }
    else if (d < 0f)
    {
      cameras.transform.position = new Vector3(cameras.transform.position.x, cameras.transform.position.y, cameras.transform.position.z - 0.1f);
    }


  }

  public void Evolve()
  {

    IncreaseGeneration();
    List<Individual> nonSelInds = new List<Individual>();
    for (int i = 0; i < individualList.Length; i++)
    {
      nonSelInds.Add(individualList[i]);
    }
    for (int i = 0; i < selectedInds.Length; i++)
    {
      nonSelInds.Remove(selectedInds[i]);
      selectedInds[i].UpdateGS(selectedInds[i].GSInd, true);
    }
    for (int i = 0; i < nonSelInds.Count; i++)
    {
      int selOption = (int)UnityEngine.Random.Range(0, 4);
      int selParent1 = (int)UnityEngine.Random.Range(0, selectedInds.Length);
      int selParent2 = (int)UnityEngine.Random.Range(0, selectedInds.Length);
      switch (selOption)
      {
        case 0:
          nonSelInds[i].UpdateGS(selectedInds[selParent1].GSInd.InsertRandomNode(nonSelInds[i].GSInd.indNum), true);
          break;
        case 1:
          nonSelInds[i].UpdateGS(selectedInds[selParent1].GSInd.Permutate(nonSelInds[i].GSInd.indNum), true);
          break;
        case 2:
          nonSelInds[i].UpdateGS(selectedInds[selParent1].GSInd.Mutate(nonSelInds[i].GSInd.indNum), true);
          break;
        case 3:
          nonSelInds[i].UpdateGS(selectedInds[selParent1].GSInd.Crossover(selectedInds[selParent2].GSInd, nonSelInds[i].GSInd.indNum), true);
          break;
      }
    }
    CopyGeneration();
    UpdateGameObjects();
  }

  public void UpdateSelection(int number)
  {
    selectedInds[currSelInd] = individualList[number];
    currSelInd++;
    if (currSelInd > 2) currSelInd = 0;

    for (int i = 0; i < indButtons.Length; i++)
    {
      indButtons[i].image.color = Color.red;
    }
    for (int i = 0; i < selectedInds.Length; i++)
    {
      int buttonNum = selectedInds[i].GSInd.indNum;
      indButtons[buttonNum].image.color = Color.green;
    }
  }


  private void IncreaseGeneration()
  {
    generationText.text = "Generation: " + ++generation;
    selGeneration = generation;
    selGenerationText.text = selGeneration.ToString();
    for (int i = 0; i < individualList.Length; i++)
    {
      individualList[i].GSInd.UpdateGeneration(generation);
    }
  }

  public void ForwardGeneration()
  {
    selGeneration = Math.Min(++selGeneration, generation);
    selGenerationText.text = selGeneration.ToString();
    for (int i = 0; i < individualList.Length; i++)
    {
      individualList[i].UpdateGeneration(selGeneration);
    }
    UpdateGameObjects();
  }

  public void RewindGeneration()
  {
    if (evolveBtn.interactable) { evolveBtn.interactable = false; }
    selGeneration = Math.Max(--selGeneration, 0);
    selGenerationText.text = selGeneration.ToString();
    for (int i = 0; i < individualList.Length; i++)
    {
      individualList[i].UpdateGeneration(selGeneration);
    }
    UpdateGameObjects();
  }

  public void SetGeneration()
  {
    for (int i = selGeneration + 1; i <= generation; i++)
    {
      Directory.Delete("Assets/Resources/GeneratedShaders/" + time + "/" + i, true);
    }
    AssetDatabase.Refresh();
    generationList.RemoveRange(selGeneration + 1, generation - selGeneration);

    generation = selGeneration;
    generationText.text = "Generation: " + generation;
    selGenerationText.text = selGeneration.ToString();
    for (int i = 0; i < individualList.Length; i++)
    {
      individualList[i].UpdateGeneration(generation);
      individualList[i].UpdateGS(generationList[generation][i].GSInd, true);
    }
    UpdateGameObjects();
    evolveBtn.interactable = true;
  }

  public static double ConvertToUnixTimestamp(DateTime date)
  {
    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    TimeSpan diff = date.ToUniversalTime() - origin;
    return Math.Floor(diff.TotalSeconds);
  }

  private void CopyGeneration()
  {
    Individual[] generationCopy = new Individual[9];
    for (int i = 0; i < individualList.Length; i++)
    {
      generationCopy[i] = DeepClone(individualList[i]);
    }
    generationList.Add(generationCopy);
  }

  public static Individual DeepClone<Individual>(Individual obj)
  {
    using (var ms = new MemoryStream())
    {
      var formatter = new BinaryFormatter();
      formatter.Serialize(ms, obj);
      ms.Position = 0;

      return (Individual)formatter.Deserialize(ms);
    }
  }

  private void UpdateGameObjects()
  {
    for (int i = 0; i < individualGameObjects.Length; i++)
    {
      individualGameObjects[i].GetComponent<Renderer>().material.shader = Shader.Find("GeneticShader/" + individualList[i].GSInd.shaderName);
    }
  }

  public void ChangeMesh(int sel)
  {
    meshSelection = sel + meshSelection;
    meshSelection = meshSelection % 4;
    GameObject gameobj;
    float scale = 1.0f;

    if (meshSelection == 0)
    {
      gameobj = GameObject.CreatePrimitive(PrimitiveType.Plane);
      scale = 0.1f;
    }
    else if (meshSelection == 1)
    {
      gameobj = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
    else if (meshSelection == 2)
    {
      gameobj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    }
    else
    {
      gameobj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

    for (int i = 0; i < individualGameObjects.Length; i++)
    {
      individualGameObjects[i].GetComponent<MeshFilter>().mesh = gameobj.GetComponent<MeshFilter>().mesh;
      individualGameObjects[i].transform.localScale = new Vector3(scale, scale, scale);
    }
    Destroy(gameobj);
  }

}


