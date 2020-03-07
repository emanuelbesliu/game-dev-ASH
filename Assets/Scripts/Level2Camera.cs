using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Camera : MonoBehaviour
{

    private float currentStep;
    public float panSteps = 0.5f;
    private Vector3 oldposition;
    private float oldfieldofview;
    public Camera camera2;
    public Camera camera;
    public GameObject wall;
    bool trigger = false;
    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Level1Music");
        FindObjectOfType<AudioManager>().Play("Level2Music");
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
    }
}
