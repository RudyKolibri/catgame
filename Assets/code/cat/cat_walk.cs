using LDtkUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cat_walk : MonoBehaviour
{
    LDtkFields data;
    int index = 0;
    public bool walking = false;
    public LDtkReferenceToAnEntityInstance[] test;
    public Vector2[] path;
    Transform cat;
    private void Start()
    {
        data = GetComponent<LDtkFields>();
        path = data.GetPointArray("path");
        test = data.GetEntityReferenceArray("othercats");
        start_movement();
        transform.position = path[0];
    }
    public void start_movement() // will be called by a play button
    {
        walking= true;
        Invoke("step", 0.5f);
    }
    public void step()
    {
        if (walking != false)
        {
            index += 1;
            transform.position = path[index];

            if (index == path.Length - 1)
            {
                Invoke("finish", 0.5f);
            }
            else
            {
                Invoke("step", 1f);
            }
        }

    }
    public void finish()
    {
        //particle effect
    }
    private void FixedUpdate()
    {
        foreach (LDtkReferenceToAnEntityInstance i in test) {
            GetComponent<CircleCollider2D>().enabled = false;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, i.FindEntity().transform.position- transform.position);
            Debug.DrawRay(transform.position, i.FindEntity().transform.position - transform.position);
            if (ray.collider != null)
            {
                
                
                if (ray.collider.tag == "cat")
                {
                    Debug.Log(ray.collider);
                    walking = false;
                    Invoke("fail", 0.1f);
                }
            }
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    private void fail()
    {
        transform.position = path[0];
        walking = false;
        index = 0;
    }
}
