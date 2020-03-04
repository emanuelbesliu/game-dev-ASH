using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text dialogueText;
	public Text dialogueText12;
	public Text dialogueText2;
	public Text dialogueText3;
	public Text dialogueText4;
	public GameObject dBox;
	public GameObject dBox12;
	public GameObject dBox2;
	public GameObject dBox3;
	public GameObject dBox4;

	public bool dialogActive;
	public bool dialogActive12;
	public bool dialogActive2;
	public bool dialogActive3;
	public bool dialogActive4;

	void Start()
	{
		dBox.SetActive(false);
		dBox12.SetActive(false);
		dBox2.SetActive(false);
		dBox3.SetActive(false);
		dBox4.SetActive(false);

	}

	void Update(){
	}
}
