using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;

    public TMP_Text gameGoalCountText;
    public TMP_Text KeyCountText;
    public Image playerHPBar;
    public GameObject playerDamagePanel;

    public Image speedIcon;
    public Image strengthIcon;
    public Image healingIcon;

    public GameObject player;
    public PlayerController playerScript;
    public GameObject TextHitBox;

    public bool isPaused;

    float timeScaleOrig;

    public int gameGoalCount;
    public int Keys;

    void Awake()
    {
       
        instance = this;
        timeScaleOrig = Time.timeScale;
      
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(true);
            }
            else if(menuActive == menuPause)
            {
                stateUnpause();
            }
        }
    }

    public void statePause()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void stateUnpause()
    {
        isPaused = false;
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(false);
        menuActive = null;
    }

    public void updateGameGoal(int amount)
    {
        gameGoalCount += amount;
        gameGoalCountText.text = gameGoalCount.ToString("F0");
    }

    public void updateKeyAmount(int amount)
    {
        Keys += amount;
        KeyCountText.text = Keys.ToString("F0");
    }

    public void youWin()
    {
               statePause();
        menuActive = menuWin;
        menuActive.SetActive(true);
    }

    public void youLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }
}
