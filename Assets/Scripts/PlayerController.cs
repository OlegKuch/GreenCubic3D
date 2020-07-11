﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float SprintSpeed = 25f;
    public float WalkSpeed = 15f;
    public float JumpForce = 200f;
    public float MouseSensitivity = 10f;
    private bool grounded = true;
    private float speed;

    private Rigidbody rb;
    public GameObject SpawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
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
            if(grounded)
            {
                rb.AddForce(Vector3.up * JumpForce);
            }
        }
    }
    void OnCollisionEnter()
    {
        grounded = true;
    }
    void OnCollisionExit()
    {
        grounded = false;
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
    }
}