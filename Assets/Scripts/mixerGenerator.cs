using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixerGenerator : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject drinkToSpawn;
    public GameObject door;
    public float smooth;


    public Vector3 initialPos;
    public Vector3 openPos;
    public float timePassed;
    float opener;

    void Start()
    {
        smooth = .001f;
        opener = 0;
        initialPos = door.transform.position;
        openPos = door.transform.position + new Vector3(0, 1, 0);
        timePassed = 3;
    }
    private void FixedUpdate()
    {
        timePassed = timePassed + (Time.deltaTime);
    }
    public void OnPress()
    {
        Debug.Log("Time passed = " + timePassed);
        if(timePassed > 3)
        {
            StartCoroutine("Door");
            Vector3 spawnPosition = spawnPoint.transform.position;
            Debug.Log("SteamVR Button pressed!");
            Instantiate(drinkToSpawn, spawnPosition, Quaternion.identity);
            timePassed = 0;
        }
    }

    
}
