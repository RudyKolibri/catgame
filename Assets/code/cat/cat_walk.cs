using LDtkUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class cat_walk : MonoBehaviour
{
    
    LDtkFields data;
    gamehandler handler;
    GameObject[] voids;
    GameObject[] glasss;
    int index = 0;
    public GameObject box_fallen;
    Vector2 mouse_position_old;
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
        
        Invoke("step", handler.catspeed);
    }
    public void step()
    {
        if (walking != false)
        {
            index += 1;
            gameObject.layer = 2;
            if (index < path.Length)
            {
                
                RaycastHit2D ray = Physics2D.Raycast(transform.position, path[index] - (Vector2)transform.position, 1f);
                
                
                if (ray.collider != null)
                {
                    
                    if (ray.collider.tag == "box")
                    {
                        ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        RaycastHit2D ray2 = Physics2D.Raycast(ray.collider.transform.position, path[index] - (Vector2)transform.position, 1f);

                        if (ray2.collider != null)
                        {
                            
                            if (ray2.collider.tag == "void")
                            {
                                Instantiate(box_fallen,ray2.collider.transform.position,ray2.collider.transform.rotation);
                                ray.collider.gameObject.GetComponent<box>().respawn();
                                ray.collider.GetComponent<BoxCollider2D>().enabled=false;
                                ray.collider.gameObject.GetComponent<SpriteRenderer>().enabled=false;
                                Destroy(ray2.collider.gameObject);

                                ray.collider.transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                                transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                                Invoke("step", handler.catspeed);
                            }
                            else
                            {
                                Invoke("fail",handler.catspeed);
                                handler.cat_done();
                            }//pipikaka
                        }
                        else
                        {
                            ray.collider.transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                            transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                            Invoke("step", handler.catspeed);
                        }
                        ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    else
                    {
                        Invoke("fail", handler.catspeed);
                        handler.cat_done();
                    }
                }
                else
                {
                    transform.position += ((Vector3)path[index] - (Vector3)transform.position);
                    Invoke("step", handler.catspeed);
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
            voids = GameObject.FindGameObjectsWithTag("void");
            glasss = GameObject.FindGameObjectsWithTag("glass");
            foreach (GameObject v in voids)
            {
                v.GetComponent<BoxCollider2D>().enabled = false;
            }
            foreach (GameObject g in glasss)
            {
                g.GetComponent<BoxCollider2D>().enabled = false;
            }
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
            foreach (GameObject v in voids)
            {
                v.GetComponent<BoxCollider2D>().enabled = true;
            }
            foreach (GameObject g in glasss)
            {
                g.GetComponent<BoxCollider2D>().enabled = true;
            }
            gameObject.layer = 0;
        }
    }
    public void fail()
    {
        
        walking = false;
        index = 0;
        
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
                    Invoke("fail", handler.catspeed);
                }
            }
            gameObject.layer = 0;
        }
    }
    public void new_path()
    {
        drawing = true;
        walking = false ;
        Vector2[] newpath = {} ;
        
        index = 0;
        Vector2 position = (_mainCamera.ScreenToWorldPoint(Input.mousePosition) / 16) * 16;

        mouse_position_old = transform.position;
    }
    public void drawing_path() {
        transform.position = path[0];
        if ((Vector2) transform.position != path[0])
        {
            drawing = false;
            
            walking = false;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 position = (_mainCamera.ScreenToWorldPoint(Input.mousePosition) / 16) * 16;
            if (index == 0)
            {
                Array.Resize(ref newpath, newpath.Length + 1);
                newpath[0] = transform.position;
                index++;
                mouse_position_old = transform.position;
            }
            else if (mouse_position_old != new Vector2(Mathf.Round(position.x + 0.5f) - 0.5f, MathF.Round(position.y + 0.5f) - 0.5f))
            {
                Array.Resize(ref newpath, newpath.Length + 1);

                if (mouse_position_old.x != Mathf.Round(position.x + 0.5f) - 0.5f)
                {
                    if (index == newpath.Length - 1)
                    {
                        if (mouse_position_old.x - (Mathf.Round(position.x + 0.5f) - 0.5f) < 0)
                        {
                            newpath[index] = new Vector2(mouse_position_old.x + 1, mouse_position_old.y);
                            mouse_position_old = newpath[index];
                            index++;
                        }else {                        
                            newpath[index] = new Vector2(mouse_position_old.x -1, mouse_position_old.y);
                            mouse_position_old = newpath[index];
                            index++;
                        }
                    }
                }
                else if (mouse_position_old.y != Mathf.Round(position.y + 0.5f) - 0.5f)
                {
                    if (index == newpath.Length - 1)
                    {
                        if (mouse_position_old.y - (Mathf.Round(position.y + 0.5f) - 0.5f) < 0)
                        {
                            newpath[index] = new Vector2(mouse_position_old.x, mouse_position_old.y + 1);
                            mouse_position_old = newpath[index];
                            index++;
                            
                        }else
                        {
                            newpath[index] = new Vector2(mouse_position_old.x, mouse_position_old.y - 1);
                            mouse_position_old = newpath[index];
                            index++;
                            
                        }
                    }
                }

                
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
            
        }

        if (Input.GetMouseButton(0) == false)
        {
            
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
