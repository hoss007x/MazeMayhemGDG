using UnityEngine;

public class TurrentAI : MonoBehaviour
{
    Transform Player;

    float distance;

    public float Detect;
    public float FireRate, nextFire;


    public Transform head, barrel;
    public GameObject Projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= Detect)
        {
            head.LookAt(Player);
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / FireRate;
                shoot();
            }
        }

    }

    void shoot()
    {
        GameObject projectile = Instantiate(Projectile, barrel.position, head.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Destroy(projectile, 10);
        rb.AddForce(head.forward * 500f);
    }





}
