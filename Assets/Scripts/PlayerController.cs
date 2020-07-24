using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float SprintSpeed = 25f;
    public float WalkSpeed = 15f;
    public float JumpForce = 200f;
    public float MouseSensitivity = 10f;
    public float groundDist = 1.0f;
    private bool grounded = true;
    private float speed;
    public float DamageSpeed = 0.5f;

    private Rigidbody rb;
    public GameObject SpawnPoint;

    public float health = 100, food = 100, water = 100, energy = 100;
    
    public GameObject healthBar, foodBar, waterBar, energyBar;
    RectTransform healthRT, foodRT, waterRT, energyRT;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        healthRT = healthBar.GetComponent<RectTransform>();
        foodRT = foodBar.GetComponent<RectTransform>();
        waterRT = waterBar.GetComponent<RectTransform>();
        energyRT = energyBar.GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {
        CheckGround();
        JumpLogic();
        MovementLogic();
        RotateLogic();
    }
    void Update()
    {
        SetBars();
        if(health < 100 && food > 0.05 && water > 0.025) // Health regeneration
        {
            health += 0.05f;
            food -= 0.05f;
            water -= 0.025f;
        }
        if(energy < 100 && food > 0.05 && water > 0.07 && !(Input.GetKey(KeyCode.LeftShift))) // Energy regeneration
        {
            energy += 0.2f;
            food -= 0.03f;
            water -= 0.05f;
        }
        if(food < 0.1 || water < 0.1)
        {
            if(energy > 0)
            {
                energy -= 1;
            }
            health -= 0.1f;
        }
        if(health < 0.1) // Die
        {
            Debug.Log("You die.");
            Respawn();
        }
    }
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(movement * speed * Time.fixedDeltaTime);
        if(Input.GetKey(KeyCode.LeftShift) && energy > 0.2) // Sprint
        {
            speed = SprintSpeed;
            if(moveHorizontal > 0.1f)
            {
                energy -= 0.2f;
            }
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
            if(grounded && energy > 5)
            {
                rb.AddForce(Vector3.up * JumpForce);
                energy -= 5;
            }
        }
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
        this.transform.position = SpawnPoint.transform.position; // Teleport to spawnpoint
        this.transform.rotation = SpawnPoint.transform.rotation; // Rotate as spawnpoint
        rb.velocity = new Vector3(0,0,0);
        health = 100;
        food = 100;
        water = 100;
        energy = 100;
    }
    void CheckGround()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast (ray, out hit)) 
        {
            if(hit.distance - 0.5f <= groundDist)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
         }
    }
    void SetBars()
    {
        healthRT.sizeDelta = new Vector2(health * 2.5f,20);
        foodRT.sizeDelta = new Vector2(food * 2.5f,20);
        waterRT.sizeDelta = new Vector2(water * 2.5f,20);
        energyRT.sizeDelta = new Vector2(energy * 2.5f,20);
    }
    void OnCollisionEnter()
    {
        if(rb.velocity.y < DamageSpeed && grounded) // Fall Damage
        {
            health -= (rb.velocity.y + DamageSpeed) * -20;
            Debug.Log("You get fall damage.");
            Debug.Log(rb.velocity.y);
        }
    }
}