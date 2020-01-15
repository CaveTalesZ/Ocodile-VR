using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
    public Button b_Enter, b_Backspace, b_Space, b_A, b_B, b_C, b_D, b_E, b_F, b_G, b_H, b_I, b_J, b_K, b_L, b_M, b_N, b_O, b_P, b_Q, b_R, b_S, b_T, b_U, b_V, b_W, b_X, b_Y, b_Z;
    private List<Button> Letters;
    public GameObject Preview;
    public GameObject objectSpawner;
    private string PreviewText;
    // Start is called before the first frame update
    void Start()
    {
        Letters = new List<Button>() {b_Space, b_A, b_B, b_C, b_D, b_E, b_F, b_G, b_H, b_I, b_J, b_K, b_L, b_M, b_N, b_O, b_P, b_Q, b_R, b_S, b_T, b_U, b_V, b_W, b_X, b_Y, b_Z };
        foreach(Button letter in Letters)
        {
            letter.onClick.AddListener(delegate { AddLetter(letter.GetComponentInChildren<Text>().text); } );
        }
        b_Backspace.onClick.AddListener(RemoveLetter);
        b_Enter.onClick.AddListener(SpawnCube);
        
    }

    public void AddLetter(string character)
    {
        PreviewText = Preview.GetComponentInChildren<Text>().text;
        PreviewText = PreviewText + character;
        Preview.GetComponentInChildren<Text>().text = PreviewText;
    }

    public void RemoveLetter()
    {
        PreviewText = Preview.GetComponentInChildren<Text>().text;
        PreviewText = PreviewText.Substring(0, PreviewText.Length - 1);
        Preview.GetComponentInChildren<Text>().text = PreviewText;
    }

    public void SpawnCube()
    {
        objectSpawner.GetComponent<TextToObject>().convertToObject(PreviewText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
