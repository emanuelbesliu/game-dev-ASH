using UnityEngine;

public class TutorialFall : MonoBehaviour
{
    public Rigidbody2D platform;
    public Vector2 platformPosition;
    public Collider2D detectionFall;
    public Collider2D timestop;
    private Movement move;
    private Vector2 force;

    private void Start()
    {
        platformPosition = platform.transform.position;
        move = GetComponent<Movement>();
        force.x = 0; force.y = 0;
    }

    private void Update()
    {
        if (platform.transform.position.y < -40)
            platform.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other == detectionFall)
        {
            detectionFall.gameObject.SetActive(false);
            platform.gravityScale = 3;
        }
        if (other == timestop)
        {
            timestop.gameObject.SetActive(false);
            move.checkTutorial = true;
            move.canMove = false;
            move.gameObject.GetComponent<Rigidbody2D>().velocity = force;
            move.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            
        }
    }
}
