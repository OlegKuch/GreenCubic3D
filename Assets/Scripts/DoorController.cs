using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool nearDoor;
    private bool doorOpened = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && nearDoor && Time.timeScale != 0) // Open/Close the door
        {
            if(doorOpened)
            {   
                Debug.Log("Closed the door.");
                doorOpened = false;
                transform.rotation = Quaternion.Euler(0,-90,0);
            }
            else
            {
                Debug.Log("Opened the door.");
                doorOpened = true;
                transform.rotation = Quaternion.Euler(0,180,0);
            }
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            nearDoor = true;
            Debug.Log("You are near the door.");
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            nearDoor = false;
            Debug.Log("You are away from door.");
        }
    }
}
