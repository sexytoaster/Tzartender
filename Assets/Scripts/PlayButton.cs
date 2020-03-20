using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private float startTime;
    private float timer;
    private GameObject playButton;
    private GameObject[] Instructions;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        playButton = GameObject.FindGameObjectWithTag("PlayButton");
        Instructions = GameObject.FindGameObjectsWithTag("Instructions");
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time - startTime;
        if(timer >= 180)
        {
            playButton.GetComponent<VRButton>().enabled = true;
            foreach (GameObject instruction in Instructions)
            {
                instruction.GetComponent<Text>().enabled = false;
            }
            this.gameObject.GetComponent<InstructionLoader>().enabled = false;
        }
    }
    public void OnPress()
    {
        startTime = Time.time;
        playButton.GetComponent<VRButton>().enabled = false;
        foreach(GameObject instruction in Instructions)
        {
            instruction.GetComponent<Text>().enabled = true;
        }
        this.gameObject.GetComponent<InstructionLoader>().enabled = true;
    }
}
