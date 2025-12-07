using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject targetTeleporter;
    [SerializeField] GameObject teleporter;

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
        if (other.CompareTag("Player"))
        {
            
            Vector3 targetPosition = targetTeleporter.transform.position;
            targetPosition.y += 1.0f; // Adjust height to avoid clipping into the ground
            other.transform.position = targetPosition;
            
        }
    }

}
