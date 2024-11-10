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
        transform.position = restart;
    }
    
}
