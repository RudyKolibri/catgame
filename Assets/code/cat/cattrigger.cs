using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cattrigger : MonoBehaviour
{
    cat_walk cat;
    private void Start()
    {
        cat = GetComponentInParent<cat_walk>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        cat.catstay(collision);
    }
}
