using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkFinished : MonoBehaviour
{
    public bool EnteredTrigger = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "PintGlass")
        {
            EnteredTrigger = true;

        }
    }
}
