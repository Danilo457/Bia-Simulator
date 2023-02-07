using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public void MouseCenterCeletor() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void MouseActiv() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MouseDesativ() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
