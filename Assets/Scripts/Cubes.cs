using UnityEngine;
using UnityEngine.SceneManagement;

public class Cubes : MonoBehaviour
{
    public bool lavaFall = false;
    bool random;

    public float delay = 2f;
    public float gravity = 1;
    public float destroyCube = -15;

    public GameObject cube;
    public GameObject cube2;
    public GameObject bird;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            gravity = 1;
            InvokeRepeating("Spawn", delay, delay);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            gravity = 0.7f;
            InvokeRepeating("Spawn", delay, delay);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.gameObject.activeInHierarchy)
            return;

        foreach (GameObject gos in GameObject.FindGameObjectsWithTag("Lava"))
        {
            if ((gos.name == "lava(Clone)" || gos.name == "lava2(Clone)") && gos.transform.position.y > -300 && gos.GetComponent<Rigidbody2D>().gravityScale == 0)
            {
                gos.GetComponent<Rigidbody2D>().gravityScale = gravity;
            }
            if ((gos.name == "lava(Clone)" || gos.name == "lava2(Clone)") && gos.transform.position.y < destroyCube)
            {
                Destroy(gos);
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
                if (bird.activeInHierarchy && gos.name == "lava2(Clone)")
                {
                    Vector3 direction = bird.transform.position - gos.transform.position;
                    gos.GetComponent<Rigidbody2D>().AddForce(0.09f * direction);

                }
        }
    }

    void Spawn()
    {
        random = (Random.value > 0.5f);
        if (lavaFall && SceneManager.GetActiveScene().buildIndex == 2)
            if (random)
                Instantiate(cube, new Vector3(Random.Range(-60f, 61f), 123, 0), Quaternion.identity);
            else
                Instantiate(cube2, new Vector3(Random.Range(-60f, 61f), 123, 0), Quaternion.identity);
        else if (lavaFall && SceneManager.GetActiveScene().buildIndex == 1)
            if (random)
                Instantiate(cube, new Vector3(Random.Range(44f, 109f), 27, 0), Quaternion.identity);
            else
                Instantiate(cube2, new Vector3(Random.Range(44f, 109f), 27, 0), Quaternion.identity);
    }

    public void LavaTutorial()
    {
        Instantiate(cube2, new Vector3(71f, 30, 0), Quaternion.identity);
    }
}

