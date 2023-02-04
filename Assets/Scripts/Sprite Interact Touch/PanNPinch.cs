using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanNPinch : MonoBehaviour
{
    //MainCamera
    [Header("Camera")]
    [SerializeField] private Camera cam;

    //Zoom 
    [Header("Zoom")]
    [SerializeField, Tooltip("Zoom speed")] private float zoomSpeed = 0.01f;
    [SerializeField, Tooltip("Limit zoom")] private float minCamSize, maxCamSize;

    //Limits of the screen
    [Header("Limit of the screen using Sprite")]
    [SerializeField, Tooltip("Limit the screen by this sprite")] private SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    //Origin
    private Vector3 dragOrigin;

    [SerializeField] private Touch myTouch;

    private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;



    void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;

        if (cam == null)
            cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
            return;

        PanCamera();
        PinchCamera();
    }

    private void PanCamera()
    {
        if (Input.touchCount > 0)
            myTouch = Input.GetTouch(0);
        else
            return;


        switch (myTouch.phase)  
        {
            case TouchPhase.Began:
                //Save de position of touch in world space when drag starts (first time clicked)
                dragOrigin = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                break;
            case TouchPhase.Moved:
                //Drag
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.GetTouch(0).position);

                //move the camera by that distance
                cam.transform.position = ClampCamera(cam.transform.position + difference);
                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Ended:
                break;
            case TouchPhase.Canceled:
                break;
            default:
                break;
        }
    }

    private void PinchCamera()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                cam.orthographicSize += zoomModifier;

            if (touchesPrevPosDifference < touchesCurPosDifference)
                cam.orthographicSize -= zoomModifier;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minCamSize, maxCamSize);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
