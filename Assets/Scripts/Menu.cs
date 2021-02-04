using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }
    void Update()
    {

    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void VeryLow()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void Low()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void Medium()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void High()
    {
        QualitySettings.SetQualityLevel(3);
    }
    public void VeryHigh()
    {
        QualitySettings.SetQualityLevel(4);
    }
    public void Ultra()
    {
        QualitySettings.SetQualityLevel(5);
    }
}
