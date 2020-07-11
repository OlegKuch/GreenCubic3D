using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject door;

    private bool nearDoor;
    private bool doorOpened = false;

    void Start()
    {
        door = GameObject.FindWithTag("Door");
    }
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
        if(Input.GetKeyDown(KeyCode.Mouse1) && nearDoor) // Open/Close the door
        {
            if(doorOpened)
            {
                Debug.Log("Closed the door.");
                doorOpened = false;
                door.transform.rotation = Quaternion.Euler(0,-90,0);
            }
            else
            {
                Debug.Log("Opened the door.");
                doorOpened = true;
                door.transform.rotation = Quaternion.Euler(0,180,0);;
            }
        }
    }
    void ReloadLevel()
    {
        Debug.Log("Reloading level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Door")
        {
            nearDoor = true;
            Debug.Log("You are near the door.");
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Door")
        {
            nearDoor = false;
            Debug.Log("You are away from door.");
        }
    }
}
