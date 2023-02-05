using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPanNPinch : MonoBehaviour
{
    [SerializeField] private Camera camMain;
    [SerializeField] private bool leftClickHold = false;
    private Vector3 dragOrigin;
    [SerializeField] private float zoomStep, minCamSize, maxCamSize;
    [Space]
    [Header("Limit of the screen using Sprite")]
    [SerializeField, Tooltip("Limit the screen by this sprite")] private SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    public bool canMove = true;

    // Start is called before the first frame update
    void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove)
        {
            PanCamera();
            Zoom();
        }        
    }

    private void PanCamera()
    {
        if (!Mouse.current.leftButton.isPressed) leftClickHold = false;
        
        if (Mouse.current.leftButton.isPressed && !leftClickHold)
        {
            dragOrigin = camMain.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            leftClickHold = true;
        }

        if (Mouse.current.leftButton.isPressed && leftClickHold)
        {
            Vector3 difference = dragOrigin - camMain.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            //move the camera by that distance
            camMain.transform.position = ClampCamera(camMain.transform.position + difference);
        }

    }

    private void Zoom()
    {
        zoomStep = Mouse.current.scroll.ReadValue().normalized.y;
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
