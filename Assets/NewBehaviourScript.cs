using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       print("Cube here");
    }

    // Update is called once per frame
    void Update()
    {
        print("Cubify");
	transform.Translate(1f*Time.deltaTime, 0f, 0f);
    }
}
