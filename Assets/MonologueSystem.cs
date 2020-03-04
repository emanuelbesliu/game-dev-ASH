using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueSystem : MonoBehaviour
{
    private DialogueManager dM;
    public Queue<string> queue;
    public string[] sentences;
    public string[] sentences12;
    public string[] sentences2;
    public string[] sentences3;
    public string[] sentences4;
    public bool camera = false;
    public bool camera12 = false;
    public bool camera2 = false;
    public bool camera3 = false;
    public bool camera4 = false;
    public GameObject popUp1;
    public GameObject popUp12;
    public GameObject popUp2;
    public GameObject popUp3;
    public GameObject popUp4;
    public GameObject popUp2Off;
    public float step;
    private string dialogue;
    private int count = 0;


    void Start()
    {
        queue = new Queue<string>();
        dM = FindObjectOfType<DialogueManager>();
        resetQueue(sentences);
    }

    // Update is called once per frame
    void Update()
    {
        if (dM.dialogActive && Input.GetButtonDown("Interact"))
            DisplayNextSentence(dM.dBox,popUp1,dM.dialogActive);
        else if (dM.dialogActive12 && Input.GetButtonDown("Interact"))
            DisplayNextSentence(dM.dBox12, popUp12, dM.dialogActive12);
        else if (dM.dialogActive2 && Input.GetButtonDown("Interact"))
            DisplayNextSentence(dM.dBox2, popUp2, dM.dialogActive2);
        else if (dM.dialogActive3 && Input.GetButtonDown("Interact"))
            DisplayNextSentence(dM.dBox3, popUp3, dM.dialogActive3);
        else if (dM.dialogActive4 && Input.GetButtonDown("Interact"))
            DisplayNextSentence(dM.dBox4, popUp4, dM.dialogActive4);
        if (step == 1)
            dM.dialogueText.text = dialogue;
        else if (step == 1.5f)
            dM.dialogueText12.text = dialogue;
        else if (step == 2)
            dM.dialogueText2.text = dialogue;
        else if (step == 3)
            dM.dialogueText3.text = dialogue;
        else if (step == 4)
            dM.dialogueText4.text = dialogue;
    }

    public void resetQueue(string[] sentences)
    {
        queue.Clear();
        foreach (string sentence in sentences)
        {
            queue.Enqueue(sentence);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name=="FirstPopUp")
        {
            gameObject.GetComponent<Movement>().anim.SetHorizontalMovement(0, 0, 0);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Movement>().canMove = false;
            resetQueue(sentences);
            step = 1;
            camera = true;
            dM.dBox.SetActive(true);
            dM.dialogActive = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(queue.Dequeue()));
        }
        if (other.gameObject.name== "FirstSecondPopUp")
        {
            count++;
            if (count == 3)
            {
                //gameObject.GetComponent<Movement>().anim.SetHorizontalMovement(0, 0, 0);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                gameObject.GetComponent<Movement>().canMove = false;
                resetQueue(sentences12);
                step = 1.5f;
                camera12 = true;
                dM.dBox12.SetActive(true);
                dM.dialogActive12 = true;
                StopAllCoroutines();
                StartCoroutine(TypeSentence(queue.Dequeue()));
            }
        }

        if (other.gameObject.name == "SecondPopUp")
        {
            resetQueue(sentences2);
            step = 2;
            camera2 = true;
            dM.dBox2.SetActive(true);
            dM.dialogActive2 = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(queue.Dequeue()));
            popUp2Off.SetActive(true);
        }

        if (other.gameObject.name == "ThirdPopUp")
        {
            resetQueue(sentences3);
            step = 3;
            camera3 = true;
            dM.dBox3.SetActive(true);
            dM.dialogActive3 = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(queue.Dequeue()));
        }

        if (other.gameObject.name == "FourthPopUp")
        {
            gameObject.GetComponent<Movement>().room2 = false;
            resetQueue(sentences4);
            step = 4;
            camera4 = true;
            dM.dBox4.SetActive(true);
            dM.dialogActive4 = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(queue.Dequeue()));
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "FirstPopUp")
        {
            dM.dBox.SetActive(false);
            dM.dialogActive = false;
            resetQueue(sentences);
        }
        if (other.gameObject.name == "FirstSecondPopUp")
        {
            dM.dBox12.SetActive(false);
            dM.dialogActive12 = false;
            resetQueue(sentences12);
        }
        if (other.gameObject.name == "SecondPopUp")
        {
            dM.dBox2.SetActive(false);
            dM.dialogActive2 = false;
            resetQueue(sentences2);
        }
        if (other.gameObject.name == "ThirdPopUp")
        {
            dM.dBox3.SetActive(false);
            dM.dialogActive3 = false;
            resetQueue(sentences3);
        }
        if (other.gameObject.name == "FourthPopUp")
        {
            dM.dBox4.SetActive(false);
            dM.dialogActive4 = false;
            resetQueue(sentences4);
        }
    }
    public void DisplayNextSentence(GameObject d, GameObject p,bool dA)
    {
        if(queue.Count == 0)
        {
            d.SetActive(false);
            dA = false;
            p.SetActive(false);
            gameObject.GetComponent<Movement>().canMove = true;
            camera = false;
            camera12 = false;
            camera2 = false;
            camera3 = false;
            camera4 = false;
            return;
        }
        string sentence = queue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogue="";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogue += letter;
            yield return null;
        }
    }
}
