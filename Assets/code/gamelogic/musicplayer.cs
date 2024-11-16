using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    AudioSource[] audioplayers;
    AudioSource audioplayer1;AudioSource audioplayer2;
    public float offset;
    
    private void Start()
    {
        audioplayers = GetComponents<AudioSource>();
        foreach (AudioSource source in audioplayers)
        {
            if (source.isPlaying)
            {
                audioplayer1 = source;
            }
            else { audioplayer2 = source; }
        }
    }
    private void Update()
    {
        if (audioplayer1.time >= 15.5f && audioplayer2.isPlaying == false)
        {
            
            audioplayer2.Play();
            
        }
    }
}
