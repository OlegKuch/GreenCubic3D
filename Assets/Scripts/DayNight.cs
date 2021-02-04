using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float speed = 1.0f;
    
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime,0,0);
    }
}
