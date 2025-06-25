using UnityEngine;

public class ExitConfirmation : MonoBehaviour
{
    public GameObject exitPanel; // Drag the ExitPanel UI here in the Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleExitPanel();
        }
    }

    public void ToggleExitPanel()
    {
        bool isActive = exitPanel.activeSelf;
        exitPanel.SetActive(!isActive); // Toggle the panel on/off

        // Pause the game when the panel is open
        Time.timeScale = isActive ? 1 : 0; // 1 = normal speed, 0 = paused
    }

    public void ConfirmExit()
    {
        Debug.Log("Exiting Game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity Editor
#else
            Application.Quit(); // Quits the game in a build
#endif
    }

    public void CancelExit()
    {
        exitPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }
}
