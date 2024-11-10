using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamehandler : MonoBehaviour
{
    cat_walk[] cats;
    void Start()
    {
        cats = FindObjectsOfType<cat_walk>();
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
            
        }
    }
}
