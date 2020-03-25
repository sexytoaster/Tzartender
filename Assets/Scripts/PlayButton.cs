using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private bool gameStart = false;
    private float startTime;
    private float timer;
    public Text Timer;
    public float timerLength = 180;
    public float countdown;
    private GameObject playButton;
    private GameObject[] Instructions;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        countdown = timerLength;
        playButton = GameObject.FindGameObjectWithTag("PlayButton");
        Instructions = GameObject.FindGameObjectsWithTag("Instructions");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart == true)
        {
            timer = Time.time - startTime;
            countdown = timerLength - timer;
            int minutes = Mathf.FloorToInt(countdown / 60);
            int seconds = (int)countdown % 60;

            string text = minutes + ":" + seconds;
            Timer.text = text;
            if (timer >= timerLength)
            {
                playButton.GetComponent<VRButton>().enabled = true;
                foreach (GameObject instruction in Instructions)
                {
                    instruction.GetComponent<Text>().enabled = false;
                }
                this.gameObject.GetComponent<InstructionLoader>().enabled = false;
                countdown = timerLength;
                gameStart = false;
            }
        }
    }
    public void OnPress()
    {
        gameStart = true;
        startTime = Time.time;
        playButton.GetComponent<VRButton>().enabled = false;
        foreach(GameObject instruction in Instructions)
        {
            instruction.GetComponent<Text>().enabled = true;
        }
        this.gameObject.GetComponent<InstructionLoader>().enabled = true;
    }
}
