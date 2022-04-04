using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance;
    public AudioSource ResourcePickedUp;
    void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    public void ResourcePickedUpSound()
    {
        ResourcePickedUp.Play();
    }
}
