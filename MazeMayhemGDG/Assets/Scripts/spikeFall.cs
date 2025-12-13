using UnityEngine;

public class spikeFall : MonoBehaviour
{
    [SerializeField] int Gravity;

    Vector3 SpikeVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;

        }
        if (other.CompareTag("Player"))
        {
            SpikeVelocity.y -= Gravity * Time.deltaTime;
        }
    }
}
    
    
