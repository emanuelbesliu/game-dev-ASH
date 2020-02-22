using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
	public float delay = 0.1f;
	public float destroyCube = -70;
	public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", delay, delay);
    }

    // Update is called once per frame
    void Update()
    {

    	foreach(GameObject gos in GameObject.FindGameObjectsWithTag("Lava")){
    		if(gos.name == "Lav(Clone)" && gos.transform.position.y > -300 && gos.GetComponent<Rigidbody2D>().gravityScale == 0){
    			gos.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
    		}

      		if(gos.name == "Lav(Clone)" && gos.transform.position.y < destroyCube){
      			//Debug.Log(gos.transform.position.y);
    			Destroy(gos);
      		}
   		}
    }

    void Spawn(){
    	Instantiate(cube, new Vector3(Random.Range(-45, 51), 30, 0), Quaternion.identity);
    }
}
