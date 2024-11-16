using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxfall : MonoBehaviour
{
    public GameObject void_tile;
    Vector2 restart;
    
    private void Start()
    {
        restart = transform.position;
        
    }
    public void respawn()
    {
        Debug.Log(transform.position);
        gameObject.SetActive(true);
        transform.position = restart;
        GameObject kip = Instantiate(void_tile,transform.position,transform.rotation);
        kip.SetActive(true);
        Debug.Log(kip);
        Destroy(gameObject);
    }
}
