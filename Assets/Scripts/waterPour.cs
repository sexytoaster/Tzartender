/*This script is to create the particle system that drives the pouring behaviour. Using a normal particle system
 could possibly have worked but creating my own meant i could directly monitor the number of drops being instantiated and 
 The names of the drops, which is used for identifying the liquid being poured.*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPour : MonoBehaviour
{
    /*create empty gameobjects for the drops being instantiated and the bottle the script is attatched to. Also a variable for the 
    speed of the drops and a vector3 that will referene the velocity component of the drops*/
    public GameObject drop;
    public GameObject bottle;
    public float speed = 1000f;
    Vector3 v3Velocity;
    bool mixerBottle = false;
    void Start()
    {
        /*get a reference to the velocity component of the bottle, so this can be added to the drops being created 
        so the liquid flows more naturally.*/
        Rigidbody rb = GetComponent<Rigidbody>();
        v3Velocity = rb.velocity;
        /*check if it's attatched to a mixer or not. This only needs to be done once and will be faster 
        to check bool than compare names everytime in update*/
        if (transform.parent.name == "MixerBottle" || transform.parent.name == "MixerBottle(Clone)")
        {
            mixerBottle = true;
        }
    }
    /* using a fixed update as that is called 60 times a second, meaning we have a fixed flow rate regardless of games performance ie not faster or slower. 
    This will make pouring consistent across different setups*/
    void FixedUpdate()
    {
        //check if its a mixerbottle or a spirit bottle
        if (mixerBottle == true)
        {
            //check if cap is kinematic. If it is not it means the cap has been removed using the opener
            bool cap = transform.parent.gameObject.GetComponentInChildren<BottleCap>().capRB.isKinematic;
            if (cap == false)
            {
                //checks if the bottle is tilted on its side 
                if (Vector3.Dot(bottle.transform.up, Vector3.down) > 0)
                {
                    //create new drop and add forces to it
                    GameObject instDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
                    Rigidbody instDropRigidbody = instDrop.GetComponent<Rigidbody>();
                    instDropRigidbody.AddForce((bottle.transform.up * speed) + v3Velocity);
                }
            }
        }
        else
        {
            //checks if bottle is tilted on its side
            if (Vector3.Dot(bottle.transform.up, Vector3.down) > 0)
            {
                //create new drop and add forces to it
                GameObject instDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
                Rigidbody instDropRigidbody = instDrop.GetComponent<Rigidbody>();
                instDropRigidbody.AddForce((bottle.transform.up * speed) + v3Velocity);
            }
        }

    }
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Glass")
            Destroy(gameObject);
    }*/
}

