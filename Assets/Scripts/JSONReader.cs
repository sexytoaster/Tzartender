//this script takes in a json file name and loads it as a text asset to be returned to another script
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonFileReader : MonoBehaviour
{
    //path holds path to json and json string holds raw json
    public static string LoadJsonAsResource(string path)
    {
        //remove json extension
        string jsonFilePath = path.Replace(".json", "");
        //loads json as a text asset and then return the text
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
        return loadedJsonFile.text;
    }
    

}
