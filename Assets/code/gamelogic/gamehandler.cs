using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class gamehandler : MonoBehaviour
{
    cat_walk[] cats;
    goal[] goals;
    box[] boxs; 
    int amount_done = 0;
    void Start()
    {
        cats = FindObjectsOfType<cat_walk>();
        goals = FindObjectsOfType<goal>();
       boxs = FindObjectsOfType<box>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            foreach (cat_walk c in cats)
            {
                c.fail();
                c.start_movement();

            }
            foreach (box b in boxs)
            {
                b.respawn();
            }
            
        }
    }
    public void restart()
    {
        foreach (cat_walk c in cats)
        {
            c.fail();
        }
        foreach (box b in boxs)
        {
            b.respawn();
        }
    }
    public void cat_done()
    {
        amount_done++;
        if (amount_done == cats.Length) {
            bool done = true;
            foreach(goal i in goals)
            {
                if (i.has_a_cat == false)
                {
                    Debug.Log("fail");
                    done = false;
                }
            }
            if (done) {
                Debug.Log("level completed");
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
            }
            amount_done = 0;
        }
    }
}
