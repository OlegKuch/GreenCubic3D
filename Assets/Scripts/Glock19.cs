using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glock19 : MonoBehaviour
{
    Animator anim;
    private AudioSource audioSrc;
    public Text ammoLabel;
    public Text magazineLabel;
    public GameObject bulletHolePrefab;
    public GameObject bulletShellPrefab;
    private new GameObject camera;
    float shootTimer = 1f;
    float fireRate = 0.15f;
    public uint magazine = 15;
    public uint ammo = 0;
    bool isReloading = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        camera = GameObject.FindWithTag("MainCamera");
        audioSrc = GetComponent<AudioSource>();
    }
    void Update()
    {
        magazineLabel.text = magazine.ToString();
        ammoLabel.text = ammo.ToString();
        if(!isReloading)
        {
            shootTimer += Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(shootTimer >= fireRate && magazine > 0)
                {
                    Shoot();
                    shootTimer = 0;
                    magazine--;
                }
            }
            else if(Input.GetKeyDown(KeyCode.R) && magazine < 15 && ammo > 0)
            {
                anim.Play("reload");
                isReloading = true;
                Invoke("Reload", 2.5f);
            }
            if(magazine == 0 && ammo > 0 && shootTimer >= fireRate)
            {
                anim.Play("reload");
                isReloading = true;
                Invoke("Reload", 2.5f);
            }
        }
    }
    void Shoot()
    {
        anim.Play("shoot");
        audioSrc.Play();
        Debug.Log("Glock shot!");
        RaycastHit shot;
        if(Physics.Raycast(camera.transform.position,camera.transform.forward,out shot,100))
        {
            Debug.Log("Shot " + shot.collider.name + " by glock.");
            Vector3 direction = shot.point - transform.position;
            
            GameObject newHole = Instantiate(bulletHolePrefab,shot.point + shot.normal * 0.001f,Quaternion.LookRotation(shot.normal) * Quaternion.Euler(0,180,0));
            newHole.transform.parent = shot.collider.gameObject.transform;
            if(shot.rigidbody) // Kick
            {
                shot.rigidbody.AddForceAtPosition(transform.TransformDirection(Vector3.right) * 250f,shot.point);
            }
        }
        // Bullet shells
        GameObject newShell = Instantiate(bulletShellPrefab,transform.position + Vector3.up, transform.rotation * Quaternion.Euler(0,0,-90));
        Rigidbody nsr = newShell.GetComponent<Rigidbody>();
        nsr.AddForce(camera.transform.right * 0.1f);
        nsr.AddForce(Vector3.up * 0.1f);
        Destroy(newShell,10);
    }
    void Reload()
    {
        uint difference = 15 - magazine;
        if(difference <= ammo)
        {
            magazine += difference;
            ammo -= difference;
        }
        else
        {
            magazine += ammo;
            ammo -= ammo;
        }
        Debug.Log("Reloaded glock.");
        isReloading = false;
        shootTimer = 1f;
    }
}
