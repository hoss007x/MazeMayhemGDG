using Unity.VisualScripting;
using UnityEngine;

public class tar : MonoBehaviour
{
    [Range(1,5)][SerializeField] int slow;
    float nSpeed;
    float speedOri;

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
        if (other.CompareTag("player"))
        {
            speedOri = GameManager.instance.player.GetComponent<PlayerController>().getSpeed();
            nSpeed = speedOri / slow;
            GameManager.instance.player.GetComponent<PlayerController>().setSpeed(nSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            GameManager.instance.player.GetComponent<PlayerController>().setSpeed(speedOri);
        }
    }
}
