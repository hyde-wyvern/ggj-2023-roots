using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPanNPinch : MonoBehaviour
{
    [SerializeField] private Camera camMain;
    private PlayerMove m_PlayerInput;

    // Start is called before the first frame update
    void Awake()
    {
        m_PlayerInput = new PlayerMove();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if(m_PlayerInput.CameraMove.MouseLeftClick.IsPressed()) 
        {
            Debug.Log("Hyde no se le ocurre el arte");
        }
    }

    #region Input Enable / Disable
    private void OnEnable()
    {
        m_PlayerInput.Enable();
    }
    private void OnDisable()
    {
        m_PlayerInput.Disable();
    }
    #endregion
}
