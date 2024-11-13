using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxfall : MonoBehaviour
{
    public GameObject void_tile;
    public void respawn()
    {
        Debug.Log(transform.position);
        Instantiate(void_tile,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
