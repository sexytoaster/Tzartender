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
        if (temp.Vodka != 0)
        {
            individualDrink.Add("Vodka: " + temp.Vodka.ToString());
            individualDrinkValues.Add(currentGlassValues.Vodka);

        }
        if (temp.Rum != 0)
        {
            individualDrink.Add("Rum: " + temp.Rum.ToString());
            individualDrinkValues.Add(currentGlassValues.Rum);
        }
        if (temp.Tequila != 0)
        {
            individualDrink.Add("Tequila: " + temp.Tequila.ToString());
            individualDrinkValues.Add(currentGlassValues.Tequila);
        }
        if (temp.Gin != 0)
        {
            individualDrink.Add("Gin: " + temp.Gin.ToString());
            individualDrinkValues.Add(currentGlassValues.Gin);
        }
        if (temp.Whiskey != 0)
        {
            individualDrink.Add("Whiskey: " + temp.Whiskey.ToString());
            individualDrinkValues.Add(currentGlassValues.Whiskey);
        }
        if (temp.Coke != 0)
        {
            individualDrink.Add("Coke: " + temp.Coke.ToString());
            individualDrinkValues.Add(currentGlassValues.Coke);
        }
        if (temp.Tonic != 0)
        {
            individualDrink.Add("Tonic: " + temp.Tonic.ToString());
            individualDrinkValues.Add(currentGlassValues.Tonic);
        }
        if (temp.Soda != 0)
        {
            individualDrink.Add("Soda: " + temp.Soda.ToString());
            individualDrinkValues.Add(currentGlassValues.Soda);
        }
        if (temp.Ginger != 0)
        {
            individualDrink.Add("Ginger: " + temp.Ginger.ToString());
            individualDrinkValues.Add(currentGlassValues.Ginger);
        }
        if (temp.Orange != 0)
        {
            individualDrink.Add("Orange: " + temp.Orange.ToString());
            individualDrinkValues.Add(currentGlassValues.Orange);
        }
        if (temp.LimeJuice != 0)
        {
            individualDrink.Add("Lime: " + temp.LimeJuice.ToString());
            individualDrinkValues.Add(currentGlassValues.LimeJuice);
        }
        if (temp.LemonJuice != 0)
        {
            individualDrink.Add("Rum: " + temp.LemonJuice.ToString());
            individualDrinkValues.Add(currentGlassValues.LemonJuice);
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
        var index = 0;
        temp = drinks[testIndex];
        
        if (temp.Vodka != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Vodka;
            index++;
        }
        if (temp.Rum != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Rum;
            index++;
        }
        if (temp.Tequila != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Tequila;
            index++;
        }
        if (temp.Gin != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Gin;
            index++;
        }
        if (temp.Whiskey != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Whiskey;
            index++;
        }
        if (temp.Coke != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Coke;
            index++;
        }
        if (temp.Tonic != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Tonic;
            index++;
        }
        if (temp.Soda != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Soda;
            index++;
        }
        if (temp.Ginger != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Ginger;
            index++;
        }
        if (temp.Orange != 0)
        {
            individualDrinkValues[index] = currentGlassValues.Orange;
            index++;
        }
        if (temp.LimeJuice != 0)
        {
            individualDrinkValues[index] = currentGlassValues.LimeJuice;
            index++;
        }
        if (temp.LemonJuice != 0)
        {
            individualDrinkValues[index] = currentGlassValues.LemonJuice;
            index++;
        }

        yield return null;
    }
    IEnumerator Score()
    {
        temp = drinks[testIndex];
        if (temp.Vodka != 0)
        {
            baseScore -= Math.Abs((int)temp.Vodka - (int)Math.Floor(currentGlassValues.Vodka));
            Debug.LogError("Score Vodka = " + baseScore);

        }
        if (temp.Rum != 0)
        {
            baseScore -= Math.Abs((int)temp.Rum - (int)Math.Floor(currentGlassValues.Rum));
            Debug.LogError("Score Rum = " + baseScore);
        }
        if (temp.Tequila != 0)
        {
            baseScore -= Math.Abs((int)temp.Tequila - (int)Math.Floor(currentGlassValues.Tequila));
        }
        if (temp.Gin != 0)
        {
            baseScore -= Math.Abs((int)temp.Gin - (int)Math.Floor(currentGlassValues.Gin));
        }
        if (temp.Whiskey != 0)
        {
            baseScore -= Math.Abs((int)temp.Whiskey - (int)Math.Floor(currentGlassValues.Whiskey));
        }
        if (temp.Coke != 0)
        {
            baseScore -= (Math.Abs((int)temp.Coke - (int)Math.Floor(currentGlassValues.Coke)))/10;
            Debug.LogError("Score Coke = " + baseScore);
        }
        if (temp.Tonic != 0)
        {
            baseScore -= Math.Abs((int)temp.Tonic - (int)Math.Floor(currentGlassValues.Tonic))/10;
        }
        if (temp.Soda != 0)
        {
            baseScore -= Math.Abs((int)temp.Soda - (int)Math.Floor(currentGlassValues.Soda))/10;
        }
        if (temp.Ginger != 0)
        {
            baseScore -= Math.Abs((int)temp.Ginger - (int)Math.Floor(currentGlassValues.Ginger))/10;
        }
        if (temp.Orange != 0)
        {
            baseScore -= Math.Abs((int)temp.Orange - (int)Math.Floor(currentGlassValues.Orange))/10;
        }
        if (temp.LimeJuice != 0)
        {
            baseScore -= Math.Abs((int)temp.LimeJuice - (int)Math.Floor(currentGlassValues.LimeJuice))/10;
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
