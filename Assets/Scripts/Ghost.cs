using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject ghost;

    public float ghostDelay;
	private float ghostDelaySeconds;
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

        		currentObject.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite; 
        		ghostDelaySeconds = ghostDelay;
        		Destroy(currentObject, 0.22f); 
        	}
    	}
    }
}
