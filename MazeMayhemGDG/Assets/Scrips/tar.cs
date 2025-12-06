using Unity.VisualScripting;
using UnityEngine;

public class tar : MonoBehaviour
{
    [Range(1,5)][SerializeField] float slow;
    float nSpeed;
    float speedOrig;
    float sprintMod;
    float sSpeed;
    int entered;

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
            
            if (entered == 0)
            {
                entered++;
                speedOrig = GameManager.instance.player.GetComponent<PlayerController>().getSpeedOrig();
                nSpeed = speedOrig / slow;
                sprintMod = GameManager.instance.player.GetComponent<PlayerController>().getSprintMod();
              
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(nSpeed);
            }
            else 
            {
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(nSpeed);
            }
            sSpeed = speedOrig * sprintMod;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {

            if (Input.GetButton("Sprint"))
            {
               
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(sSpeed);
            }
            else
            { 
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(speedOrig); 
            }
           
        }
    }
}
