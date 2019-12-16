using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

//An Array or List of a custom class like this will have a name variable for you to change
//[System.Serializable]//makes sure this shows up in the inspector
//public class TextureContain
//{
//    public string name;//your name variable to edit
//    public Texture tex;//place texture in here
//}
public class TextToObject : MonoBehaviour
{
    [System.Serializable]
    public class SynonymDictionary
    {
        public string name = "";
        public List<string> synonyms = new List<string>();
    }

    [System.Serializable]
    public class stringFloatPair
    {
        public string name = "";
        public float value = 0;
    }

    public List<SynonymDictionary> nouns = new List<SynonymDictionary>() {  };
    
    public List<string> colors = new List<string>() { "yellow", "green", "blue", "red", "black", "white" };
    public List<stringFloatPair> sizes = new List<stringFloatPair>();
    private GameObject myPrefab;
    private List<string> inputWords;
    private List<string> inputNouns;
    private GameObject item;
    private Object[] prefabList;
    public void GetInput(string input) {
        convertToObject(input);
    }

    // Start is called before the first frame update
    void Start()
    {

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
    GameObject convertToObject(string input)
    {
    inputWords = new List<string>();
    inputNouns = new List<string>();
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
            foreach (SynonymDictionary definition in nouns)
            {
                // Check for synonyms
                foreach(string synonym in definition.synonyms)
                {
                    if(noun == synonym)
                    {
                        noun = definition.name;
                    }
                }
                if (definition.name == noun)
                {
                    inputNouns.Add(noun);
                }
            }
        }

        // Turns a single noun into an object, error otherwise.
        if (inputNouns.Count == 1)
        {
            myPrefab = (GameObject)Resources.Load(inputNouns[0]);
            item = Instantiate(myPrefab,
                               transform.position,
                               Quaternion.identity);
            inputNouns.Clear();
        }
        else
        {
            if(inputNouns.Count == 0)
            {
                Debug.Log("No nouns!");
            }
            else
            {
                Debug.Log("Too many nouns:"+inputNouns.Count);
                inputNouns.Clear();
            }
        }

        // Checks all words for possible uses as adjectives
        foreach (string possibleAdjective in inputWords)
        {
            string adjective = possibleAdjective.ToLower();
            if (colors.Contains(adjective))
            {
                ModifyColour(adjective);
            }
            foreach(stringFloatPair size in sizes)
            {
                
                if(adjective == size.name.ToLower())
                {
                    ModifySize(size.value);
                }
            }
        }

        return item;
    }

    void ModifySize(float size)
    {
        item.transform.localScale = new Vector3(item.transform.localScale.x * size, item.transform.localScale.y * size, item.transform.localScale.z * size);
    }

    //Changes the colour of the object
    void ModifyColour(string input)
    {
        item.GetComponent<Renderer>().material.color = (Color)typeof(Color).GetProperty(input.ToLowerInvariant()).GetValue(null, null);
    }

   
    // Doesn't currently work, supposed to help automatically create support for new Objects
    Dictionary<string, List<string>> ListAllObjects()
    {
        var totalList = new Dictionary<string, List<string>>();
        //List<string> gameObjectList = new List<string>();
        Object[] objectList = Resources.LoadAll("", typeof(GameObject));

        foreach (GameObject newItem in objectList)
        {
            totalList[newItem.name] = new List<string>();
        }



        //foreach(GameObject in )

        return totalList;
    }
}
