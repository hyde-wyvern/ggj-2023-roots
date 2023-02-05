using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class SpriteInteract : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField, Tooltip("Get the touch position")] private Camera cam;
    [SerializeField, Tooltip("Set the minimun distance of the camera to interact")] private float minDistance; 

    [Header("Object Event")]
    [SerializeField, Tooltip("Make sure to use a public void")] private UnityEvent interactAction;

    // Start is called before the first frame update
    void Awake()
    {
        if(cam == null) 
        {
            cam = FindObjectOfType<Camera>();
        }
    }

    public void OnMouseDown()
    {
        if (cam.orthographicSize <= minDistance)
        { 
            Debug.Log("hit");
            interactAction.Invoke();
        }
    }
}
