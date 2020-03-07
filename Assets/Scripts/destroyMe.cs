using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour {
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag != "Liquid")
        {
            Destroy(gameObject);
        }
    }
}