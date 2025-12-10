using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage, ITypesOfItems, IPickup
{
    // Cache the CharacterController component
    [SerializeField] CharacterController CharacterController;
    [SerializeField] LayerMask ignoreLayer;

    // Movement parameters
    // Player hit points
    [SerializeField] int HP;
    // Base movement speed
    [SerializeField] float Speed;
    // Sprint modifier
    [SerializeField] float SprintModifier;
    // Jump parameters
    [SerializeField] int JumpSpeed;
    // Maximum number of jumps
    [SerializeField] int MaxJumps;
    // Gravity force
    [SerializeField] int Gravity;
    // Item buff timer
    [SerializeField] int itemBuffTime;

    // List of guns
    [SerializeField] List<gunStats> gunList = new List<gunStats>();
    // Current gun index
    [SerializeField] GameObject gunModel;
    // Shooting parameters
    [SerializeField] int ShootDamage;
    // Shooting parameters
    [SerializeField] int ShootDistances;
    // Time between shots
    [SerializeField] float ShootRate;

    // Movement direction
    Vector3 MoveDirection;
    // Player velocity
    Vector3 PlayerVelocity;

    // Current jump count
    int JumpCount;
    // Original HP
    int HPOrig;
    //Speed buff amount
    int SpeedBuffAmount;
    // Current gun list position
    int gunListPos;

    // Timer for shooting
    float ShootTimer;
    float speedOrig;

    bool sprinting;
    bool speedActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedOrig = Speed;

        HPOrig = HP;
        UpdatePlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * ShootDistances, Color.red);

        // Update the shoot timer
        ShootTimer += Time.deltaTime;

        // Handle player movement
        Movement();
        // Handle sprinting
        Sprint();
    }

    // Handle player movement
    void Movement()
    {

        // Check if the player is grounded
        if (CharacterController.isGrounded)
        {
            // Reset vertical velocity when grounded
            PlayerVelocity = Vector3.zero;
            // Reset jump count when grounded
            JumpCount = 0;
        }
        else
        {

            // Apply gravity when not grounded
            PlayerVelocity.y -= Gravity * Time.deltaTime;
        }

        // Get input for movement direction
        MoveDirection = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        // Move the player
        CharacterController.Move(MoveDirection * Speed * Time.deltaTime);

        // Handle jumping
        Jump();
        // Apply vertical velocity
        CharacterController.Move(PlayerVelocity * Time.deltaTime);

        // Handle shooting
        if (Input.GetButton("Fire1") && gunList.Count > 0 && gunList[gunListPos].ammoCurr > 0 && ShootTimer >= ShootRate)
        {
            // Handle shooting
            Shoot();
        }

        selectGun();
        reload();
    }

    void Sprint()
    {
        // Check if the sprint button is pressed
        if (Input.GetButtonDown("Sprint"))
        {
            sprinting = true;
            // Increase speed when sprint button is pressed
            Speed *= SprintModifier;
            
        }
        else if(Input.GetButtonUp("Sprint"))
        {
            sprinting = false;
            // Reset speed when sprint button is released
            Speed /= SprintModifier;

        }
    }
   public float getSprintMod()
    {
        return SprintModifier;
    }

    // Handle jumping
    void Jump()
    {
        // Check if the jump button is pressed and if the player can jump
        if (Input.GetButtonDown("Jump") && JumpCount < MaxJumps)
        {
            // Apply jump speed to the player's vertical velocity
            PlayerVelocity.y = JumpSpeed;
            // Increment jump count
            JumpCount++;
        }
    }

    void Shoot()
    {
        // Check if the shoot button is pressed and if the shoot timer has exceeded the shoot rate
        ShootTimer = 0;

        gunList[gunListPos].ammoCurr--;

        // Declare a RaycastHit variable to store hit information
        RaycastHit hit;
        // Perform a raycast from the camera's position forward
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ShootDistances, ~ignoreLayer))
        {
            // Log the name of the hit object
            Debug.Log("Hit: " + hit.collider.name);

            // Instantiate the hit effect at the hit point
            Instantiate(gunList[gunListPos].hitEffect, hit.point, Quaternion.identity);

            // Try to get the IDamage component from the hit object
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            // Check if the hit object has an IDamage component
            if (dmg != null)
            {
                // Apply damage to the hit object
                dmg.TakeDamage(ShootDamage);
            }
        }
    }
    void reload()
    {
        if (Input.GetButtonDown("Reload") && gunList.Count > 0)
        {
            gunList[gunListPos].ammoCurr = gunList[gunListPos].ammoMax;
        }
    }

    public void getGunStats(gunStats gun)
    {
        gunList.Add(gun);
        gunListPos = gunList.Count - 1;

        changeGun();
    }

    void changeGun()
    {
        ShootDamage = gunList[gunListPos].ShootDamage;
        ShootDistances = gunList[gunListPos].ShootDistances;
        ShootRate = gunList[gunListPos].ShootRate;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[gunListPos].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[gunListPos].gunModel.GetComponent<MeshRenderer>().sharedMaterial;


    }


    void selectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && gunListPos < gunList.Count - 1)
        {
            gunListPos++;
            changeGun();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && gunListPos > 0)
        {
            gunListPos--;

            changeGun();
        }


    }

    public void TakeDamage(int amount)
    {
        // Remove an amount from the HP
        HP -= amount;
        UpdatePlayerUI();
        StartCoroutine(screenFlashDamage());

        if (HP <= 0)
        {
            // You Lose!
            GameManager.instance.youLose();
        }
    }

    public void UpdatePlayerUI()
    {
        GameManager.instance.playerHPBar.fillAmount = (float)HP / HPOrig;
    }
    public void setSpeed(float nSpeed)
    {
        Speed = nSpeed;
    }
    public float getSpeed()
    {
        return Speed;
    }
   public float getSpeedOrig()
    {
        if (Input.GetButton("Sprint"))
        {
            Speed = speedOrig;
        }
        speedOrig = Speed;
        return speedOrig;
    }
    
    IEnumerator screenFlashDamage()
    {
        GameManager.instance.playerDamagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerDamagePanel.SetActive(false);
    }

    //Handle healing
    public void healing(int amount)
    {
        HP += amount;
        UpdatePlayerUI();
    }
    //Handle speed buff 
    public void faster(int amount)
    {
        SpeedBuffAmount = amount;
        speedActive = true;
        if (sprinting)
        {
            Speed /= SprintModifier;
            Speed += amount;
            Speed *= SprintModifier;
        }
        else
        {
            Speed += amount;
        }
        
        StartCoroutine(FasterTimer(amount));
    }
    //Handle speed buff timer
    IEnumerator FasterTimer(int amount)
    {
        yield return new WaitForSeconds(itemBuffTime);
        speedActive= false;
        if (sprinting)
        { 

            Speed /= SprintModifier;
            Speed -= amount;
            Speed *= SprintModifier;
        }
        else
        {
            Speed = speedOrig;
        }
    }
    //Handle damage buff
    public void stronger(int amount)
    {
        ShootDamage += amount;
        StartCoroutine(StrongerTimer(amount));
    }
    //Handle damage buff timer
    IEnumerator StrongerTimer(int amount)
    {
        yield return new WaitForSeconds(itemBuffTime);
        ShootDamage -= amount;

    }
    public bool GetIsSprinting()
    {
        return sprinting;
    }
    public bool GetSpeedActive()
    {
        return speedActive;
    }
    public int GetSpeedBuff()
    {
       return SpeedBuffAmount;
    }
}
