using UnityEngine;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) // Reload level
        {
            ReloadLevel();
        }
        if(Input.GetKeyDown(KeyCode.Escape)) // Exit game
        {
            Application.Quit();
        }
    }
    void ReloadLevel()
    {
        Debug.Log("Reloading level...");
        Application.LoadLevel(Application.loadedLevel);
    }
}
