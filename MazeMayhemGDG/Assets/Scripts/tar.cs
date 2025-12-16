using Unity.VisualScripting;
using UnityEngine;

public class tar : MonoBehaviour
{
    [Range(1,5)][SerializeField] float slow;
    //new speed
    float nSpeed;
    //Sprinting new speed
    float sNSpeed;
    //New speed w/ active speed buff
    float nASpeed;
    //Sprinting new speed w/ active speed buff
    float sNASpeed;
    //Original speed
    float speedOrig;
    //Sprinting Original speed
    float sSpeedOrig;
    //Original speed w/ active speed buff
    float aSpeedOrig;
    //Sprinting original speed w/ active speed buff
    float sASpeedOrig;
    //Sprint modifier
    float sprintMod;
    int speedBoostAmount;

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
            speedBoostAmount = GameManager.instance.player.GetComponent<PlayerController>().GetSpeedBuff();
            sNASpeed = sNSpeed * speedBoostAmount;
            nASpeed = nSpeed * speedBoostAmount;

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
            if (GameManager.instance.player.GetComponent<PlayerController>().GetSpeedActive() == true)
            {
                if (GameManager.instance.player.GetComponent<PlayerController>().GetIsSprinting() == true)
                {
                    

                    GameManager.instance.player.GetComponent<PlayerController>().setSpeed(sNASpeed);


                }
                else
                {
                    
                    GameManager.instance.player.GetComponent<PlayerController>().setSpeed(nASpeed);
                }
            }
            else
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
