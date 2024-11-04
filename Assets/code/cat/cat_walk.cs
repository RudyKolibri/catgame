using LDtkUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_walk : MonoBehaviour
{
    LDtkFields data;
    int index = 0;
    public Vector2[] path;
    Transform cat;
    private void Start()
    {
        data = GetComponent<LDtkFields>();
        path = data.GetPointArray("path");
        start_movement();
        transform.position = path[0];
    }
    public void start_movement() // will be called by a play button
    {
        Invoke("step", 0.5f);
    }
    public void step()
    {
        index += 1;
        transform.position = path[index];
        
        if (index == path.Length - 1) {
            Invoke("finish", 0.5f);
        }
        else
        {
            Invoke("step", 0.5f);
        }
    }
    public void finish()
    {
        //particle effect
    }
    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, cat.position - transform.position);
        if (ray.collider != null)
        {
            //hit
            Invoke("fail",0.1f);
        }
    }
    private void fail()
    {
        transform.position = path[0];
        index = 0;
    }
}
