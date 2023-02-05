using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCode : MonoBehaviour
{
    private UiManager uiManager;

    [SerializeField] private string answerCode = ""; 

    // Start is called before the first frame update
    void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
    }

    public void NumberCode(int number)
    {
        uiManager.answerCodeTxt.text += number.ToString();
    }

    public void CheckAnswer()
    {
        if(uiManager.answerCodeTxt.text == answerCode)
        {
            Debug.Log("El codigo es el mismo");
            //Se agrega cualquier elemento
        }
        else
        {
            Debug.Log("El codigo no es el mismo");
            uiManager.answerCodeTxt.text = null;
        }
    }
}
