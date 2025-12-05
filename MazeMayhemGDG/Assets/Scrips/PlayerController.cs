using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
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

    // Timer for shooting
    float ShootTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (Input.GetButtonDown("Fire1") && ShootTimer >= ShootRate)
        {
            // Handle shooting
            Shoot();
        }
    }

    void Sprint()
    {
        // Check if the sprint button is pressed
        if (Input.GetButtonDown("Sprint"))
        {
            // Increase speed when sprint button is pressed
            Speed *= SprintModifier;
        }
        else if(Input.GetButtonUp("Sprint"))
        {

            // Reset speed when sprint button is released
            Speed /= SprintModifier;
        }
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

        // Declare a RaycastHit variable to store hit information
        RaycastHit hit;
        // Perform a raycast from the camera's position forward
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ShootDistances, ~ignoreLayer))
        {
            // Log the name of the hit object
            Debug.Log("Hit: " + hit.collider.name);

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

    IEnumerator screenFlashDamage()
    {
        GameManager.instance.playerDamagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerDamagePanel.SetActive(false);
    }
}
