using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//An Array or List of a custom class like this will have a name variable for you to change
//[System.Serializable]//makes sure this shows up in the inspector
//public class TextureContain
//{
//    public string name;//your name variable to edit
//    public Texture tex;//place texture in here
//}
public class TextToObject : MonoBehaviour
{

    public List<string> nouns = new List<string>();
    private List<string> colors = new List<string>() { "yellow", "green", "blue", "red", "black", "white" };
    private GameObject myPrefab;
    public string input;
    private List<string> inputWords = new List<string>();
    private List<string> inputNouns = new List<string>();
    private GameObject item;
    private Object[] prefabList;

    // Start is called before the first frame update
    void Start()
    {
        convertToObject(input);
    }

    /* Get (input)
     * Check (input) against list of nouns
     * else check (input) against list(s) of adjectives (color list, size list, w/e)
     * if found noun, check if noun in synonym list, if so, use synonym list's name instead
     * take noun
     *
     *
    */



    // Update is called once per frame
    void Update()
    {

    }

    // Takes a string as input and turns it into a fully fledged object
    void convertToObject(string input)
    {
        // Separates input into multiple words if necessary
        string word = "";
        for (int i = 0; i < input.Length; i++)
        {
            string c = "" + input[i];
            if (c == " ")
            {
                inputWords.Add(word);
                word = "";
            }
            else
            {
                word = word + input[i];
            }

        }
        inputWords.Add(word);

        
        // Checks all words for any nouns
        foreach (string possibleNoun in inputWords)
        {
            string noun = possibleNoun[0].ToString().ToUpper() + possibleNoun.Substring(1).ToLower();
            if (nouns.Contains(noun))
            {
                inputNouns.Add(noun);
            }
        }

        // Turns a single noun into an object, error otherwise.
        if (inputNouns.Count == 1)
        {
            myPrefab = (GameObject)Resources.Load(inputNouns[0]);
            item = Instantiate(myPrefab,
                               transform.position,
                               Quaternion.identity);
        }
        else
        {
            if(inputNouns.Count == 0)
            {
                Debug.Log("No nouns!");
            }
            else
            {
                Debug.Log("Too many nouns!");
            }
        }

        foreach (string possibleAdjective in inputWords)
        {
            string adjective = possibleAdjective.ToLower();
            if (colors.Contains(adjective))
            {
                ModifyColour(adjective);
            }
        }
        
    }

    //Changes the colour of the object
    void ModifyColour(string input)
    {
        item.GetComponent<Renderer>().material.color = (Color)typeof(Color).GetProperty(input.ToLowerInvariant()).GetValue(null, null);
    }
}
