using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeChecker : MonoBehaviour
{
    public List<string> Solutions = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        foreach(string item in Solutions)
        {
            if(collision.gameObject.name == item)
            {
                Debug.Log("Coolio");
            }
        }
            
    }
}
