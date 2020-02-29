using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSteps = 10f; 
    private float currentStep;
    private bool lavaFirstTutorial = true;
    public Camera cameratarget;
    public GameObject player;
    public GameObject wall;
    public Rigidbody2D bird;

    void FixedUpdate()
    {
        if(player.transform.position.x>-11)
        {
            currentStep += Time.deltaTime;
            Vector3 oldposition = this.transform.position;
            this.transform.position = Vector3.Lerp(oldposition, cameratarget.transform.position, currentStep / panSteps);
            wall.SetActive(true);
            if(bird.gameObject.activeInHierarchy)
            {
                player.GetComponent<Movement>().canMove = false;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
                if(player.GetComponent<Collision>().onGround)
                {
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
                bird.velocity = new Vector2(bird.velocity.x + 0.07f, bird.velocity.y + 0.07f);
            }
            if (lavaFirstTutorial)
            {
                player.GetComponent<Movement>().playerPos = player.transform.position;
                GetComponentInParent<Cubes>().LavaTutorial();
                lavaFirstTutorial = false;
            }
            //GetComponentInParent<Cubes>().lavaFall = true;
        }
    }
}
