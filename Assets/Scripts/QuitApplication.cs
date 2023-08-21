using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if (IsPressingESC())
        {
            Debug.Log("Game quited");
            Application.Quit();            
        }
    }

    bool IsPressingESC() {return Input.GetKey(KeyCode.Escape);}
}
