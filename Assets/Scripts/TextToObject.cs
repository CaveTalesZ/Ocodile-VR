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
    public GameObject errorLog;
    private string errorMessage;
    private Object[] prefabList;
    private int position;
    private int nounPosition;
    private bool recolor;
    private string newColor;
    private int colorPosition;
    private bool resize;
    private float newScale;
    private int sizePosition;
    private bool succesfulSpawn;
    private string itemName;
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
    public GameObject convertToObject(string input)
    {
        succesfulSpawn = false;
        myPrefab = new GameObject();
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

        position = 0;
        // Checks all words for any nouns
        foreach (string possibleNoun in inputWords)
        {
            position = position + 1;
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
            myPrefab = (GameObject)Resources.Load("Objects/" + inputNouns[0]);
            succesfulSpawn = true;
            nounPosition = position;
            itemName = inputNouns[0];
            inputNouns.Clear();
        }
        else
        {
            if(inputNouns.Count == 0)
            {
                errorMessage = "No recognized nouns";
            }
            else
            {
                errorMessage = "Too many nouns";
                inputNouns.Clear();
            }
        }

        position = 0;
        colorPosition = 0;
        sizePosition = 0;
        newScale = 1;
        // Checks all words for possible uses as adjectives
        foreach (string possibleAdjective in inputWords)
        {
            position = position + 1;
            string adjective = possibleAdjective.ToLower();
            if (colors.Contains(adjective))
            {
                if (position < nounPosition)
                {
                    if (colorPosition == 0)
                    {
                        colorPosition = position;
                        newColor = adjective;
                        recolor = true;
                    }
                    else
                    {
                        errorMessage = "No more than 1 colour!";
                        succesfulSpawn = false;
                    }
                }
                else
                {
                    errorMessage = "Adjectives before nouns!";
                    succesfulSpawn = false;
                }
            }
            else foreach(stringFloatPair size in sizes)
            {
                
                if(adjective == size.name.ToLower())
                {
                    if (position < nounPosition)
                    {
                        sizePosition = position;
                        resize = true;
                        newScale = newScale * size.value;
                        
                    }
                    else
                    {
                        errorMessage = "Adjectives before nouns!";
                        succesfulSpawn = false;
                    }
                    
                }
            }
            //switch(adjective)
            //{
            //    case "edible":
            //    case "nutritious":
            //    case "delicious":
            //    case "tasty":
            //    case "scrumptious":
            //        item.GetComponent<ObjectTags>().Edible = true;
            //        break;
            //    default:
            //        break;
            //}
        }

        if(sizePosition > colorPosition && colorPosition > 0 && sizePosition > 0)
        {
            errorMessage = "Size before colour!";
            succesfulSpawn = false;
        }

        if(succesfulSpawn)
        {
            item = Instantiate(myPrefab,
                               transform.position,
                               Quaternion.identity);
            item.name = itemName;
            ModifySize(newScale);
            if(recolor)
            {
                ModifyColour(newColor);
            }
            errorLog.GetComponentInChildren<Text>().text = "";
            errorMessage = "";
        }
        else
        {
            Debug.Log(errorMessage);
            errorLog.GetComponentInChildren<Text>().text = errorMessage;
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
