/*this script controls the level of the liquid in the glass, the values for the specific liquids in the glass, the wobble of liquid, 
and also contains coroutines that control changing the liquids colour and emptying the liquid in the glass when either the player
puts the glass on the matt or the liquid is all spilled out*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wobble : MonoBehaviour
{
    //create an empty reference to the renderer of the liquid, as well as floats that control the wobble
    Renderer rend;
    float wobbleX;
    float wobbleZ;
    float wobbleAddX;
    float wobbleAddZ;
    float sine;
    float time = 0f;
    public float MaxWobble = 0.07f;
    public float WobbleSpeed = 1f;
    public float Recovery = 1f;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;

    //floats for the fill amount and the change in fill. These values are used by the shader so it can display how full the glass is along
    //with other parts of the script
    public float fillAmount;
    public float fillChange;
    public float minFillAmount = .575f;
    public float totalValues = 0;

    private float currentValueSpirit = .1f;
    private float currentValueMixer = 1f;
    private float valueChangeSpirit = .000025f;
    private float valueChangeMixer = .00025f;
    
    //for the colour changing we need a base colour to go back to when the glass is emptied
    private Color baseColour = new Color(0, 0, 0, 0);

    //for spiling we need a list of empty game objects
    private List<GameObject> Emitters;

    //reference to the drink mat that ends a round and empties the glass
    public GameObject drinkMat;

    //current values in the glass, of type drinkinstructions
    public DrinkInstructions currentValues;

    // Use this for initialization
    void Start()
    {
        //initialise fillchange, reference to renderer and disable the renderer at start because no liquid in glass
        fillChange = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        /*get current fill amount, get reference to drinkmat and the emitters attatched to the spill script. need this for changing the spilled 
        liquid colour*/
        fillAmount = rend.material.GetFloat("fillAmount");
        drinkMat = GameObject.FindGameObjectWithTag("Finished");
        Emitters = GameObject.FindGameObjectWithTag("liquidInGlass").GetComponent<Spill>().Emitters;
    }
    private void Update()
    {
        //if drink has been placed on the mat or the fill amount has gone below the minimum
        if (drinkMat.GetComponent<DrinkFinished>().EnteredTrigger == true || fillAmount >= minFillAmount)
        {
            //hide liquid and start the empty drink coroutine
            rend.enabled = false;
            StartCoroutine("EmptyDrink");
        }

        //the following code is taken from https://www.patreon.com/posts/quick-game-art-18245226, credit going to Minions art for lines 75-98
        //math wobble of the liquid as the glass moves 
        time += Time.deltaTime;
        // decrease wobble 
        wobbleAddX = Mathf.Lerp(wobbleAddX, 0, Time.deltaTime * (Recovery));
        wobbleAddZ = Mathf.Lerp(wobbleAddZ, 0, Time.deltaTime * (Recovery));
        sine = 2 * Mathf.PI * WobbleSpeed;
        wobbleX = wobbleAddX * Mathf.Sin(sine * time);
        wobbleZ = wobbleAddZ * Mathf.Sin(sine * time);

        // send to shader
        rend.material.SetFloat("wobbleX", wobbleX);
        rend.material.SetFloat("wobbleZ", wobbleZ);

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;


        //wobble + velocity
        wobbleAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        // store last position
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;

        //set fill amount
        rend.material.SetFloat("fillAmount", fillAmount);
    }

    void OnTriggerEnter(Collider collision)
    {
        //if an object collides with the liquid object
        //if it is tagged with liquid
        if(collision.gameObject.tag == "Liquid")
        {
            //render the liquid in the glass
            rend.enabled = true;
            //get the colour of the liquid that hit it
            Color colour = collision.GetComponent<TrailRenderer>().material.color;
            //if the transparency of that colour is lower than what is currently in the glass
            if (colour.a > rend.material.GetColor("tint").a)
            {
                //change colour
                StartCoroutine(changeColour(colour));
            }
            
            /*I could not find a cleaner soloution than to check each name individually, unfortunately this leaves a lot of if statements one after the other
             this is something I should come back to*/

            //check if name of colliding object matches 
            if (collision.gameObject.name == "DropVodka(Clone)")
            {
                /*if it does, increase its value in the glass, decrease fill amount and increase fill change
                fill amount works in reverse, the smaller it is the more of the liquid will show
                repeat for all possible liquids*/
                currentValues.Vodka += currentValueSpirit;
                fillAmount = fillAmount - valueChangeSpirit;
                fillChange += valueChangeSpirit;
            }
            if (collision.gameObject.name == "DropRum(Clone)")
            {
                currentValues.Rum += currentValueSpirit;
                fillAmount = fillAmount - valueChangeSpirit;
                fillChange += valueChangeSpirit;
            }
            if (collision.gameObject.name == "DropTequila(Clone)")
            {
                currentValues.Tequila += currentValueSpirit;
                fillAmount = fillAmount - valueChangeSpirit;
                fillChange += valueChangeSpirit;
            }
            if (collision.gameObject.name == "DropGin(Clone)")
            {
                currentValues.Gin += currentValueSpirit;
                fillAmount = fillAmount - valueChangeSpirit;
                fillChange += valueChangeSpirit;
            }
            if (collision.gameObject.name == "DropWhiskey(Clone)")
            {
                currentValues.Whiskey += currentValueSpirit;
                fillAmount = fillAmount - valueChangeSpirit;
                fillChange += valueChangeSpirit;
            }
            if (collision.gameObject.name == "DropCoke(Clone)")
            {
                currentValues.Coke += currentValueMixer;
                fillAmount = fillAmount - valueChangeMixer;
                fillChange += valueChangeMixer;
            }
            if (collision.gameObject.name == "DropTonic(Clone)")
            {
                currentValues.Tonic += currentValueMixer;
                fillAmount = fillAmount - valueChangeMixer;
                fillChange += valueChangeMixer;
            }
            if (collision.gameObject.name == "DropSoda(Clone)")
            {
                currentValues.Soda += currentValueMixer;
                fillAmount = fillAmount - valueChangeMixer;
                fillChange += valueChangeMixer;
            }
            if (collision.gameObject.name == "DropGinger(Clone)")
            {
                currentValues.Ginger += currentValueMixer;
                fillAmount = fillAmount - valueChangeMixer;
                fillChange += valueChangeMixer;
            }
            if (collision.gameObject.name == "DropOrange(Clone)")
            {
                currentValues.Orange += currentValueMixer;
                fillAmount = fillAmount - valueChangeMixer;
                fillChange += valueChangeMixer;
            }
            if (collision.gameObject.name == "DropLime(Clone)")
            {
                currentValues.LimeJuice += currentValueMixer;
                fillAmount = fillAmount - valueChangeMixer;
                fillChange += valueChangeMixer;
            }
            if (collision.gameObject.name == "DropLemon(Clone)")
            {
                currentValues.LemonJuice += currentValueMixer;
                fillAmount = fillAmount - valueChangeMixer;
                fillChange += valueChangeMixer;
            }
            if (collision.gameObject.tag == "Liquid")
            {
                //for all drops that hit, set the fill amount and destroy the drop
                rend.material.SetFloat("fillAmount", fillAmount);
                Destroy(collision.gameObject);
            }
        }
    }
    //coroutine for changing the colour gradully
    IEnumerator changeColour(Color colour)
    {
        //floats for time duration and smoothness
        float t = 0;
        float duration = 1.5f;
        float smoothness = .002f;
        //get starting colour and an empty colour to hold the changing colour
        Color initialColour = rend.material.GetColor("tint");
        Color changingColour;
        //for 1 second
        while (t < 1)
        {
            //set changing colour to a lerp between initial colour and desired colour - increment time 
            changingColour = Color.Lerp(initialColour, colour, t);
            t += Time.deltaTime / duration;
            //set material to changing colour and set each particle system colour in Spill to the changing colour
            rend.material.SetColor("tint", changingColour);
            foreach(GameObject emitter in Emitters)
            {
                emitter.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color", changingColour);
            }
            
            yield return new WaitForSeconds(smoothness);
        }
       
    }
    //for emptying the drink
    IEnumerator EmptyDrink()
    {
        //I couldnt find a way to loop through an object so again it looks a bit clunky. Will work on solution in future
        //set all values in the glass to 0
        currentValues.Vodka = 0;
        currentValues.Rum = 0;
        currentValues.Tequila = 0;
        currentValues.Gin = 0;
        currentValues.Whiskey = 0;
        currentValues.Coke = 0;
        currentValues.Tonic = 0;
        currentValues.Soda = 0;
        currentValues.Ginger = 0;
        currentValues.Orange = 0;
        currentValues.LimeJuice = 0;
        currentValues.LemonJuice = 0;
        //set fillAmount to the minimum and reset fillchange
        fillAmount = minFillAmount;
        fillChange = 0;
        //set the tint back to a base colour with full transparency so colourchange will work after glass is emptied
        rend.material.SetColor("tint", baseColour);
        
        /*this is error checking to see if instructionloader is enabled or not. It is not enabled in between rounds so if a player wants to mess around and fill
        a drink, after it is emptied it wont fill up again until round start without this piece of code*/
        if (GameObject.FindGameObjectWithTag("Manager").GetComponent<InstructionLoader>().enabled == false)
        {
            drinkMat.GetComponent<DrinkFinished>().EnteredTrigger = false;
        }
        yield return null;
    }

}