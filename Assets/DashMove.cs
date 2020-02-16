using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{ 
    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
	    dashTime = startDashTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			direction = 1;
		}else if(Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.RightArrow)){
			direction = 2;
		}else if(Input.GetKeyDown(KeyCode.RightArrow)){
			direction = 3;
		}else if(Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.DownArrow)){
			direction = 4;
		}else if(Input.GetKeyDown(KeyCode.DownArrow)){
			direction = 5;
		}else if(Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftArrow)){
			direction = 6;
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			direction = 7;
		}else if(Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.UpArrow)){
			direction = 8;
		}
	}else{
		if(dashTime <= 0){
			direction = 0;
			dashTime = startDashTime;
			rb.velocity = Vector2.zero;
		} else{
			dashTime -= Time.deltaTime;

			if(direction == 1){
				rb.velocity = Vector2(0,1) * dashSpeed;
			}else if(direction == 2){
				rb.velocity = Vector2(1,1) * dashSpeed;
			}else if(direction == 3){
				rb.velocity = Vector2(1,0) * dashSpeed;
			}else if(direction == 4){
				rb.velocity = Vector2(1,-1) * dashSpeed;
			}else if(direction == 5){
				rb.velocity = Vector2(0,-1) * dashSpeed;
			}else if(direction == 6){
				rb.velocity = Vector2(-1,-1) * dashSpeed;
			}else if(direction == 7){
				rb.velocity = Vector2(-1,0) * dashSpeed;
			}else if(direction == 8){
				rb.velocity = Vector(-1,1) * dashSpeed;
			}
		}
	}
    }
}
