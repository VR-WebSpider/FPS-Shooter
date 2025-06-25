using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 5f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private TMP_Text promptText;
    private PlayerUI playerUI;
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        if (inputManager == null) Debug.LogError("InputManager component is missing!", this);

        playerUI = GetComponent<PlayerUI>();
        if (playerUI == null) Debug.LogError("PlayerUI is not assigned!", this);

        PlayerLook playerLook = GetComponent<PlayerLook>();
        if (playerLook != null)
            cam = playerLook.Cam;
        else
            Debug.LogError("PlayerLook component is missing!", this);
    }

    void Update()
    {
        if (playerUI == null || cam == null || inputManager == null) return; // Prevents null reference issues

        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
