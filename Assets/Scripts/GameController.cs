using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject door;
    private AudioSource switchAudio;
    private Light lightSource;
    public GameObject lightSwitch;
    public GameObject lustreLight;
    public GameObject switchButton;

    public AudioClip switchOnSound,switchOffSound;

    private bool nearDoor;
    private bool doorOpened = false;
    private bool nearSwitch = false;
    private bool lightOn = true;

    void Start()
    {
        door = GameObject.FindWithTag("Door");
        lightSource = lustreLight.GetComponent<Light>();
        switchAudio = lightSwitch.GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) // Reload level
        {
            ReloadLevel();
        }
        if(Input.GetKeyDown(KeyCode.Escape)) // Exit game
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) // Some interacts with right click
        {
            if(nearDoor) // Open/Close the door
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
                    door.transform.rotation = Quaternion.Euler(0,180,0);
                }
            }
            if(nearSwitch) // On/Off the light
            {
                if(lightOn)
                {
                    Debug.Log("Turned off the light");
                    lightOn = false;
                    lightSource.enabled = false;
                    switchButton.transform.rotation = Quaternion.Euler(0,0,-5);
                    switchAudio.clip = switchOffSound;
                    switchAudio.Play();
                }
                else
                {
                    Debug.Log("Turned on the light");
                    lightOn = true;
                    lightSource.enabled = true;
                    switchButton.transform.rotation = Quaternion.Euler(0,0,5);
                    switchAudio.clip = switchOnSound;
                    switchAudio.Play();
                }
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
        else if(collider.tag == "LightSwitch")
        {
            nearSwitch = true;
            Debug.Log("You are near the light switch.");
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Door")
        {
            nearDoor = false;
            Debug.Log("You are away from door.");
        }
        else if(collider.tag == "LightSwitch")
        {
            nearSwitch = false;
            Debug.Log("You are away from light switch.");
        }
    }
    
}
