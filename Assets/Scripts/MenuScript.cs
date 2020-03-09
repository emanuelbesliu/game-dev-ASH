using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public bool resume = true;
    public bool options = false;
    public bool help = false;
    public bool volume = false;
    public bool back = false;
    public bool back2 = false;
    public bool exit = false;


    public GameObject resumeButton;
    public GameObject optionsButton;
    public GameObject helpButton;
    public GameObject volumeButton;
    public GameObject backButton;
    public GameObject back2Button;
    public GameObject exitButton;

    public GameObject whiteResumeButton;
    public GameObject whiteOptionsButton;
    public GameObject whiteHelpButton;
    public GameObject whiteVolumeButton;
    public GameObject whiteBackButton;
    public GameObject whiteExitButton;

    public GameObject menu;
    public GameObject optionsMenu;
    public GameObject helpMenu;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = AudioListener.volume;
        FindObjectOfType<AudioManager>().setVolume(slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (resume)
        {
            resumeButton.SetActive(true);
            whiteResumeButton.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<AudioManager>().Play("Confirm");
                FindObjectOfType<Movement>().rb.velocity = FindObjectOfType<Movement>().oldVelocity;
                FindObjectOfType<Movement>().canMove = true;
                FindObjectOfType<Movement>().rb.gravityScale = FindObjectOfType<Movement>().oldGravity;
                this.gameObject.SetActive(false);
                FindObjectOfType<Cubes>().lavaFall = true;
                foreach (GameObject gos in GameObject.FindGameObjectsWithTag("Lava"))
                    if ((gos.name == "lava(Clone)" || gos.name == "lava2(Clone)") && gos.GetComponent<Rigidbody2D>().gravityScale == 0)
                    {
                        gos.GetComponent<Rigidbody2D>().gravityScale = FindObjectOfType<Movement>().oldLavaGravity;
                        gos.GetComponent<Rigidbody2D>().velocity = FindObjectOfType<Movement>().oldLavaVelocity;
                    }
            }
            if (Input.GetKeyDown("down"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                resume = false;
                options = true;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                resume = false;
                exit = true;
                return;
            }
        }
        else
        {
            resumeButton.SetActive(false);
            whiteResumeButton.SetActive(true);
        }

        if (options)
        {
            whiteOptionsButton.SetActive(false);
            optionsButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<AudioManager>().Play("Confirm");
                menu.SetActive(false);
                optionsMenu.SetActive(true);
                options = false;
                volume = true;
            }
            if (Input.GetKeyDown("down"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                help = true;
                options = false;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                options = false;
                resume = true;
                return;
            }
        }
        else
        {
            whiteOptionsButton.SetActive(true);
            optionsButton.SetActive(false);
        }
        if (help)
        {
            whiteHelpButton.SetActive(false);
            helpButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<AudioManager>().Play("Confirm");
                menu.SetActive(false);
                helpMenu.SetActive(true);
                help = false;
                back2 = true;
                return;
            }
            if (Input.GetKeyDown("down"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                exit = true;
                help = false;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                help = false;
                options = true;
                return;
            }
        }
        else
        {
            whiteHelpButton.SetActive(true);
            helpButton.SetActive(false);
        }
        if (exit)
        {
            whiteExitButton.SetActive(false);
            exitButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<AudioManager>().Play("Confirm");
                Debug.Log("Da merge");
                foreach (Sound s in FindObjectOfType<AudioManager>().sounds)
                {
                    s.source.Stop();

                }
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown("down"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                exit = false;
                resume = true;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                exit = false;
                help = true;
                return;
            }
        }
        else
        {
            whiteExitButton.SetActive(true);
            exitButton.SetActive(false);
        }
        if (volume)
        {
            slider.value = AudioListener.volume;
            whiteVolumeButton.SetActive(false);
            volumeButton.SetActive(true);
            if (Input.GetKeyDown("left"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                slider.GetComponent<Slider>().value -= 0.05f;
                FindObjectOfType<AudioManager>().setVolume(slider.value);
                return;
            }
            if (Input.GetKeyDown("right"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                slider.GetComponent<Slider>().value += 0.05f;
                FindObjectOfType<AudioManager>().setVolume(slider.value);
                return;
            }
            if (Input.GetKeyDown("down"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                back = true;
                volume = false;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                back = true;
                volume = false;
                return;
            }
        }
        else
        {
            whiteVolumeButton.SetActive(true);
            volumeButton.SetActive(false);
        }
        if (back)
        {
            whiteBackButton.SetActive(false);
            backButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<AudioManager>().Play("Back");
                menu.SetActive(true);
                optionsMenu.SetActive(false);
                back = false;
                options = true;
            }
            if (Input.GetKeyDown("down"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                back = false;
                volume = true;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                FindObjectOfType<AudioManager>().Play("Cursor");
                back = false;
                volume = true;
                return;
            }
        }
        else
        {
            whiteBackButton.SetActive(true);
            backButton.SetActive(false);
        }
        if (back2)
        {
            back2Button.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<AudioManager>().Play("Back");
                menu.SetActive(true);
                helpMenu.SetActive(false);
                back2 = false;
                help = true;
            }
        }
        else
        {
            back2Button.SetActive(false);
        }
    }
}
