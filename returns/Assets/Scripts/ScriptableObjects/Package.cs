using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Package", menuName = "ScriptableObjects/Package", order = 2)]
public class Package : ScriptableObject
{
   public GameObject gameObject;

   [SerializeField]
   private string[] descriptions;

   const int UP = 1;
   const int DOWN = -1;

    public string getDescription()
    {
        if (descriptions.Length == 0)
        {
            return "";
        }
        else
        {
            return descriptions[Random.Range(0, descriptions.Length)];
        }
    }
}
