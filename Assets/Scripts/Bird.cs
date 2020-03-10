using UnityEngine;

public class Bird : MonoBehaviour
{
    public Cubes cubes;
    public Movement move;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            this.gameObject.SetActive(false);
            move.canMove = true;
            cubes.lavaFall = true;
        }
    }
}
