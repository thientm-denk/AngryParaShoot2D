using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceGame : MonoBehaviour
{
    private void Awake()
    {
        if (!AudioManager.Initialized)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Initialze(audioSource);
         
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
