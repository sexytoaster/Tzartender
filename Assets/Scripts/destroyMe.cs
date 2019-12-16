using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour {

    public float lifetime = 5.0f;
    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}