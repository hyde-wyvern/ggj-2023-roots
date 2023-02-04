using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteInteract : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField, Tooltip("Get the touch position")] private Camera cam;
    [SerializeField, Tooltip("Set the minimun distance of the camera to interact")] private float minDistance; 

    [Header("Object Event")]
    [SerializeField, Tooltip("Make sure to use a public void")] private UnityEvent interactAction;

    //Not in inspector
    private Collider2D _collider;

    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForTouch();
    }

    bool CheckForTouch()
    {
        if (cam.orthographicSize <= minDistance)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                var wp = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                var touchPosition = new Vector2(wp.x, wp.y);

                if (_collider == Physics2D.OverlapPoint(touchPosition))
                    interactAction.Invoke();
            }
        }

        return false;
    }
}
