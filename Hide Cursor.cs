using UnityEngine;

public class HideCursor : MonoBehaviour
{
    private bool isCursorVisible = false; // Track cursor state

    void Start()
    {
        SetCursorState(false); // Initially hide the cursor
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isCursorVisible = !isCursorVisible; // Toggle state
            SetCursorState(isCursorVisible);
        }
    }

    private void SetCursorState(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
