using UnityEngine;

public class Bird : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
