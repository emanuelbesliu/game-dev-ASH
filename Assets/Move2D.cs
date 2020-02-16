using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    public Vector2 jumpHeight;
    public float moveSpeed = 5f;
    public bool isGrounded = false;
    public bool hasBoost   = true;	
    // Start is called before the first frame update
    void Start()
    {
       print("Cube here");
    }

    // Update is called once per frame
    void Update()
    {
	Jump();
	Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
	transform.position += movement* Time.deltaTime * moveSpeed;
    }

    void Jump(){
    	if(Input.GetButtonDown("Boost") && isGrounded == true){
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
    	}
    }

	 /*if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))  //makes player jump
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
	*/

         //if (Input.GetKey(KeyCode.UpArrow))
         //{
         //    transform.position += Vector3.up * speed * Time.deltaTime;
         //}
         //if (Input.GetKey(KeyCode.DownArrow))
         //{
         //    transform.position += Vector3.down * speed * Time.deltaTime;
         //}	
}
