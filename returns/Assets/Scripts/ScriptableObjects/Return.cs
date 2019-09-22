using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Return", menuName = "ScriptableObjects/Return", order = 1)]
public class Return : ScriptableObject {
    public bool legit;
    public Sprite receipt;
    public Sprite package;
    public AudioClip soundEffect;

    [SerializeField]
    private string[] descriptions;

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
