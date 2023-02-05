using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPanNPinch : MonoBehaviour
{
    [SerializeField] private Camera camMain;
    private Vector3 dragOrigin;
    [SerializeField] private float zoomStep, minCamSize, maxCamSize;
    [Space]
    [Header("Limit of the screen using Sprite")]
    [SerializeField, Tooltip("Limit the screen by this sprite")] private SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    // Start is called before the first frame update
    void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
        //m_PlayerInput.CameraMove.MouseScroll.performed += ctx => Zoom();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PanCamera();
        Zoom();
    }

    private void PanCamera()
    {
        //if(m_PlayerInput.CameraMove.MouseLeftClick.IsPressed()) 
        if(Input.GetMouseButtonDown(0)) 
        {
            dragOrigin = camMain.ScreenToWorldPoint(Input.mousePosition);            
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - camMain.ScreenToWorldPoint(Input.mousePosition);

            //move the camera by that distance
            camMain.transform.position = ClampCamera(camMain.transform.position + difference);
            //camMain.orthographicSize = Mathf.Clamp(camMain.orthographicSize, minCamSize, maxCamSize);
            //camMain.transform.position = ClampCamera(camMain.transform.position + difference);
            //camMain.transform.position += difference;
        }
        
    }

    private void Zoom()
    {
        zoomStep = Input.GetAxis("Mouse ScrollWheel");
        float newSize = camMain.orthographicSize - zoomStep;

        camMain.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = camMain.orthographicSize;
        float camWidth = camMain.orthographicSize * camMain.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
