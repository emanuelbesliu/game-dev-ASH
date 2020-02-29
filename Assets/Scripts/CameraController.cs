using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSteps = 0.5f; 
    private float currentStep;
    private float currentStep2;
    private float currentStep3;
    private float currentStep4;
    private float currentStep5;
    private float currentStep6;
    private float currentStep7;
    private float currentStep8;
    private float currentStep9;
    private bool lavaFirstTutorial = true;
    public Camera cameratarget;
    public Camera camera2;
    public Camera cameratutorial;
    public Camera cameratutorial2;
    public Camera cameratutorial3;
    public Camera cameratutorial4;
    public GameObject player;
    public GameObject wall;
    public GameObject wall2;
    public Rigidbody2D bird;
    private Vector3 oldposition;
    private float oldfieldofview;

    private void Start()
    {
        oldposition = this.transform.position;
        oldfieldofview = this.GetComponent<Camera>().fieldOfView;
    }
    void FixedUpdate()
    {
        if (player.GetComponent<Movement>().room2)
        {
            player.GetComponent<MonologueSystem>().step = 0;
            Debug.Log("Pulapula");
            currentStep9 += Time.deltaTime;
            this.transform.position = Vector3.Lerp(oldposition, camera2.transform.position, currentStep9 / panSteps);
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(oldfieldofview, camera2.fieldOfView, currentStep9 / panSteps);
            wall.SetActive(true);
        }
        if (player.GetComponent<MonologueSystem>().camera && player.GetComponent<MonologueSystem>().step == 1)
        {
            currentStep2 = 0;
            currentStep += Time.deltaTime;
            this.transform.position = Vector3.Lerp(oldposition, cameratutorial.transform.position, currentStep / panSteps);
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(oldfieldofview, cameratutorial.fieldOfView, currentStep / panSteps);
        }
        else if(!player.GetComponent<MonologueSystem>().camera && player.GetComponent<MonologueSystem>().step == 1)
        {
            currentStep = 0;
            currentStep2 += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, oldposition, currentStep2 / (panSteps+2f));
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(this.GetComponent<Camera>().fieldOfView, oldfieldofview, currentStep2 / (panSteps+2f));
        }
        else if (player.GetComponent<MonologueSystem>().camera2 && player.GetComponent<MonologueSystem>().step == 2)
        {
            currentStep4 = 0;
            currentStep3 += Time.deltaTime;
            this.transform.position = Vector3.Lerp(oldposition, cameratutorial2.transform.position, currentStep3 / panSteps);
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(oldfieldofview, cameratutorial2.fieldOfView, currentStep3 / panSteps);
        }
        else if(!player.GetComponent<MonologueSystem>().camera2 && player.GetComponent<MonologueSystem>().step == 2)
        {
            currentStep3 = 0;
            currentStep4 += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, oldposition, currentStep4 / (panSteps + 2f));
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(this.GetComponent<Camera>().fieldOfView, oldfieldofview, currentStep4 / (panSteps + 2f));
        }
        else if (player.GetComponent<MonologueSystem>().camera3 && player.GetComponent<MonologueSystem>().step == 3)
        {
            currentStep6 = 0;
            currentStep5 += Time.deltaTime;
            this.transform.position = Vector3.Lerp(oldposition, cameratutorial3.transform.position, currentStep5 / panSteps);
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(oldfieldofview, cameratutorial3.fieldOfView, currentStep5 / panSteps);
        }
        else if (!player.GetComponent<MonologueSystem>().camera3 && player.GetComponent<MonologueSystem>().step == 3)
        {
            currentStep5 = 0;
            currentStep6 += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, oldposition, currentStep6 / (panSteps + 2f));
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(this.GetComponent<Camera>().fieldOfView, oldfieldofview, currentStep6 / (panSteps + 2f));
        }
        else if (player.GetComponent<MonologueSystem>().camera4 && player.GetComponent<MonologueSystem>().step == 4)
        {
            currentStep8 = 0;
            currentStep7 += Time.deltaTime;
            this.transform.position = Vector3.Lerp(oldposition, cameratutorial4.transform.position, currentStep7 / panSteps);
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(oldfieldofview, cameratutorial4.fieldOfView, currentStep7 / panSteps);
            wall2.SetActive(true);
            if (bird.gameObject.activeInHierarchy)
            {
                player.GetComponent<Movement>().canMove = false;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
                if (player.GetComponent<Collision>().onGround)
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
        }
        else if (!player.GetComponent<MonologueSystem>().camera4 && player.GetComponent<MonologueSystem>().step == 4)
        {
            currentStep7 = 0;
            currentStep8 += Time.deltaTime;
            oldposition = cameratarget.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, oldposition, currentStep8 / (panSteps + 2f));
            this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(this.GetComponent<Camera>().fieldOfView, oldfieldofview, currentStep8 / (panSteps + 2f));
        }
        //if (player.transform.position.x>-10.5)
        //{
            //currentStep += Time.deltaTime;
            //currentStep2 += Time.deltaTime;
            //oldposition = this.transform.position;
            //this.transform.position = Vector3.Lerp(oldposition, cameratarget.transform.position, currentStep / panSteps);
            //this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(this.GetComponent<Camera>().fieldOfView, oldfieldofview, currentStep2 / (panSteps + 2f));
            
            
            //GetComponentInParent<Cubes>().lavaFall = true;
        //}
        
    }
}
