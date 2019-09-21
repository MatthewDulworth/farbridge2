using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "ScriptableObjects/Day", order = 4)]
public class Day : ScriptableObject {
   public int index;
   public List<Customer> customers;
}
