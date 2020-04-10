/*This script controls things like the timer for the game, the gameplay loop itself and the enabling and disabling of artifacts on
the in game screen that instruct the player on what to do to play the game and show their score and their time remaining*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    //here we create empty references for variables used in the script and Text objects that this script manipulates
    private bool gameStart = false;
    private float startTime;
    private float timer;
    private float timerLength = 120;
    private float countdown;
    public Text Timer;
    public Text Score;
    public Text FinalScore;
    public Text startText;

    //here are empty references for the gameobjects that will be referenced and manipulated
    private GameObject playButton;
    private GameObject[] Instructions;
    
    // Start is called before the first frame update
    void Start()
    {
        //initialize timer and countdown and get references to objects needed
        timer = 0;
        countdown = timerLength;
        playButton = GameObject.FindGameObjectWithTag("PlayButton");
        Instructions = GameObject.FindGameObjectsWithTag("Instructions");
    }

    // Update is called once per frame
    void Update()
    {
        //while the flag is true
        if (gameStart == true)
        {
            //update our timer using simple equation, convert our timer into a countdown
            timer = Time.time - startTime;
            countdown = timerLength - timer;

            //get the minutes and seconds for the timer that will be displayed to the player, and display it (if statement so if <10 seconds left in a minute it will display
            //2:07 instead of 2:7
            int minutes = Mathf.FloorToInt(countdown / 60);
            int seconds = (int)countdown % 60;
            string text;
            if (seconds <=9 )
            {
                text = minutes + ":0" + seconds;
            }
            else
            {
                text = minutes + ":" + seconds;
            }
            Timer.text = "Time: " + text;
            //if timer for round has completed
            if (timer >= timerLength)
            {
                //get score at the end of the round
                string score = Score.text;
                //enable the button again and disable all of the text for the round
                playButton.GetComponent<VRButton>().enabled = true;
                foreach (GameObject instruction in Instructions)
                {
                    instruction.GetComponent<Text>().enabled = false;
                }
                //enable the text that displayed the score and prompts the user to play again, and disable the instructionloader script. 
                FinalScore.enabled = true;
                FinalScore.text = "Final " + score + "\n Press the button to PLAY again";
                this.gameObject.GetComponent<InstructionLoader>().enabled = false;
                //reset countdown and flag
                countdown = timerLength;
                gameStart = false;
            }
        }
    }
    public void OnPress()
    {
        //if button is pressed

        //set flag = true, get our start time, disable the playbutton, disable old test on screen and enable the text we want to see during the gameplay loop
        gameStart = true;
        startTime = Time.time;
        playButton.GetComponent<VRButton>().enabled = false;
        startText.enabled = false;
        FinalScore.enabled = false;
        foreach(GameObject instruction in Instructions)
        {
            instruction.GetComponent<Text>().enabled = true;
        }
        this.gameObject.GetComponent<InstructionLoader>().enabled = true;
    }
}
