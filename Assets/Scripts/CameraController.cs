using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSteps = 10f; 
    private float currentStep;
    public Camera cameratarget;
    public GameObject player;
    public GameObject wall;

    void LateUpdate()
    {
        if(player.transform.position.x>-11)
        {
            currentStep += Time.deltaTime;
            Vector3 oldposition = this.transform.position;
            this.transform.position = Vector3.Lerp(oldposition, cameratarget.transform.position, currentStep / panSteps);
            wall.SetActive(true);
        }
    }
}
