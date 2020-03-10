using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager2 : MonoBehaviour
{

    public Text dText;

	public GameObject dBoxx;
    public GameObject player;

    private string dialogue;
    
    void Start()
	{
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.gameObject.GetComponent<Movement>().canMove = false;
        player.gameObject.GetComponent<Movement>().tutorial = true;
        dBoxx.SetActive(true);
        StartCoroutine(TypeSentence("Those are... colas? Suuper! By collecting those my dash will be reset without having to touch the ground."));

    }

    private void Update()
    {
        if(dBoxx.activeInHierarchy&& Input.GetButtonDown("Interact"))
        {
            dBoxx.SetActive(false);
            player.gameObject.GetComponent<Movement>().canMove = true;
            player.gameObject.GetComponent<Movement>().tutorial = false;
        }

        dText.text = dialogue;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogue = "";
        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().Play("Typing");
            dialogue += letter;
            yield return new WaitForSeconds(Random.Range(0.007f, 0.10f));
        }
    }
}
