using UnityEngine;

public class MouseController : MonoBehaviour
{
    public void MouseConfined() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void MouseNoneTrue() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MouseLockedFalse() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
