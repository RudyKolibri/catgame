using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    gamehandler handler;
    public bool has_a_cat = false;
    private void Start()
    {
        handler= FindObjectOfType<gamehandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    
    
        if (collision.tag == "cat")
        {
            has_a_cat = true;
            Debug.Log("cat");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "cat")
        {
            has_a_cat = false;
        }
    }
}
