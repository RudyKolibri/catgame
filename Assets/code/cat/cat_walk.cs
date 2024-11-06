using LDtkUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cat_walk : MonoBehaviour
{
    LDtkFields data;
    int index = 0;
    public bool walking = false;
    LineRenderer line;
    public LDtkReferenceToAnEntityInstance[] test;
    public Vector2[] path;
    Transform cat;
    private void Start()
    {
        data = GetComponent<LDtkFields>();
        path = data.GetPointArray("path");
        test = data.GetEntityReferenceArray("othercats");
        transform.position = path[0];
        line = GetComponent<LineRenderer>();
        line.positionCount = path.Length;
        line.SetColors(new Color(0,1,0,0.1f), new Color(0, 1, 0, 0.1f));
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        for (int index = 0; index <= (path.Length-1); index++)
        {
            line.SetPosition(index,new Vector3(path[index].x, path[index].y,-1));
        }
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
            gameObject.layer = 2;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, path[index]- new Vector2(transform.position.x,transform.position.y),1f);
            if (ray.collider == null )
            {
                gameObject.layer = 0;
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
            else
            {
                gameObject.layer = 0;
                Debug.Log(ray.collider);
                fail();
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
            gameObject.layer = 2;
            RaycastHit2D ray1 = Physics2D.Raycast(transform.position, new Vector3(20, 0));
            RaycastHit2D ray2 = Physics2D.Raycast(transform.position, new Vector3(-20, 0));
            RaycastHit2D ray3 = Physics2D.Raycast(transform.position, new Vector3(0, 20, 0));
            RaycastHit2D ray4 = Physics2D.Raycast(transform.position, new Vector3(0,-20, 0));
            if (ray1.collider != null)
            {
                
                
                if (ray1.collider.tag == "cat")
                {
                    walking = false;
                    Invoke("fail", 2f);
                }
            }            
            if (ray2.collider != null)
            {
                
                
                if (ray2.collider.tag == "cat")
                {
                    walking = false;
                    Invoke("fail", 2f);
                }
            }            
            if (ray3.collider != null)
            {
                
                
                if (ray3.collider.tag == "cat")
                {
                    walking = false;
                    Invoke("fail", 2f);
                }
            }            
            if (ray4.collider != null)
            {
                
                
                if (ray4.collider.tag == "cat")
                {
                    walking = false;
                    Invoke("fail", 2f);
                }
            }
            gameObject.layer = 0;
        }
    }
    public void fail()
    {
        Debug.Log("fail");
        
        transform.position = path[0];
        walking = false;
        index = 0;
    }
    public void catstay(Collider2D collision)
    {
        if (collision.tag == "cat")
        {
            gameObject.layer = 2;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, collision.gameObject.transform.position - transform.position);
            Debug.DrawRay(transform.position, collision.transform.position - transform.position);
            if (ray.collider != null)
            {
                if (ray.collider.tag == "cat")
                {
                    walking = false;
                    Invoke("fail", 2f);
                }
            }
            gameObject.layer = 0;
        }
    }
}
