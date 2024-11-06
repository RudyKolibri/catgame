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
            gameObject.layer = 2;
            RaycastHit2D ray1 = Physics2D.Raycast(transform.position, new Vector3(20, 0));
            RaycastHit2D ray2 = Physics2D.Raycast(transform.position, new Vector3(-20, 0));
            RaycastHit2D ray3 = Physics2D.Raycast(transform.position, new Vector3(0, 20, 0));
            RaycastHit2D ray4 = Physics2D.Raycast(transform.position, new Vector3(0,-20, 0));
            Debug.DrawRay(transform.position,  new Vector3(20,0));
            Debug.DrawRay(transform.position, new Vector3(-20, 0));
            Debug.DrawRay(transform.position, new Vector3(0,20));
            Debug.DrawRay(transform.position, new Vector3(0,-20));
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
    private void fail()
    {
        Debug.Log("fail");
        transform.position = path[0];
        walking = false;
        index = 0;
    }
    public void catstay(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.tag == "cat")
        {
            Debug.Log(collision);
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
