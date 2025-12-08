using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player") && GameManager.instance.Keys == 0 && GameManager.instance.gameGoalCount == 0)
        {
            GameManager.instance.youWin();
        }
    }
}
