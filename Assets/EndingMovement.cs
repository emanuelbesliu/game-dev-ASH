using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D rb2;
    public Canvas canvas;
    public GameObject player;
    public SpriteRenderer sp;
    public SpriteRenderer sp2;

    void Start()
    {
        player.SetActive(false);
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
            sp.flipX=true;
            sp2.flipX = true;
            rb2.velocity = new Vector2(-10, 0);
            rb.velocity = new Vector2(-10, 0);
        }
    }

}
