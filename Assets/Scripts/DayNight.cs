using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float rotateSpeed = 1;
    Vector3 rotate = new Vector3(1,0,0);
    void Update()
    {
        transform.Rotate(rotate);
    }
}
