using UnityEngine;
using TMPro;

public class Textspawner : MonoBehaviour
{
    [SerializeField] int TextTimeShown;
    [SerializeField] TMP_Text Text;
    [SerializeField] string message;

    public bool timer;
    public bool ShowText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Hide text at the start
        if (Text != null)
            Text.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        // Check if the object entering is the player
        if (other.CompareTag("Player") && Text != null)
        {
            ShowText = true;
            Text.text = message;
            Text.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (ShowText == true && timer == true)
        {
            Invoke("HideText", TextTimeShown);
        }
        else if(ShowText == true && timer == false)
        {
            if (Input.GetButtonDown("Submit"))
            {
                HideText();
            }
        }
    }

    private void HideText()
   {
      Text.gameObject.SetActive(false);
      ShowText = false;
      timer = false;
      Destroy(gameObject);
    }
}
