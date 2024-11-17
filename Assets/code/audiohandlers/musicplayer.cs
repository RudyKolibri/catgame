using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    AudioSource[] audioplayers;
    public AudioSource intromainmenu;
    public AudioSource loopmainmenu;
    public AudioSource introgameplay;
    public AudioSource loopgameplay;
    
    public float offset;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (intromainmenu.time >= 15.5f && loopmainmenu.isPlaying == false)
        {
            
            loopmainmenu.Play();
            
        }
        if (introgameplay.time >= 49.5f && loopgameplay.isPlaying == false)
        {

            loopgameplay.Play();

        }
    }
    public void changetogame()
    {

        Invoke("startgameplay",0.2f);
    }
    private void startgameplay()
    {
        loopmainmenu.Pause();
        intromainmenu.Pause();
        introgameplay.Play();
    }
}
