using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
    public Button b_Enter, b_Backspace, b_Space, b_Left, b_Right, b_A, b_B, b_C, b_D, b_E, b_F, b_G, b_H, b_I, b_J, b_K, b_L, b_M, b_N, b_O, b_P, b_Q, b_R, b_S, b_T, b_U, b_V, b_W, b_X, b_Y, b_Z;
    private List<Button> Letters;
    public GameObject Preview;
    public GameObject objectSpawner;
    public GameObject Error;
    private string PreviewText;
    public GameObject EventSystemManager;
    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log("It's started at all");
        Preview.GetComponent<CustomInputField>().ActivateInputField();
        //Preview.GetComponent<CustomInputField>().ForceLabelUpdate();
        //EventSystemManager.currentSystem.SetSelectedGameObject(Preview.gameObject, null);
        //Preview.OnPointerClick(null);
        //Preview.GetComponentInChildren<Text>().text = Preview.GetComponent<CustomInputField>().text;
        Letters = new List<Button>() { b_Space, b_A, b_B, b_C, b_D, b_E, b_F, b_G, b_H, b_I, b_J, b_K, b_L, b_M, b_N, b_O, b_P, b_Q, b_R, b_S, b_T, b_U, b_V, b_W, b_X, b_Y, b_Z };
        foreach (Button letter in Letters)
        {
            letter.onClick.AddListener(delegate { AddLetter(letter.GetComponent<CustomInputField>().text); });
        }
        b_Backspace.onClick.AddListener(RemoveLetter);
        b_Enter.onClick.AddListener(SpawnCube);
        b_Left.onClick.AddListener(delegate { MoveCaret(-1); });
        b_Right.onClick.AddListener(delegate { MoveCaret(1); });

        
    }

    public void AddLetter(string character)
    {
        PreviewText = Preview.GetComponent<CustomInputField>().text;
        PreviewText = PreviewText.Substring(0, Preview.GetComponent<CustomInputField>().caretPosition) + character + PreviewText.Substring(Preview.GetComponent<CustomInputField>().caretPosition);
        Preview.GetComponent<CustomInputField>().text = PreviewText;
        Preview.GetComponent<CustomInputField>().caretPosition += 1;
    }

    public void MoveCaret(int direction)
    {
        Preview.GetComponent<CustomInputField>().caretPosition += direction;
    }

    public void RemoveLetter()
    {
        PreviewText = Preview.GetComponent<CustomInputField>().text;
        PreviewText = PreviewText.Substring(0, Preview.GetComponent<CustomInputField>().caretPosition - 1) + PreviewText.Substring(Preview.GetComponent<CustomInputField>().caretPosition);
        Preview.GetComponent<CustomInputField>().text = PreviewText;
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
