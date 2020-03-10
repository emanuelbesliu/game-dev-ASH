using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D rb2;
    public Text deathCount;

    public Canvas canvas;
    public GameObject player;

    public SpriteRenderer sp;
    public SpriteRenderer sp2;

    void Start()
    {
        player.SetActive(false);
        deathCount.text = FindObjectOfType<AudioManager>().deathCount.ToString();   
    }

    void Update()
    {
        if (rb.gameObject.transform.position.x < -38)
        {
            runRight();
        }
        else if (rb.gameObject.transform.position.x > 28)
            runLeft();
        if (canvas.gameObject.activeInHierarchy)
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
    }

    void runRight()
    {
        if (canvas.gameObject.activeInHierarchy)
        {
            sp.flipX = false;
            sp2.flipX = false;
            rb2.velocity = new Vector2(10, 0);
            rb.velocity = new Vector2(10, 0);
        }
    }

    void runLeft()
    {
        if (canvas.gameObject.activeInHierarchy)
        {
            sp.flipX = true;
            sp2.flipX = true;
            rb2.velocity = new Vector2(-10, 0);
            rb.velocity = new Vector2(-10, 0);
        }
    }
}
