using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    Vector2 restart;
    
    
    
    void Start()
    {
        restart = transform.position;
        
    }

    public void respawn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
        transform.position = restart;
        Debug.Log("test");
    }public void voided()
    {
        
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        transform.position = restart;
        Debug.Log("test");
    }
    
}
