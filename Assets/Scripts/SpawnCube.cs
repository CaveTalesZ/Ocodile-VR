using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject ObjectSpawner = new GameObject();
    // Start is called before the first frame update
    void Start()
    {
        TextToObject script = ObjectSpawner.GetComponent<TextToObject>();
        //script.convertToObject("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
