using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector2 jumpHeight;
    // Start is called before the first frame update
    void Start()
    {
       print("Cube here");
    }

    // Update is called once per frame
    void Update()
    {
	 if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))  //makes player jump
    	 {
        	GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
	 }
         print("Cubify");
	 //transform.Translate(1f*Time.deltaTime, 0f, 0f);
    	 if (Input.GetKey(KeyCode.LeftArrow))
         {
             transform.position += Vector3.left * speed * Time.deltaTime;
         }
         if (Input.GetKey(KeyCode.RightArrow))
         {
             transform.position += Vector3.right * speed * Time.deltaTime;
         }
         if (Input.GetKey(KeyCode.UpArrow))
         {
             transform.position += Vector3.up * speed * Time.deltaTime;
         }
         if (Input.GetKey(KeyCode.DownArrow))
         {
             transform.position += Vector3.down * speed * Time.deltaTime;
         }	
   }
}
