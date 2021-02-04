using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Pause game
        {
            if(Time.timeScale != 0)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void ReloadLevel()
    {
        Debug.Log("Reloading level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        ResumeGame();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
