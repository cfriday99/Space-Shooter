using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public GameObject shot2;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    public Transform shotSpawn4;
    public float fireRate;
    public float fireRate2;

    private Rigidbody rb;
    private float nextFire;
    private AudioSource audioSource;
    private bool weaponUpgrade;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        weaponUpgrade = false;
    }

    private void Update()
    {
        if (weaponUpgrade == false)
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
            }
        if (weaponUpgrade == true)
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate2;
                //Instantiate(shot2, shotSpawn2.position, shotSpawn2.rotation);
                Instantiate(shot2, shotSpawn2.position, Quaternion.identity);
                Instantiate(shot2, shotSpawn3.position, Quaternion.identity);
                Instantiate(shot2, shotSpawn4.position, Quaternion.identity);
                audioSource.Play();
            }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            weaponUpgrade = true;
        }
    }
}