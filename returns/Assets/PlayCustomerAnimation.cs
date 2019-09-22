using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCustomerAnimation : MonoBehaviour
{
    public void PlayWalkIn()
    {
        GetComponent<Animation>()?.Play("WalkIn");
    }

    public void PlayWalkOut()
    {
        GetComponent<Animation>()?.Play("Leave");
    }
}
