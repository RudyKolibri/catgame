using LDtkUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class cat_walk : MonoBehaviour
{
    LDtkFields data;
    gamehandler handler;
    int index = 0;
    bool drawing = false;
    public bool walking = false;
    LineRenderer line;
    public LDtkReferenceToAnEntityInstance[] test;
    public Vector2[] path;
    private Camera _mainCamera;
    public Vector2[] newpath;
    Transform cat;
    private void Start()
    {
        
        handler = FindObjectOfType<gamehandler>();
        _mainCamera = Camera.main;
        data = GetComponent<LDtkFields>();
        path = new Vector2[] { new Vector2(transform.position.x, transform.position.y) };
        test = data.GetEntityReferenceArray("othercats");
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

    private void Update()
    {
        if (drawing)
        {
            drawing_path();
        }
    }
    public void start_movement() // will be called by a play button
    {
        walking= true;
        Debug.Log("start");
        Invoke("step", 0.5f);
    }
    public void step()
    {
        if (walking != false)
        {
            index += 1;
            gameObject.layer = 2;
            if (index < path.Length)
            {
                Debug.Log(index);
                RaycastHit2D ray = Physics2D.Raycast(transform.position, path[index] - (Vector2)transform.position, 1f);
                
                Debug.Log(path[index] - (Vector2)transform.position);
                if (ray.collider != null)
                {
                    Debug.Log(ray.collider);
                    if (ray.collider.tag == "box")
                    {
                        ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        RaycastHit2D ray2 = Physics2D.Raycast(ray.collider.transform.position, path[index] - (Vector2)transform.position, 1f);

                        if (ray2.collider != null)
                        {
                            Debug.Log(ray2.collider);
                            fail();
                        }
                        else
                        {
                            ray.collider.transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                            transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                            Invoke("step", 0.5f);
                        }
                        ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    }

                }
                else
                {
                    transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                    Invoke("step", 0.5f);
                }
            }
            else
            {
                finish();
            }
        }
    }
    public void finish()
    {
        handler.cat_done();
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
                    handler.restart();
                    
                }
            }            
            if (ray2.collider != null)
            {
                
                
                if (ray2.collider.tag == "cat")
                {
                    walking = false;
                    handler.restart();
                }
            }            
            if (ray3.collider != null)
            {
                
                
                if (ray3.collider.tag == "cat")
                {
                    walking = false;
                    handler.restart();
                }
            }            
            if (ray4.collider != null)
            {
                
                
                if (ray4.collider.tag == "cat")
                {
                    walking = false;
                    handler.restart();
                }
            }
            gameObject.layer = 0;
        }
    }
    public void fail()
    {
        Debug.Log(gameObject);
        walking = false;
        index = 0;
        Debug.Log(path.Length);
        transform.position = path[0];
    }
    public void catstay(Collider2D collision)
    {
        if (collision.tag == "cat")
        {
            gameObject.layer = 2;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, collision.gameObject.transform.position - transform.position);
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
    public void new_path()
    {
        drawing = true;
        Vector2[] newpath = { } ;
        
        index = 0;
    }
    public void drawing_path() {
        transform.position = path[0];
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = ( _mainCamera.ScreenToWorldPoint( Input.mousePosition) / 16 )* 16;
            Array.Resize(ref newpath, newpath.Length + 1);
            newpath[index] = new Vector2 (Mathf.Round(position.x + 0.5f)-0.5f, MathF.Round( position.y+0.5f)-0.5f); //fixen
            Debug.Log(newpath.Length);
            index ++;

            line = GetComponent<LineRenderer>();
            line.positionCount = newpath.Length;
            line.SetColors(new Color(0, 1, 0, 0.1f), new Color(0, 1, 0, 0.1f));
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;
            for (int index = 0; index <= (newpath.Length - 1); index++)
            {
                line.SetPosition(index, new Vector3(newpath[index].x, newpath[index].y, -1));
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(newpath);
            if (newpath.Length > 0)
            {
                path = newpath;
            }
            newpath = new Vector2[] { };
            drawing = false;
            line = GetComponent<LineRenderer>();
            line.positionCount = path.Length;
            line.SetColors(new Color(0, 1, 0, 0.1f), new Color(0, 1, 0, 0.1f));
            line.startWidth = 0.2f;
            line.endWidth = 0.2f;
            for (int index = 0; index <= (path.Length - 1); index++)
            {
                line.SetPosition(index, new Vector3(path[index].x, path[index].y, -1));
            }
        }
        
    }
}
