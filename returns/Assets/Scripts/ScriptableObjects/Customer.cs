using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/Customer", order = 1)]
public class Customer : ScriptableObject {
   public GameObject gameObject;
   public Return returnedGoods;
   public AudioClip theme;

   public string introDialouge;
   public string acceptDialouge;
   public string denyDialouge;
}
