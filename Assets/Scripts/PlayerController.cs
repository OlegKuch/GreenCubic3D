using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float SprintSpeed = 25f;
    public float WalkSpeed = 15f;
    public float JumpForce = 200f;
    public float MouseSensitivity = 10f;

    private bool _isGrounded;
    private float speed;

    private Rigidbody _rb;
    private GameObject SpawnPoint;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
    }
    void FixedUpdate()
    {
        JumpLogic();
        MovementLogic();
        RotateLogic();
    }
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.fixedDeltaTime);
        if(Input.GetKey(KeyCode.LeftShift)) // Sprint
        {
            speed = SprintSpeed;
        }
        else
        {
            speed = WalkSpeed;
        }
    }
    private void RotateLogic()
    {
        float h = MouseSensitivity * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);
    }
    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if(_isGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce);
            }
        }
    }
    void OnCollisionEnter()
    {
        _isGrounded = true;
    }
    void OnCollisionExit()
    {
        _isGrounded = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Respawn")
        {
            Respawn();
        }
    }
    void Respawn()
    {   
        Debug.Log("Respawning player.");
        this.transform.position = SpawnPoint.transform.position;
        this.transform.rotation = SpawnPoint.transform.rotation;
    }
}