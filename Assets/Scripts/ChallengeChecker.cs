using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeChecker : MonoBehaviour
{
    public List<string> Solutions = new List<string>();
    public AudioSource PlayerSound;
    public AudioSource SolvedSound;
    public AudioSource NotSolvedSound;
    public AudioSource NigelSound;
    bool ChallengeSolved = false;

    // Start is called before the first frame update
    void Start()
    {
     AudioSource[] Sounds = GetComponents<AudioSource>();
     PlayerSound = Sounds[0];
     SolvedSound = Sounds[1];
     NotSolvedSound = Sounds[2];
     NigelSound = Sounds[3];
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
                if (ChallengeSolved == false)
                {
                NotSolvedSound.Stop();
                ChallengeSolved = true;
                Debug.Log("Coolio");
                SolvedSound.Play();
                }
            }
            else if(collision.gameObject.name == "OVRPlayerController")
            {
                if (ChallengeSolved == false)
                {
                Debug.Log("Hello");
                PlayerSound.Play();
                NotSolvedSound.Stop();
                }
            }
            else if(collision.gameObject.name == "Nigel")
            {
                Debug.Log("Hi Nigel");
                NigelSound.Play();
            }
            else
            {
                if (ChallengeSolved == false)
                {
                Debug.Log("Wrong");
                NotSolvedSound.Play();
                }
            }
        }
            
    }
}
