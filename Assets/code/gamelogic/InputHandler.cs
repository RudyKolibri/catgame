using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{  
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    public void onclick(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        if (rayHit.collider.gameObject.tag == "cat")
        {
            rayHit.collider.GetComponent<cat_walk>().new_path();
        }
    }
}
