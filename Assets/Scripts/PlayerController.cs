using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SprintSpeed = 25f;
    public float WalkSpeed = 15f;
    public float JumpForce = 200f;
    public float MouseSensitivity = 200f;
    public float groundDist = 1.0f;
    private bool grounded = true;
    private float speed;
    public float DamageSpeed = 0.5f;

    private AudioSource audioSrc;
    private Rigidbody rb;
    public GameObject SpawnPoint;
    private GameObject Camera;
    public GameObject Hand;
    public FixedJoystick WalkJoystick;
    public GameObject glock;
    private Glock19 glockScript;
    public float health = 100, food = 100, water = 100, energy = 100;
    
    public GameObject healthBar, foodBar, waterBar, energyBar;
    RectTransform healthRT, foodRT, waterRT, energyRT;

    Transform carryingObject = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera");
        Cursor.visible = false;
        healthRT = healthBar.GetComponent<RectTransform>();
        foodRT = foodBar.GetComponent<RectTransform>();
        waterRT = waterBar.GetComponent<RectTransform>();
        energyRT = energyBar.GetComponent<RectTransform>();
        audioSrc = GetComponent<AudioSource>();
        glockScript = glock.GetComponent<Glock19>();
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
            health += 0.05f * Time.timeScale;
            food -= 0.05f * Time.timeScale;
            water -= 0.025f * Time.timeScale;
        }
        if(energy < 100 && food > 0.05 && water > 0.07 && !(Input.GetKey(KeyCode.LeftShift))) // Energy regeneration
        {
            energy += 0.2f * Time.timeScale;
            food -= 0.03f * Time.timeScale;
            water -= 0.05f * Time.timeScale;
        }
        if(food < 0.1 || water < 0.1)
        {
            if(energy > 0)
            {
                energy -= 1;
            }
            health -= 0.1f * Time.timeScale;
        }
        if(health < 0.1) // Die
        {
            Debug.Log("You die.");
            Respawn();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.transform.position,Camera.transform.forward,out hit,10)) // Grab objects
            {
                if(hit.rigidbody)
                {
                    if(hit.collider.gameObject.transform.parent)
                    {
                        hit.collider.transform.parent = null;
                    }
                    else
                    {
                        hit.collider.gameObject.transform.parent = Camera.transform;
                    }
                }
            }
        }
    }
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Keyboard
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.fixedDeltaTime);

        moveHorizontal = WalkJoystick.Horizontal; // Joystick
        moveVertical = WalkJoystick.Vertical;
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.fixedDeltaTime);
        if(Input.GetKey(KeyCode.LeftShift) && energy > 0.2) // Sprint
        {
            speed = SprintSpeed;
            if(moveHorizontal != 0.0f || moveVertical != 0.0f)
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
        float h = MouseSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime;
        float v = MouseSensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime;
        Camera.transform.Rotate(v / 3 * -1,0,0);
        Hand.transform.Rotate(v / 3 * -1,0,0);  
        transform.Rotate(0,h,0); 
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
        if(collider.gameObject.tag == "9x19mm")
        {
            collider.gameObject.SetActive(false);
            Destroy(collider.gameObject);
            glockScript.ammo++;
            audioSrc.Play();
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
}