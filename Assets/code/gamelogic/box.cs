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
        transform.position = restart;
        Debug.Log("test");
    }
    
}
