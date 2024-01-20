using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioClip pickup_sound, gameover_sound, background_sound;
    void Awake()
    {
        MakeInstance();
        AudioSource.PlayClipAtPoint(background_sound, transform.position);
    }

    // Update is called once per frame
    void MakeInstance()
    {
       if(instance == null){
        instance = this;
       } 
    }

    public void Play_pickupSound(){
        AudioSource.PlayClipAtPoint(pickup_sound, transform.position);
    }

    public void Play_gameoverSound(){
        AudioSource.PlayClipAtPoint(gameover_sound, transform.position);
    }
}
