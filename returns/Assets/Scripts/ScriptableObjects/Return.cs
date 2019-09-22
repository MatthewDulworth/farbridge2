using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Return", menuName = "ScriptableObjects/Return", order = 1)]
public class Return : ScriptableObject {
    public Package package;
    public GameObject receipt;
    public bool legit;
}
