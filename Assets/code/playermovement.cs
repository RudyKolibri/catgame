using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playermovement : MonoBehaviour
{

    cat_walk[] cat;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            cat = FindObjectsOfType<cat_walk>();
            foreach(cat_walk i in cat)
            {
                i.fail();
                i.start_movement();
            }
            
            Debug.Log("start");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector3(-0.5f, 0), 1f);

            if (ray.collider != null)
            {
                Debug.Log(ray.collider);
                if (ray.collider.tag == "box")
                {
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    RaycastHit2D ray2 = Physics2D.Raycast(ray.collider.transform.position, new Vector3(-0.5f, 0), 1f);

                    if (ray2.collider != null)
                    {
                        Debug.Log(ray2.collider);
                    }
                    else
                    {
                        ray.collider.transform.position += new Vector3(-1, 0);
                        transform.position += new Vector3(-1, 0);
                    }
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                
            }
            else
            {
                transform.position += new Vector3(-1, 0);
            }
        }        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector3(0.5f, 0),1f);
            
            if (ray.collider != null)
            {
                Debug.Log(ray.collider);
                if (ray.collider.tag == "box")
                {
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    RaycastHit2D ray2 = Physics2D.Raycast(ray.collider.transform.position, new Vector3(0.5f, 0), 1f);

                    if (ray2.collider != null)
                    {
                        Debug.Log(ray2.collider);
                    }
                    else
                    {
                        ray.collider.transform.position += new Vector3(1, 0);
                        transform.position += new Vector3(1, 0);
                    }
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            else
            {
                transform.position += new Vector3(1, 0);
            }
        }        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector3(0,0.5f, 0), 1f);

            if (ray.collider != null)
            {
                Debug.Log(ray.collider);
                if (ray.collider.tag == "box")
                {
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    RaycastHit2D ray2 = Physics2D.Raycast(ray.collider.transform.position, new Vector3(0,0.5f, 0), 1f);

                    if (ray2.collider != null)
                    {
                        Debug.Log(ray2.collider);
                    }
                    else
                    {
                        ray.collider.transform.position += new Vector3(0,1, 0);
                        transform.position += new Vector3(0,1, 0);
                    }
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            else
            {
                transform.position += new Vector3(0,1, 0);
            }
        }        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector3(0, -0.5f, 0), 1f);

            if (ray.collider != null)
            {
                Debug.Log(ray.collider);
                if (ray.collider.tag == "box")
                {
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    RaycastHit2D ray2 = Physics2D.Raycast(ray.collider.transform.position, new Vector3(0, -0.5f, 0), 1f);

                    if (ray2.collider != null)
                    {
                        Debug.Log(ray2.collider);
                    }
                    else
                    {
                        ray.collider.transform.position += new Vector3(0, -1, 0);
                        transform.position += new Vector3(0,- 1, 0);
                    }
                    ray.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            else
            {
                transform.position += new Vector3(0, -1, 0);
            }
        }
    }
}
