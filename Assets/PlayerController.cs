using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    // wartosc wczytana z klawiatury
    Vector2 input;


    //mnoznik przyspieszenia
    public float enginePower = 10;

    public float gyroPower = 3;


    private CameraScript cs;


    public GameObject bulletPrefab;


    public Transform gunLeft, gunRight;


    public float bulletSpeed = 30;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        cs = Camera.main.transform.GetComponent<CameraScript>();

        input = Vector2.zero;

        gunLeft = transform.Find("GunLeft").transform;
        gunRight = transform.Find("GunRight").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // sterowanie w poziomie AD
        float x = Input.GetAxis("Horizontal");

        // WS
        float y = Input.GetAxis("Vertical");


        input.x = x;
        input.y = y;


        // teleportuj jesli wyjdzie z ekranu

        if(Mathf.Abs(transform.position.x) > cs.gameWidth / 2)
        {
            Vector3 newPosition = new Vector3(transform.position.x * (-0.99f),
                                        0,
                                        transform.position.z);

            transform.position = newPosition;

        }

        if (Mathf.Abs(transform.position.z) > cs.gameHeight / 2)
        {
            Vector3 newPosition = new Vector3(transform.position.x,
                                                0,
                                                transform.position.z * (-0.99f));

            transform.position = newPosition;

        }

        if (Input.GetKeyDown(KeyCode.Space))
            Fire();


    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * input.y * enginePower, ForceMode.Acceleration);

        rb.AddTorque(transform.up * input.x * gyroPower, ForceMode.Acceleration);
    }


    void Fire()
    {
        GameObject leftBullet = Instantiate(bulletPrefab, gunLeft.position,
                                                        Quaternion.identity);
        leftBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed,
                                                        ForceMode.VelocityChange);

        Destroy(leftBullet, 5);
    }
}
