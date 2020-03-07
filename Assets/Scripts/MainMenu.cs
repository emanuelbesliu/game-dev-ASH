using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool play = true;
    public bool options = false;
    public bool exit = false;
    public bool volume = false;
    public bool back = false;

    public AudioManager aM;
    public GameObject playArrow;
    public GameObject optionsArrow;
    public GameObject exitArrow;
    public GameObject volumeArrow;
    public GameObject backArrow;

    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject exitButton;
    public GameObject volumeButton;
    public GameObject backButton;

    public GameObject whitePlayButton;
    public GameObject whiteOptionsButton;
    public GameObject whiteExitButton;
    public GameObject whiteVolumeButton;
    public GameObject whiteBackButton;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject slider;

    void Start()
    {
        aM.Play("MainTheme");
    }

    void Update()
    {
        if (play) {
            playArrow.SetActive(true);
            playButton.SetActive(true);
            whitePlayButton.SetActive(false);

            if (Input.GetKeyDown("down"))
            {
                aM.Play("Cursor");
                play = false;
                options = true;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                aM.Play("Cursor");
                play = false;
                exit = true;
                return;
            }
        }
        else
        {
            playArrow.SetActive(false);
            playButton.SetActive(false);
            whitePlayButton.SetActive(true);
        }

        if (options)
        {
            optionsArrow.SetActive(true);
            whiteOptionsButton.SetActive(false);
            optionsButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                aM.Play("Confirm");
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
                options = false;
                volume = true;
            }
            if (Input.GetKeyDown("down"))
            {
                aM.Play("Cursor");
                exit = true;
                options = false;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                aM.Play("Cursor");
                options = false;
                play = true;
                return;
            }
        }
        else
        {
            optionsArrow.SetActive(false);
            whiteOptionsButton.SetActive(true);
            optionsButton.SetActive(false);
        }
        if (exit)
        {
            exitArrow.SetActive(true);
            whiteExitButton.SetActive(false);
            exitButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                aM.Play("Confirm");
                Debug.Log("Da merge");
                Application.Quit();
            }
            if (Input.GetKeyDown("down"))
            {
                aM.Play("Cursor");
                exit = false;
                play = true;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                aM.Play("Cursor");
                exit = false;
                options = true;
                return;
            }
        }
        else
        {
            exitArrow.SetActive(false);
            whiteExitButton.SetActive(true);
            exitButton.SetActive(false);
        }
        if (volume)
        {
            volumeArrow.SetActive(true);
            whiteVolumeButton.SetActive(false);
            volumeButton.SetActive(true);
            if(Input.GetKeyDown("left"))
                {
                aM.Play("Cursor");
                slider.GetComponent<Slider>().value -= 0.05f;
                return;
            }
            if (Input.GetKeyDown("right"))
            {
                aM.Play("Cursor");
                slider.GetComponent<Slider>().value+= 0.05f;
                return;
            }
            if (Input.GetKeyDown("down"))
            {
                aM.Play("Cursor");
                back = true;
                volume = false;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                aM.Play("Cursor");
                back = true;
                volume = false;
                return;
            }
        }
        else
        {
            volumeArrow.SetActive(false);
            whiteVolumeButton.SetActive(true);
            volumeButton.SetActive(false);
        }
        if (back)
        {
            backArrow.SetActive(true);
            whiteBackButton.SetActive(false);
            backButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                aM.Play("Back");
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);
                back = false;
                options = true;
            }
            if (Input.GetKeyDown("down"))
            {
                aM.Play("Cursor");
                back = false;
                volume = true;
                return;
            }
            if (Input.GetKeyDown("up"))
            {
                aM.Play("Cursor");
                back = false;
                volume = true;
                return;
            }
        }
        else
        {
            backArrow.SetActive(false);
            whiteBackButton.SetActive(true);
            backButton.SetActive(false);
        }



    }

}
