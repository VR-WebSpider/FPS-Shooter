using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Flip it to face correctly
    }
}
