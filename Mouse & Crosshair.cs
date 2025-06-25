using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public RectTransform crosshairUI; // Assign the crosshair UI in the Inspector

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    void Update()
    {
        // Update crosshair position to match mouse cursor
        crosshairUI.position = Input.mousePosition;
    }
}
