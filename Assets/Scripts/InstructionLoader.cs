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
    //reference to the glass
    public GameObject glass;
    //values inside glass
    public DrinkInstructions currentGlassValues;
    //lists for values
    public List<string> individualDrink;
    public List<float> individualDrinkValues;
    public List<float> displayedIndividualDrinkValues;

    public DrinkInstructions temp;
    //indexes for the drink
    public int rumIndex;
    public int vodkaIndex;
    public int cokeIndex;


    private void Awake()
    {
        //create a new dictionary containing our drinks
        drinks = new Dictionary<int, DrinkInstructions>();
        //parse json into dict
        DrinkDictionary dictionary = JsonUtility.FromJson<DrinkDictionary>(JsonFileReader.LoadJsonAsResource("Drinks/drinkDictionary.json"));
        foreach(string dictionaryDrink in dictionary.Drinks)
        {
            //add drinks from dictionary into drinks array
            LoadDrink(dictionaryDrink);
        }
        //for testing to see dict entries
        foreach(KeyValuePair<int, DrinkInstructions> entry in drinks)
        {
            DrinkInstructions temp = entry.Value;
            drinksList.Add(temp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //index for keeping track of generated drinks, should change later nb****
        var index = 0;
        //get reference to glass values
        currentGlassValues = glass.GetComponentInChildren<wobble>().currentValues;
        //testing
        DrinkInstructions temp = drinks[0];
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
    }

    // Update is called once per frame
    void Update()
    {
        temp = drinks[0];
        StartCoroutine("UpdateValues");
        
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
        } else
        {
            drinks.Add(drink.DrinkID, drink);
        }
        
    }

    //update the current drink values on the game screen
    IEnumerator UpdateValues()
    {
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
}
