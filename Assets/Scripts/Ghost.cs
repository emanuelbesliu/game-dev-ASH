using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	public float ghostDelay;
	private float ghostDelaySeconds;
	public GameObject ghost;
	public bool makeGhost = false; 
    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
    	if(makeGhost){
        	if(ghostDelaySeconds > 0){
        		ghostDelaySeconds -= Time.deltaTime;
        	}else{
        		GameObject currentObject = Instantiate(ghost, transform.position, transform.rotation); 
        		Sprite currentSprite = GetComponentInChildren<SpriteRenderer>().sprite;
        		//currentObject.transform.localScale = this.transform.localScale;
        		currentObject.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite; 
        		ghostDelaySeconds = ghostDelay;
        		Destroy(currentObject, 0.22f); 
        	}
    	}
    }
}
