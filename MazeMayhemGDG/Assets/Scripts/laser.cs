using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class laser : MonoBehaviour
{
    [SerializeField] LineRenderer laserLine;

    [SerializeField] Transform model;
    [SerializeField] GameObject hitEffect;

    [SerializeField] int distance;
    [SerializeField] int damageAmount;
    [SerializeField] float damageTime;
    [SerializeField] int rotationSpeed;

    bool isDamaging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        model.transform.RotateAround(model.position, Vector3.up, Time.deltaTime * rotationSpeed);

        createLaser();

    }

    void createLaser()
    {
        RaycastHit hit;
        if(Physics.Raycast(model.position, model.transform.forward, out hit, distance))
        {
            Debug.DrawLine(model.position, hit.point);

            laserLine.SetPosition(0, model.position);
            laserLine.SetPosition(1, hit.point);
            hitEffect.SetActive(true);
            hitEffect.transform.position = hit.point;

            IDamage dmg = hit.collider.GetComponent<IDamage>(); 
            if(dmg != null && !isDamaging)
            {
                StartCoroutine(damageTimer(dmg));
            }
        }
        else
        {
            laserLine.SetPosition(0, model.position);
            laserLine.SetPosition(1, model.position + model.forward * distance);
            hitEffect.SetActive(false);
        }
    }

    IEnumerator damageTimer(IDamage d)
    {
        isDamaging = true;
        d.TakeDamage(damageAmount);
        yield return new WaitForSeconds(damageTime);
        isDamaging = false;
    }
}
