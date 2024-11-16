using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using LDtkUnity;
using UnityEngine.UI;

public class gamehandler : MonoBehaviour
{
    cat_walk[] cats;
    goal[] goals;
    box[] boxs;
    public Sprite speed1;
    public Sprite speed2;
    public Sprite speed3;
    public Image button;
    public float catspeed = 0.5f;
    boxfall[] boxesfall;
    private Camera _mainCamera;
    LDtkComponentLevel[] levels;
    public GameObject currentlevel;
    public int level;
    int amount_done = 0;
    int speedindex = 1;
    void Start()
    {
        _mainCamera = Camera.main;
        levels = FindObjectsOfType<LDtkComponentLevel>();
    }
    public void changespeed()
    {
        speedindex++;
        if (speedindex > 3)
        {
            speedindex = 1;
        }
        if (speedindex == 1) {
            button.sprite = speed1;
            catspeed = 0.5f;
        } else if (speedindex == 2)
        {
            catspeed = 0.25f;
            button.sprite = speed2;
        }
        else
        {
            catspeed = 0.0625f;
            button.sprite = speed3;
        }
    }
    public void playbuttonclick()
    {
        cats = FindObjectsOfType<cat_walk>();
        goals = FindObjectsOfType<goal>();
        boxs = FindObjectsOfType<box>();
        boxesfall = FindObjectsOfType<boxfall>();
        foreach (cat_walk c in cats)
        {
            c.fail();
            c.start_movement();

        }
        foreach (box b in boxs)
        {

            b.respawn();
        }
        foreach (boxfall d in boxesfall)
        {
            d.respawn();
        }
    }
    private void Update()
    {
        foreach (LDtkComponentLevel c in levels)
        {
            if (c.gameObject != currentlevel)
            {
                c.gameObject.SetActive(false);
            }
            else {
                c.gameObject.SetActive(true);
            }
        }
    }
    public void restart()
    {
        cats = FindObjectsOfType<cat_walk>();
        goals = FindObjectsOfType<goal>();
        boxs = FindObjectsOfType<box>();
        boxesfall = FindObjectsOfType<boxfall>();
        Debug.Log(GameObject.FindGameObjectsWithTag("boxfallen"));
        foreach (cat_walk c in cats)
        {
            c.fail();
        }
        foreach (box b in boxs)
        {
            b.respawn();
        }
        foreach(boxfall d in boxesfall)
        {
            d.respawn();
        }
    }
    public void cat_done()
    {
        cats = FindObjectsOfType<cat_walk>();
        goals = FindObjectsOfType<goal>();
        boxs = FindObjectsOfType<box>();
        boxesfall = FindObjectsOfType<boxfall>();
        amount_done++;
        if (amount_done == cats.Length) {
            bool done = true;
            foreach(goal i in goals)
            {
                if (i.has_a_cat == false)
                {
                    
                    done = false;
                }
            }
            if (done) {
                Debug.Log("level completed");
                level++;
                foreach(LDtkNeighbour n in currentlevel.GetComponent<LDtkComponentLevel>().Neighbours)
                {
                    if (n.IsEast)
                    {
                        currentlevel = n.FindLevel().gameObject;
                        _mainCamera.gameObject.transform.position = new Vector3( n.FindLevel().gameObject.transform.position.x + (n.FindLevel().GetComponent<LDtkComponentLevel>().Size.x/2),_mainCamera.gameObject.transform.position.y,_mainCamera.transform.position.z );
                        
                    }
                }
            }
            else
            {
                foreach(cat_walk c in cats)
                {
                    c.fail();
                }
                foreach(box b in boxs)
                {
                    
                    b.respawn();
                }
                foreach (boxfall d in boxesfall)
                {
                    d.respawn();
                }
            }
            amount_done = 0;
        }
    }
}
