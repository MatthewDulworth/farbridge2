using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Package", menuName = "ScriptableObjects/Package", order = 2)]
public class Package : ScriptableObject
{
   public bool legit;
   public string descsription;
   public GameObject gameObject;

   const int UP = 1;
   const int DOWN = -1;

   public bool isLegit()
   {
      return legit;
   }
}
