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
    public AudioSource credits;
    
    public float offset;
    bool gameplay_looping = false;
    bool intro_looping = false;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (intromainmenu.time >= 15.5f && loopmainmenu.isPlaying == false)
        {
            
            loopmainmenu.Play();
            intro_looping = true;
            
        }if (loopmainmenu.time >= 191.5f && intro_looping)
        {
            loopmainmenu.time = 0;
            loopmainmenu.Play();
            
        }
        
        if (introgameplay.time >= 49.5f && loopgameplay.isPlaying == false)
        {

            loopgameplay.Play();
            gameplay_looping=true;
        }
        if (loopgameplay.time >= 169.5f && gameplay_looping)
        {
            loopgameplay.time = 0;
            loopgameplay.Play();
            

        }
    }
    public void changetogame()
    {

        Invoke("startgameplay",0.2f);
    }
    public void changetocredits()
    {

        Invoke("startcredits",0.2f);
    }
    public void changetointro()
    {

        Invoke("startintro",0.2f);
    }
    private void startgameplay()
    {
        loopmainmenu.Pause();
        intromainmenu.Pause();
        introgameplay.Play();
        credits.Pause();
        gameplay_looping = false ;
        intro_looping=false ;
    }
    private void startcredits()
    {
        loopgameplay.Pause();
        introgameplay.Pause();
        intromainmenu.Pause();
        intromainmenu.time = 0;
        credits.time = 0;
        credits.Play();
        gameplay_looping = false ;
        intro_looping =false ;
    }
    private void startintro()
    {
        loopgameplay.Pause();
        introgameplay.Pause();
        credits.Pause();
        intromainmenu.Play();
        intromainmenu.time = 0;
        
        gameplay_looping = false ;
        intro_looping =false ;
    }
}
