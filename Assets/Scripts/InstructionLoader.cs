using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.Newtonsoft.Json;

public class InstructionLoader : MonoBehaviour
{
    //storing drink contents in a dictionary - faster to load from than list
    public Dictionary<int, DrinkInstructions> drinks;
    //temporary for seeing contents of dict
    public List<DrinkInstructions> drinksList;
    //two text boxes for displaying the values
    public Text instructions;
    public Text currentValues;
    public Text drinkName;
    public Text currentScore;
    //reference to the glass
    public GameObject glass;
    //reference to the mat
    public GameObject drinkMat;
    //values inside glass
    public DrinkInstructions currentGlassValues;
    //lists for values
    public List<string> individualDrink;
    public List<float> individualDrinkValues;
    public List<float> displayedIndividualDrinkValues;

    //a max score that will be subtracted from for time and also accuracy
    public int baseScore;
    //min score is the minimum score a player can get
    public int minScore;
    //total score of the player
    public int totalScore;
    public DrinkInstructions temp;
    //indexes for the drink
    public int rumIndex;
    public int vodkaIndex;
    public int cokeIndex;

    public int testIndex;

    private void Awake()
    {
        currentScore.text = "Score: " + totalScore.ToString();
        baseScore = 100;
        minScore = 20;
        totalScore = 0;
        //create a new dictionary containing our drinks
        drinks = new Dictionary<int, DrinkInstructions>();
        //parse json into dict
        DrinkDictionary dictionary = JsonUtility.FromJson<DrinkDictionary>(JsonFileReader.LoadJsonAsResource("Drinks/drinkDictionary.json"));
        foreach (string dictionaryDrink in dictionary.Drinks)
        {
            //add drinks from dictionary into drinks array
            LoadDrink(dictionaryDrink);
        }
        //for testing to see dict entries
        foreach (KeyValuePair<int, DrinkInstructions> entry in drinks)
        {
            DrinkInstructions temp = entry.Value;
            drinksList.Add(temp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        drinkMat = GameObject.FindGameObjectWithTag("Finished");
        //index for keeping track of generated drinks, should change later nb****
        //var index = 0;
        //get reference to glass values
        currentGlassValues = glass.GetComponentInChildren<wobble>().currentValues;
        StartCoroutine("PickDrink");
        
    }

    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine("UpdateValues");
        if (drinkMat.GetComponent<DrinkFinished>().EnteredTrigger == true)
        {
            StartCoroutine("Score");
            StartCoroutine("PickDrink");
            drinkMat.GetComponent<DrinkFinished>().EnteredTrigger = false;
        }
        string combindedString = string.Join("\n", individualDrinkValues.ToArray());
        combindedString.Trim('"');
        currentValues.text = combindedString;
        Debug.Log(currentGlassValues.Vodka.ToString());
    }

    public void LoadDrink(string path)
    {
        //loads json data into string
        string loadedDrink = JsonFileReader.LoadJsonAsResource(path);
        //loads string data into drinkInstructions object
        DrinkInstructions drink = JsonUtility.FromJson<DrinkInstructions>(loadedDrink);

        //error checking to see if all keys unique, if not dont load and send warning to console
        if (drinks.ContainsKey(drink.DrinkID))
        {
            Debug.LogWarning("Drink " + drink.Name + " key already exists as " + drinks[drink.DrinkID].Name);
        }
        else
        {
            drinks.Add(drink.DrinkID, drink);
        }

    }
    IEnumerator PickDrink()
    {
        //index for keeping track of generated drinks, should change later nb****
        var index = 0;
        //testing
        testIndex = UnityEngine.Random.Range(0, drinks.Count);
        individualDrink.Clear();
        individualDrinkValues.Clear();
        //currentGlassValues.Clear();
        DrinkInstructions temp = drinks[testIndex];
        if (temp.Rum != 0)
        {
            rumIndex = index;
            index++;
            individualDrink.Add("Rum: " + temp.Rum.ToString());
            individualDrinkValues.Add(currentGlassValues.Rum);
        }
        if (temp.Vodka != 0)
        {
            vodkaIndex = index;
            index++;
            individualDrink.Add("Vodka: " + temp.Vodka.ToString());
            individualDrinkValues.Add(currentGlassValues.Vodka);

        }
        if (temp.Coke != 0)
        {
            cokeIndex = index;
            index++;
            individualDrink.Add("Coke: " + temp.Coke.ToString());
            individualDrinkValues.Add(currentGlassValues.Coke);
        }
        string combindedString = string.Join("\n", individualDrink.ToArray());
        combindedString.Trim('"');
        instructions.text = combindedString;
        drinkName.text = temp.Name;
        yield return null;
    }
    //update the current drink values on the game screen
    IEnumerator UpdateValues()
    {
        temp = drinks[testIndex];
        if (temp.Rum != 0)
        {
            individualDrinkValues[rumIndex] = currentGlassValues.Rum;
        }
        if (temp.Vodka != 0)
        {
            individualDrinkValues[vodkaIndex] = currentGlassValues.Vodka;

        }
        if (temp.Coke != 0)
        {
            individualDrinkValues[cokeIndex] = currentGlassValues.Coke;
        }

        yield return null;
    }
    IEnumerator Score()
    {
        temp = drinks[testIndex];
        if (temp.Rum != 0)
        {
            baseScore -= Math.Abs((int)temp.Rum - (int)Math.Floor(currentGlassValues.Rum));
            Debug.LogError("Score Rum = " + baseScore);
        }
        if (temp.Vodka != 0)
        {
            baseScore -= Math.Abs((int)temp.Vodka - (int)Math.Floor(currentGlassValues.Vodka));
            Debug.LogError("Score Vodka = " + baseScore);

        }
        if (temp.Coke != 0)
        {
            baseScore -= (Math.Abs((int)temp.Coke - (int)Math.Floor(currentGlassValues.Coke)))/10;
            Debug.LogError("Score Coke = " + baseScore);
        }
        if (baseScore > minScore)
        {
            totalScore += baseScore;
        }
        else
        {
            totalScore += minScore;
        }
        currentScore.text = "Score: " + totalScore.ToString();
        yield return null;
    }
}
