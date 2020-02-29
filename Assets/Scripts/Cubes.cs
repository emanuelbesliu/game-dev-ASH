using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    public bool lavaFall = false;
	public float delay = 2f;
	public float destroyCube = -15;
    bool random;
    public GameObject cube;
    public GameObject cube2;
    public GameObject bird;
    public float gravity = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", delay, delay);
    }

    // Update is called once per frame
    void Update()
    {
        //cube2.transform.position = Vector3.MoveTowards(cube2.transform.position, bird.transform.position, .03f);
        foreach (GameObject gos in GameObject.FindGameObjectsWithTag("Lava")){
    		if((gos.name == "lava(Clone)" || gos.name == "lava2(Clone)") && gos.transform.position.y > -300 && gos.GetComponent<Rigidbody2D>().gravityScale == 0){
    			gos.GetComponent<Rigidbody2D>().gravityScale = gravity;
    		}
      		if((gos.name == "lava(Clone)" || gos.name == "lava2(Clone)") && gos.transform.position.y < destroyCube){
      			//Debug.Log(gos.transform.position.y);
    			Destroy(gos);
      		}

            if(bird.activeInHierarchy && gos.name == "lava2(Clone)"){
                Vector3 direction = bird.transform.position - gos.transform.position;
                gos.GetComponent<Rigidbody2D>().AddForce(0.09f * direction);
            }
   		}
    }

    void Spawn(){
        random = (Random.value > 0.5f);
        if (lavaFall)
            if(random)
    	        Instantiate(cube, new Vector3(Random.Range(-9.52f, 41.05f), 30, 0), Quaternion.identity);
            else
                Instantiate(cube2, new Vector3(Random.Range(-9.52f, 41.05f), 30, 0), Quaternion.identity);
    }
    public void LavaTutorial()
    {      
        Instantiate(cube2, new Vector3(71f, 30, 0), Quaternion.identity);
    }
}
