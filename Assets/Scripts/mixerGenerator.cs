using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixerGenerator : MonoBehaviour
{
    public GameObject spawnPoint;
    public List<GameObject> mixers;
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
    public void OnPress(GameObject sender)
    {
        Debug.Log("Time passed = " + timePassed);
        if(timePassed > 3)
        {
            //StartCoroutine("Door");
            Vector3 spawnPosition = spawnPoint.transform.position;
            if (sender.name == "ColaButton")
            {
                Instantiate(mixers[0], spawnPosition, Quaternion.identity);
                timePassed = 0;
            }
            if (sender.name == "TonicButton")
            {
                Instantiate(mixers[1], spawnPosition, Quaternion.identity);
                timePassed = 0;
            }
            if (sender.name == "SodaButton")
            {
                Instantiate(mixers[2], spawnPosition, Quaternion.identity);
                timePassed = 0;
            }
            if (sender.name == "GingerButton")
            {
                Instantiate(mixers[3], spawnPosition, Quaternion.identity);
                timePassed = 0;
            }
            if (sender.name == "OrangeButton")
            {
                Instantiate(mixers[4], spawnPosition, Quaternion.identity);
                timePassed = 0;
            }


        }
    }

    
}
