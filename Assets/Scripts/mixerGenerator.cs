/*Simple script attatched to the mixer generator that spawns in mixers*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixerGenerator : MonoBehaviour
{
    /*public gameobjects for an empty gameobject that defines the spawn point for the mixers and a list of the mixers that can
    be spawned. Alsoa float for time passed so that pressing a button once wont spawn in multiple bottles*/
    public GameObject spawnPoint;
    public List<GameObject> mixers;
    public float timePassed;
    Vector3 spawnPosition;
    void Start()
    {
        //set timepassed = 3 so player can spawn a drink at once if wanted. also get position of spawnpoint and keep locally
        timePassed = 3;
        spawnPosition = spawnPoint.transform.position;
    }
    private void FixedUpdate()
    {
        //keep track of time
        timePassed = timePassed + (Time.deltaTime);
    }
    public void OnPress(GameObject sender)
    {
        //if enough time has passed
        if(timePassed > 3)
        {
            //switch to see the name of the sender. Each button has a specific name based on what it should spawn.
            switch (sender.name)
            {
                case "ColaButton":
                    Instantiate(mixers[0], spawnPosition, Quaternion.identity);
                    timePassed = 0;
                    break;
                case "TonicButton":
                    Instantiate(mixers[1], spawnPosition, Quaternion.identity);
                    timePassed = 0;
                    break;
                case "SodaButton":
                    Instantiate(mixers[2], spawnPosition, Quaternion.identity);
                    timePassed = 0;
                    break;
                case "GingerButton":
                    Instantiate(mixers[3], spawnPosition, Quaternion.identity);
                    timePassed = 0;
                    break;
                case "OrangeButton":
                    Instantiate(mixers[4], spawnPosition, Quaternion.identity);
                    timePassed = 0;
                    break;
                
            }
        }
    }
}
