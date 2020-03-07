using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f;
    public GameObject player;
    public GameObject mainMenu;
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
            if (player.transform.position.y >= 19.5 && player.transform.position.x>=95)
                LoadNextLevel();
        if (SceneManager.GetActiveScene().buildIndex==0 && mainMenu.GetComponent<MainMenu>().play && Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<AudioManager>().Play("Confirm");
            LoadNextLevel();
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
