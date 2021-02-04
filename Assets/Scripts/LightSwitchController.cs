using UnityEngine;

public class LightSwitchController : MonoBehaviour
{
    private AudioSource switchAudio;
    private Light lightSource;

    public GameObject lustreLight;
    public GameObject switchButton;
    public AudioClip switchOnSound,switchOffSound;

    private bool nearSwitch = false;
    private bool lightOn = true;

    void Start()
    {
        lightSource = lustreLight.GetComponent<Light>();
        switchAudio = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && nearSwitch && Time.timeScale != 0) // On/Off the light
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
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            nearSwitch = true;
            Debug.Log("You are near the light switch.");
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            nearSwitch = false;
            Debug.Log("You are away from light switch.");
        }
    }
}
