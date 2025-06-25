using UnityEngine;
using UnityEngine.UI;

public class DamageScreenEffect : MonoBehaviour
{
    public Image damageImage; // Assign the UI Image in the Inspector
    public float fadeSpeed = 2f; // How fast the blood fades
    private float alpha = 0f;

    void Start()
    {
        // Ensure the image starts invisible
        damageImage.color = new Color(1, 0, 0, 0);
    }

    void Update()
    {
        if (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed; // Gradually fade out
            alpha = Mathf.Clamp01(alpha); // Keep between 0 and 1
            damageImage.color = new Color(1, 0, 0, alpha); // Update transparency
        }
    }

    public void ShowDamageEffect()
    {
        alpha = 0.7f; // Make the screen turn red when hit
        damageImage.color = new Color(1, 0, 0, alpha);
    }
}
