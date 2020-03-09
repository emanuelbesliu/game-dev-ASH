using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Camera : MonoBehaviour
{

    private float currentStep;
    private float currentStep2;
    public float panSteps = 0.5f;
    private Vector3 oldposition;
    private float oldfieldofview;
    public Camera camera3;
    public Camera camera2;
    public Camera camera;
    public GameObject wall;
    bool trigger = false;
    bool trigger2 = false;
    public GameObject coll;
    public GameObject fill;
    public Canvas finalCanvas;
    public Canvas firstDialogue;
    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Level1Music");
        FindObjectOfType<AudioManager>().Play("Level2Music");
        firstDialogue.gameObject.SetActive(true);
        oldposition = camera.transform.position;
        oldfieldofview = camera.GetComponent<Camera>().fieldOfView;
    }
    private void FixedUpdate()
    {
        if (trigger == true)
        {
            currentStep += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(oldposition, camera2.transform.position, currentStep / panSteps);
            camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(oldfieldofview, camera2.fieldOfView, currentStep / panSteps);
        }
        if (trigger2 == true)
        {
            currentStep2 += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(camera2.transform.position, camera3.transform.position, currentStep2 / panSteps);
            camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera2.fieldOfView, camera3.fieldOfView, currentStep2 / panSteps);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "ChangeCamera")
        {
            trigger = true;
            this.gameObject.GetComponent<Movement>().playerPos = this.gameObject.transform.position;
            wall.gameObject.SetActive(true);
            collision.gameObject.SetActive(true);
        }
        if (collision.name == "ChangeCamera2")
        {
            FindObjectOfType<Movement>().isDashing = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            trigger2 = true;
            FindObjectOfType<Movement>().canMove = false;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            FindObjectOfType<Movement>().start = false;
            this.gameObject.transform.position = new Vector3(-26, 80, 0);
            coll.SetActive(false);
            StartCoroutine(AutomaticJump());
        }
        if (collision.name == "ChangeCamera3")
        {
            finalCanvas.gameObject.SetActive(true);
        }
    }
    IEnumerator AutomaticJump()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.gameObject.transform.position = new Vector3(-26, 80, 0);
        FindObjectOfType<Movement>().Jump(new Vector2(0, 2f));
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        FindObjectOfType<Movement>().anim.Flip(!FindObjectOfType<Movement>().side);
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<Movement>().canMove = true;
        FindObjectOfType<Movement>().start = true;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        FindObjectOfType<Movement>().playerPos = new Vector3(-30, 85, 0) ;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<Movement>().grabTime = 3;
        fill.SetActive(true);
        firstDialogue.gameObject.SetActive(true);

    }
}
