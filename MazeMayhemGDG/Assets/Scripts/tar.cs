using Unity.VisualScripting;
using UnityEngine;

public class tar : MonoBehaviour
{
    [Range(1,5)][SerializeField] float slow;
    float nSpeed;
    float sNSpeed;
    float speedOrig;
    float sSpeedOrig;
    float sprintMod;

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
            sSpeedOrig = GameManager.instance.player.GetComponent<PlayerController>().getSpeedOrig();
            sprintMod = GameManager.instance.player.GetComponent<PlayerController>().getSprintMod();
            sSpeedOrig *= sprintMod;
            sNSpeed = sSpeedOrig / slow;
            speedOrig = GameManager.instance.player.GetComponent<PlayerController>().getSpeedOrig();
            nSpeed = speedOrig / slow;


        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.player.GetComponent<PlayerController>().GetIsSprinting() == true)
            {
                
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(sNSpeed);


            }
            else
            {
               
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(nSpeed);
            }


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

            if (GameManager.instance.player.GetComponent<PlayerController>().GetIsSprinting() == true)
            {
               
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(sSpeedOrig);


            }
            else
            {
               
                GameManager.instance.player.GetComponent<PlayerController>().setSpeed(speedOrig);
            }

        }
    }
}
